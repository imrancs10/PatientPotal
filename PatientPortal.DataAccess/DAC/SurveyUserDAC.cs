//-----------------------------------------------------------------------
// <copyright file="SurveyUserDAC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the SurveyUserDAC.cs file.</summary>
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
    public class SurveyUserDAC : DACBase, ISurveyUserDAC
    {
        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="SurveyUserDAC"/> class.
        /// </summary>
        /// <param name="exceptionManager">Exception manager.</param>
        public SurveyUserDAC(IExceptionManager exceptionManager)
        {
            this.exceptionManager = exceptionManager;
        }

        /// <summary>
        /// Get Question answers list on given survey Id.
        /// </summary>
        /// <param name="Id">object of type string</param>
        /// <returns></returns>
        public IList<ISurveyQuestionMapDTO> GetSurveyQuestionList(string surveyId)
        {
            IList<ISurveyQuestionMapDTO> surveyList = new List<ISurveyQuestionMapDTO>();
            try
            {
                if (!string.IsNullOrEmpty(surveyId))
                {
                    string[] surveyIds = surveyId.Split('/');
                    int surveyID = Convert.ToInt32(surveyIds[0]);
                    //int languageID = Int32.Parse(surveyIds[1]);
                    PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();
                    List<SurveyQuestionMap> surveys = PatientPortalEntities.SurveyQuestionMaps.Include("Survey").Include("Question").Include("Question.QuestionType").Include("Answer").Include("Product").Where(x => x.SurveyId == surveyID).ToList<SurveyQuestionMap>(); // && (x.ProductId != null || x.ChildQuestionId != null)

                    foreach (SurveyQuestionMap survey in surveys)
                    {
                        var surveyDTO = Mapper.Map<SurveyQuestionMap, SurveyQuestionMapDTO>(survey);
                        List<Answer> answers = PatientPortalEntities.Answers.Where(x => x.QuestionId == survey.QuestionId).ToList<Answer>();
                        surveyDTO.IQuestionDTO.IAnswerDTOList.Clear();
                        foreach (Answer answer in answers)
                        {
                            var answerMap = Mapper.Map<Answer, AnswerDTO>(answer);
                            surveyDTO.IQuestionDTO.IAnswerDTOList.Add(answerMap);
                        }
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

        /// <summary>
        /// save attempt survey submitted
        /// </summary>
        /// <param name="list">object of type List<SurveyQuestionAnswerDTO></param>
        /// <returns>object of type ISurveyQuestionAnswerDTO</returns>
        public ISurveyQuestionAnswerDTO SaveSurveyAttempt(List<ISurveyQuestionAnswerDTO> list)
        {
            SurveyQuestionAnswer surveyEntity = new SurveyQuestionAnswer();
            try
            {

                PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();
                foreach (ISurveyQuestionAnswerDTO surveyQuestionAnswerDTO in list)
                {
                    surveyQuestionAnswerDTO.CreatedDate = DateTime.Now;
                    surveyEntity = Mapper.Map<ISurveyQuestionAnswerDTO, SurveyQuestionAnswer>(surveyQuestionAnswerDTO);
                    PatientPortalEntities.AddToSurveyQuestionAnswers(surveyEntity);
                }
                PatientPortalEntities.SaveChanges();

            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while insert survey.", ex);
            }
            surveyEntity.Question = new Question();
            surveyEntity.Question.QuestionType = new QuestionType();
            return Mapper.Map<SurveyQuestionAnswer, SurveyQuestionAnswerDTO>(surveyEntity);
        }

        /// <summary>
        /// This method will use to get the past surveys list of a user.
        /// </summary>
        /// <returns></returns>
        public IList<ISurveyQuestionAnswerDTO> GetSurveyQuestionList(Guid Guid)
        {
            IList<ISurveyQuestionAnswerDTO> surveyList = new List<ISurveyQuestionAnswerDTO>();
            try
            {
                if (!string.IsNullOrEmpty(Guid.ToString()))
                {
                    PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();

                    List<SurveyQuestionAnswer> surveys = PatientPortalEntities.SurveyQuestionAnswers.Include("Survey").Include("Question").Include("Question.QuestionType").Include("Question.Answers").Where(x => x.Guid == Guid).OrderBy(x => x.Id).ToList<SurveyQuestionAnswer>();

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