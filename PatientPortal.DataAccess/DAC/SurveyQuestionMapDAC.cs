//-----------------------------------------------------------------------
// <copyright file="SurveyQuestionMapDAC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the SurveyQuestionMapDAC.cs file.</summary>
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
    public class SurveyQuestionMapDAC : DACBase, ISurveyQuestionMapDAC
    {
        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="SurveyQuestionMapDAC"/> class.
        /// </summary>
        /// <param name="exceptionManager">Exception manager.</param>
        public SurveyQuestionMapDAC(IExceptionManager exceptionManager)
        {
            this.exceptionManager = exceptionManager;
        }

        /// <summary>
        /// Add a SurveyQuestionMap.
        /// </summary>
        /// <param name="survey">SurveyQuestionMap data.</param>
        /// <returns>Flag indicating if the operation succeed.</returns>
        public ISurveyQuestionMapDTO InsertSurveyQuestionMap(ISurveyQuestionMapDTO surveyQuestionMapDTO)
        {
            ISurveyQuestionMapDTO surveyQuestionMapDTOObj = new SurveyQuestionMapDTO();
            SurveyQuestionMap surveyQuestionMapEntity = new SurveyQuestionMap();
            PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();
            try
            {
               
                surveyQuestionMapEntity = Mapper.Map<ISurveyQuestionMapDTO, SurveyQuestionMap>(surveyQuestionMapDTO);
                PatientPortalEntities.AddToSurveyQuestionMaps(surveyQuestionMapEntity);
                PatientPortalEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while insert SurveyQuestionMap.", ex);
            }

            SurveyQuestionMap surveyQuestionMap = PatientPortalEntities.SurveyQuestionMaps.Include("Survey").Include("Question").Include("Answer").Include("Product").Where(c => c.Id == surveyQuestionMapEntity.Id).FirstOrDefault(); ;
            surveyQuestionMapDTOObj = Mapper.Map<SurveyQuestionMap, SurveyQuestionMapDTO>(surveyQuestionMap);

            return surveyQuestionMapDTOObj;
        }

        /// <summary>
        /// Delete a SurveyQuestionMap.
        /// </summary>
        /// <param name="id">SurveyQuestionMap id.</param>
        /// <returns>Flag indicating if the operation succeed.</returns>
        public bool DeleteSurveyQuestionMapById(int id)
        {
            try
            {

                PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();
                SurveyQuestionMap surveyQuestionMap = PatientPortalEntities.SurveyQuestionMaps.AsQueryable<SurveyQuestionMap>().Where(c => c.Id == id).FirstOrDefault();
                PatientPortalEntities.SurveyQuestionMaps.DeleteObject(surveyQuestionMap);
                PatientPortalEntities.SaveChanges();

            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while delete SurveyQuestionMaps.", ex);
            }

            return true;
        }


    }
}