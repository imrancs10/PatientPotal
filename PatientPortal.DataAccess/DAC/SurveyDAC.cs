//-----------------------------------------------------------------------
// <copyright file="SurveyDAC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the SurveyDAC.cs file.</summary>
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
    /// <summary>
    /// Represents Country Data Access Component.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public class SurveyDAC : DACBase, ISurveyDAC
    {
        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="SurveyDAC"/> class.
        /// </summary>
        /// <param name="exceptionManager">Exception manager.</param>
        public SurveyDAC(IExceptionManager exceptionManager)
        {
            this.exceptionManager = exceptionManager;
        }

        /// <summary>
        /// Gets the survey list.
        /// </summary>
        /// <returns>List of survey.</returns>
        public IList<ISurveyDTO> GetSurveyList()
        {
            IList<ISurveyDTO> surveyList = new List<ISurveyDTO>();

            try
            {
                PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();
                IQueryable<Survey> surveys = PatientPortalEntities.Surveys.Include("Language").AsQueryable<Survey>();
                foreach (Survey survey in surveys)
                {
                    var surveyObj = Mapper.Map<Survey, SurveyDTO>(survey);
                    surveyObj.IsMapped = CheckSurveyIsMapped(PatientPortalEntities, survey);
                    surveyList.Add(surveyObj);
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
        /// Check if this question is mapped with any survey
        /// </summary>
        /// <param name="PatientPortalEntities">PatientPortalEntity Object</param>
        /// <param name="questR>questionEntity Object</param>
        /// <returns>return trure or false</returns>
        private bool CheckSurveyIsMapped(PatientPortalEntities PatientPortalEntities, Survey survey)
        {
            bool isMapped = false;
            SurveyQuestionAnswer surveyQuestionAnswer = PatientPortalEntities.SurveyQuestionAnswers.Where(c => c.SurveyId == survey.Id).FirstOrDefault();
            if (surveyQuestionAnswer != null)
            {
                isMapped = true;
            }
            return isMapped;
        }

        /// <summary>
        /// Gets the survey by id.
        /// </summary>
        /// <param name="id">Survey id.</param>
        /// <returns>Survey information.</returns>
        public ISurveyDTO GetSurveyById(int id)
        {
            ISurveyDTO surveyDTO = new SurveyDTO();
            List<SurveyQuestionMap> surveyQuestionMapList = new List<SurveyQuestionMap>();
            try
            {
                PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();
                Survey survey = PatientPortalEntities.Surveys.Include("Language").AsQueryable<Survey>().Where(c => c.Id == id).FirstOrDefault();
                surveyDTO = Mapper.Map<Survey, SurveyDTO>(survey);
                surveyDTO.IsMapped = CheckSurveyIsMapped(PatientPortalEntities, survey);
                List<SurveyQuestionMap> surveyQuestionMapDTOList = PatientPortalEntities.SurveyQuestionMaps.Include("Survey").Include("Question").Include("Answer").Include("Product").Where(c => c.SurveyId == id).OrderBy(c => c.QuestionOrderNumber).ToList<SurveyQuestionMap>();

                if (surveyQuestionMapDTOList != null && surveyQuestionMapDTOList.Count() > 0)
                {
                    foreach (SurveyQuestionMap surveyQuestionMap in surveyQuestionMapDTOList)
                    {
                        var surveyQuestionMapEntity = Mapper.Map<SurveyQuestionMap, SurveyQuestionMapDTO>(surveyQuestionMap);
                        surveyDTO.ISurveyQuestionMapList.Add(surveyQuestionMapEntity);
                    }
                }
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while fetching survey list.", ex);
            }

            IList<SurveyQuestionMapDTO> a = ((SurveyDTO)(surveyDTO)).SurveyQuestionMapList;
            if (surveyDTO != null && surveyDTO.ISurveyQuestionMapList.Count > 0)
            {
                foreach (SurveyQuestionMapDTO obj in surveyDTO.ISurveyQuestionMapList)
                {
                    ((SurveyDTO)(surveyDTO)).SurveyQuestionMapList.Add(obj);
                }

            }

            return surveyDTO;
        }

        /// <summary>
        /// Add a survey.
        /// </summary>
        /// <param name="survey">Survey data.</param>
        /// <returns>Flag indicating if the operation succeed.</returns>
        public bool InsertSurvey(ISurveyDTO survey)
        {
            ISurveyQuestionMapDTO surveyQuestionMapDTOObj = new SurveyQuestionMapDTO();
            int? surveyQuestionMapId = null;
            IList<ISurveyQuestionMapDTO> containSurveyQuestionMapObj = new List<ISurveyQuestionMapDTO>();
            Survey surveyEntity = new Survey();
            survey.CreatedDate = DateTime.UtcNow;
            survey.ModifiedDate = DateTime.UtcNow;
            try
            {
                PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();
                surveyEntity = Mapper.Map<ISurveyDTO, Survey>(survey);
                PatientPortalEntities.AddToSurveys(surveyEntity);
                PatientPortalEntities.SaveChanges();

                if (survey.ISurveyQuestionMapList.Count() > 0)
                {
                    // Save Answer of relevant question
                    int? nodeNumber = 0;
                    SurveyQuestionMapDAC surveyQuestionMapDAC = new SurveyQuestionMapDAC(this.exceptionManager);
                    foreach (ISurveyQuestionMapDTO surveyQuestionMapDTO in survey.ISurveyQuestionMapList)
                    {
                        surveyQuestionMapDTO.SurveyId = surveyEntity.Id;
                        surveyQuestionMapId = surveyQuestionMapId == 0 ? null : (surveyQuestionMapDTO.IsMainNode != nodeNumber ? null : surveyQuestionMapId);
                        if (surveyQuestionMapDTO.SameNodeNumber > 1)
                        {
                            if (containSurveyQuestionMapObj.Where(c => c.QuestionId == surveyQuestionMapDTO.QuestionId && c.IsMainNode == nodeNumber && c.SameQuestionNumber == surveyQuestionMapDTO.SameQuestionNumber).ToList().Count <= 0)
                            {
                                survey.ISurveyQuestionMapList.Where(c => c.QuestionId == surveyQuestionMapDTO.QuestionId && c.IsMainNode == surveyQuestionMapDTO.IsMainNode && c.SameQuestionNumber == surveyQuestionMapDTO.SameQuestionNumber).ToList().ForEach(x => { x.SurveyQuestionMapId = surveyQuestionMapId; });
                            }
                        }
                        containSurveyQuestionMapObj.Add(surveyQuestionMapDTO);
                        surveyQuestionMapDTOObj = surveyQuestionMapDAC.InsertSurveyQuestionMap(surveyQuestionMapDTO);
                        surveyQuestionMapId = surveyQuestionMapDTOObj.Id;
                        nodeNumber = surveyQuestionMapDTO.IsMainNode;
                    }
                }
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while insert survey.", ex);
            }

            return true;
        }

        /// <summary>
        /// Update a survey.
        /// </summary>
        /// <param name="survey">Survey data.</param>
        /// <returns>Flag indicating if the operation succeed.</returns>
        public bool UpdateSurvey(ISurveyDTO survey)
        {
            try
            {
                survey.ModifiedDate = DateTime.UtcNow;
                PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();
                ISurveyQuestionMapDTO surveyQuestionMapDTOObj = new SurveyQuestionMapDTO();
                int? surveyQuestionMapId = null;
                IList<ISurveyQuestionMapDTO> containSurveyQuestionMapObj = new List<ISurveyQuestionMapDTO>();
                SurveyQuestionMapDAC surveyQuestionMapDAC = new SurveyQuestionMapDAC(this.exceptionManager);
                Survey surveyObj = PatientPortalEntities.Surveys.Include("SurveyQuestionMaps").AsQueryable<Survey>().Where(c => c.Id == survey.Id).FirstOrDefault();
                //if (!survey.IsMapped)
                //{
                if (surveyObj.SurveyQuestionMaps.Count() > 0)
                {
                    foreach (SurveyQuestionMap surveyQuestionMap in surveyObj.SurveyQuestionMaps.ToList())
                    {
                        PatientPortalEntities.SurveyQuestionMaps.DeleteObject(surveyQuestionMap);
                    }
                }

                // Save Answer of relevant question
                int? nodeNumber = 0;
                foreach (ISurveyQuestionMapDTO surveyQuestionMapDTO in survey.ISurveyQuestionMapList)
                {
                    surveyQuestionMapDTO.SurveyId = surveyObj.Id;
                    surveyQuestionMapId = surveyQuestionMapId == 0 ? null : (surveyQuestionMapDTO.IsMainNode != nodeNumber ? null : surveyQuestionMapId);
                    if (surveyQuestionMapDTO.SameNodeNumber > 1)
                    {
                        //if (surveyQuestionMapDTO.SurveyQuestionMapId == null && surveyQuestionMapDTO.IsMainNode == nodeNumber && surveyQuestionMapDTOObj.QuestionId != surveyQuestionMapDTO.QuestionId)
                        //{
                        if (containSurveyQuestionMapObj.Where(c => c.QuestionId == surveyQuestionMapDTO.QuestionId && c.IsMainNode == nodeNumber && c.SameQuestionNumber == surveyQuestionMapDTO.SameQuestionNumber).ToList().Count <= 0)
                        {
                            survey.ISurveyQuestionMapList.Where(c => c.QuestionId == surveyQuestionMapDTO.QuestionId && c.IsMainNode == surveyQuestionMapDTO.IsMainNode && c.SameQuestionNumber == surveyQuestionMapDTO.SameQuestionNumber).ToList().ForEach(x => { x.SurveyQuestionMapId = surveyQuestionMapId; });
                        }
                        //}
                    }
                    containSurveyQuestionMapObj.Add(surveyQuestionMapDTO);


                    surveyQuestionMapDTOObj = surveyQuestionMapDAC.InsertSurveyQuestionMap(surveyQuestionMapDTO);
                    surveyQuestionMapId = surveyQuestionMapDTOObj.Id;
                    nodeNumber = surveyQuestionMapDTO.IsMainNode;
                }
                //}

                Mapper.Map<ISurveyDTO, Survey>(survey, surveyObj);
                PatientPortalEntities.SaveChanges();

            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while update survey.", ex);
            }

            return true;
        }

        /// <summary>
        /// Delete a survey.
        /// </summary>
        /// <param name="id">Survey id.</param>
        /// <returns>Flag indicating if the operation succeed.</returns>
        public bool DeleteSurveyById(int id)
        {
            try
            {
                PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();
                Survey survey = PatientPortalEntities.Surveys.Include("SurveyQuestionMaps").AsQueryable<Survey>().Where(c => c.Id == id).FirstOrDefault();
                if (survey.SurveyQuestionMaps.Count() > 0)
                {
                    // Delete the survey referenced record
                    foreach (SurveyQuestionMap surveyQuestionMap in survey.SurveyQuestionMaps.ToList())
                    {
                        PatientPortalEntities.SurveyQuestionMaps.DeleteObject(surveyQuestionMap);
                    }
                }
                PatientPortalEntities.Surveys.DeleteObject(survey);
                PatientPortalEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while insert survey.", ex);
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<ISurveyQuestionMapDTO> GetSurveyQuestionList()
        {
            IList<ISurveyQuestionMapDTO> surveyList = new List<ISurveyQuestionMapDTO>();
            try
            {
                PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();
                IQueryable<SurveyQuestionMap> surveys = PatientPortalEntities.SurveyQuestionMaps.AsQueryable<SurveyQuestionMap>();
                foreach (SurveyQuestionMap survey in surveys)
                {
                    var surveyDTO = Mapper.Map<SurveyQuestionMap, SurveyQuestionMapDTO>(survey);
                    surveyList.Add(surveyDTO);
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