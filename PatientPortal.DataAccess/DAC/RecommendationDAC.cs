//-----------------------------------------------------------------------
// <copyright file="RecommendationDAC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the RecommendationDAC.cs file.</summary>
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
    public class RecommendationDAC : DACBase, IRecommendationDAC
    {
        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecommendationDAC"/> class.
        /// </summary>
        /// <param name="exceptionManager">Exception manager.</param>
        public RecommendationDAC(IExceptionManager exceptionManager)
        {
            this.exceptionManager = exceptionManager;
        }

        /// <summary>
        /// Get Recommendation list for the filled survey
        /// </summary>
        /// <returns>object of type IList<IRecommendedProductDTO></returns>
        public IList<IRecommendedProductDTO> GetRecommendationList(string Guid)
        {
            IList<IRecommendedProductDTO> productList = new List<IRecommendedProductDTO>();
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
                //join SQM in surveyQuestionMap on new { p1 = SQM.AnswerId, p2 = SQM.QuestionId, p3 = SQM.SurveyId } equals new { p1 = (int?)SQM.AnswerId, p2 = SQM.QuestionId, p3 = SQM.SurveyId }

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
                            TextInputType = x.TextInputType,
                            DuplicateProductConstraint = string.Empty
                        });
                productList = recommendation.OrderBy(c => c.ProductName).ToList<IRecommendedProductDTO>();
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while fetching survey list.", ex);
            }

            return productList;
        }

    }
}