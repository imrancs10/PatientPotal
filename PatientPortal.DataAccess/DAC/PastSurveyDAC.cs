//-----------------------------------------------------------------------
// <copyright file="PastSurveyDAC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the PastSurveyDAC.cs file.</summary>
//-----------------------------------------------------------------------


namespace PatientPortal.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using PatientPortal.Entities.Entities;
    using PatientPortal.Shared;
    using PatientPortal.DTOModel;
    using PatientPortal.Entities;
    using AutoMapper;
    using System.Globalization;
    /// <summary>
    /// Represents Country Data Access Component.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public class PastSurveyDAC : DACBase, IPastSurveyDAC
    {
        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="PastSurveyDAC"/> class.
        /// </summary>
        /// <param name="exceptionManager">Exception manager.</param>
        public PastSurveyDAC(IExceptionManager exceptionManager)
        {
            this.exceptionManager = exceptionManager;
        }

        /// <summary>
        /// This method will use to get the recommended product list.
        /// </summary>
        /// <returns>object of type IList<IRecommendedProductDTO></returns>
        public IList<IRecommendedProductDTO> GetRecommendationList(string Guid)
        {
            IList<IRecommendedProductDTO> IProductList = new List<IRecommendedProductDTO>();
            try
            {
                PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();
                IQueryable<Product> products = PatientPortalEntities.Products.AsQueryable<Product>();
                IQueryable<ProductImage> productImages = PatientPortalEntities.ProductImages.AsQueryable<ProductImage>();
                IQueryable<Survey> surveys = PatientPortalEntities.Surveys.AsQueryable<Survey>();
                IQueryable<Question> question = PatientPortalEntities.Questions.AsQueryable<Question>();
                IQueryable<Answer> answers = PatientPortalEntities.Answers.AsQueryable<Answer>();
                IQueryable<SurveyQuestionAnswer> surveyQuestionAnswer = PatientPortalEntities.SurveyQuestionAnswers.AsQueryable<SurveyQuestionAnswer>();
                //IQueryable<SurveyQuestionMap> surveyQuestionMap = PatientPortalEntities.SurveyQuestionMaps.AsQueryable<SurveyQuestionMap>();
                  //join SQM in surveyQuestionMap on new { p1 = SQA.AnswerId, p2 = SQA.QuestionId, p3 = SQA.SurveyId } equals new { p1 = (int?)SQM.AnswerId, p2 = SQM.QuestionId, p3 = SQM.SurveyId }

                var recommendation = (
                   from SQA in surveyQuestionAnswer
                   join P in products on SQA.ProductId equals P.Id
                   join S in surveys on SQA.SurveyId equals S.Id
                   join Q in question on SQA.QuestionId equals Q.Id
                   join A in answers on SQA.AnswerId equals A.Id
                   join PI in productImages on P.Id equals PI.ProductId into tempJoin
                   from PI in tempJoin.DefaultIfEmpty()
                   where SQA.Guid == new Guid(Guid) && SQA.ProductId != null && ((PI != null && PI.IsPrimary == true) || PI == null)
                   select new
                   {
                       ProductId = P.Id,
                       ProductLanguageId = P.LanguageId,
                       ProductName = P.Name,
                       ProductDescription = P.Description,
                       SurveyId = SQA.SurveyId,
                       ProductURL = P.ProductURL,
                       RecommendedCount = SQA.TextInput,
                       SurveyName = S.Name,
                       AnswerTitle = A.Title,
                       ProductMainImage = PI != null ? PI.ImagePath : string.Empty,
                       TextInputType = Q.TextInputType.Id
                   }
                   ).ToList()
                        .Select(x => new RecommendedProductDTO()
                        {
                            ProductId = x.ProductId,
                            ProductLanguageId = x.ProductLanguageId,
                            ProductName = x.ProductName,
                            ProductDescription = x.ProductDescription,
                            SurveyId = x.SurveyId,
                            SurveyName = x.SurveyName,
                            ProductURL = x.ProductURL,
                            RecommendedCount = x.RecommendedCount,
                            AnswerTitle = x.AnswerTitle,
                            ProductMainImage = x.ProductMainImage,
                            TextInputType = x.TextInputType
                        });
                IProductList = recommendation.OrderBy(c => c.ProductName).ToList<IRecommendedProductDTO>();
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while fetching survey list.", ex);
            }

            return IProductList;
        }

        /// <summary>
        /// This method will use to get the past surveys list of a user.
        /// </summary>
        /// <returns></returns>
        public IList<IPastSurveyDTO> GetPastSurveyList(int userId)
        {
            IList<IPastSurveyDTO> pastSurveyList = new List<IPastSurveyDTO>();
            try
            {
                PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();
                IQueryable<SurveyQuestionAnswer> surveyQuestionAnswer = PatientPortalEntities.SurveyQuestionAnswers.AsQueryable<SurveyQuestionAnswer>();
                IQueryable<SurveyQuestionMap> surveyQuestionMap = PatientPortalEntities.SurveyQuestionMaps.AsQueryable<SurveyQuestionMap>();
                IQueryable<Survey> surveys = PatientPortalEntities.Surveys.AsQueryable<Survey>();
                var pastsurveys = (from SQA in surveyQuestionAnswer
                                   join S in surveys on SQA.SurveyId equals S.Id
                                   where SQA.UserId == userId
                                   select new
                                   {
                                       SurveyId = SQA.SurveyId,
                                       SurveyName = S.Name,
                                       SurveyTakenDate = SQA.CreatedDate,
                                       Guid = SQA.Guid,
                                       LanguageId = S.LanguageId
                                   }).Distinct().OrderByDescending(x => x.SurveyTakenDate).ToList().Select(x => new PastSurveyDTO()
                        {
                            SurveyId = x.SurveyId,
                            SurveyName = x.SurveyName,
                            SurveyTakenDate = x.SurveyTakenDate,
                            Guid = x.Guid,
                            LanguageId = x.LanguageId
                        });
                pastSurveyList = pastsurveys.ToList<IPastSurveyDTO>();
                for (int i = 0; i < pastsurveys.Count(); i++)
                {
                    IList<IRecommendedProductDTO> recommendedProducts = GetRecommendationList((pastsurveys.ToList()[i] as PastSurveyDTO).Guid.ToString());
                    foreach (IRecommendedProductDTO dtoProduct in recommendedProducts)
                    {
                        (pastSurveyList[i] as PastSurveyDTO).RecommendationProducts.Add(new ProductDTO() { Id = dtoProduct.ProductId, Name = dtoProduct.ProductName });
                        (pastSurveyList[i] as PastSurveyDTO).Recommendations += dtoProduct.ProductName + ", ";
                    }
                    if (!string.IsNullOrEmpty((pastSurveyList[i] as PastSurveyDTO).Recommendations))
                    {
                        string recommendation = (pastSurveyList[i] as PastSurveyDTO).Recommendations;
                        (pastSurveyList[i] as PastSurveyDTO).Recommendations = recommendation.Remove(recommendation.LastIndexOf(','), 1);
                    }
                    pastSurveyList[i].SurveyTakenDateDisplay = pastSurveyList[i].SurveyTakenDate.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                }
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while fetching past surveys list.", ex);
            }

            return pastSurveyList;
        }

        /// <summary>
        /// Get Past Survey Question Answers list
        /// </summary>
        /// <param name="Id">object of type string</param>
        /// <returns>object of type IList<ISurveyQuestionAnswerDTO></returns>
        public IList<ISurveyQuestionAnswerDTO> GetPastSurveyQuestionList(string Guid)
        {
            IList<ISurveyQuestionAnswerDTO> surveyList = new List<ISurveyQuestionAnswerDTO>();
            try
            {
                if (!string.IsNullOrEmpty(Guid))
                {
                    PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();

                    List<SurveyQuestionAnswer> surveys = PatientPortalEntities.SurveyQuestionAnswers.Include("Survey").Include("Question").Include("Question.QuestionType").Include("Question.Answers").Where(x => x.Guid == new Guid(Guid)).OrderBy(x => x.Id).ToList<SurveyQuestionAnswer>();

                    foreach (SurveyQuestionAnswer survey in surveys)
                    {
                        var surveyDTO = Mapper.Map<SurveyQuestionAnswer, SurveyQuestionAnswerDTO>(survey);
                        surveyList.Add(surveyDTO);
                    }
                }
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while fetching survey list.", ex);
            }

            return surveyList;
        }

    }
}