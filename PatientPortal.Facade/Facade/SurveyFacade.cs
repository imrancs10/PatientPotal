//-----------------------------------------------------------------------
// <copyright file="SurveyFacade.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the SurveyFacade.cs file.</summary>
//-----------------------------------------------------------------------
 

namespace PatientPortal.Facade
{
    using System;
    using System.Collections.Generic;
    using PatientPortal.Shared;

    /// <summary>
    /// Represents the facade for survey data management module.
    /// </summary>
    public class SurveyFacade : FacadeBase, ISurveyFacade
    {
        /// <summary>
        /// Instance of survey BDC.
        /// </summary>
        private readonly ISurveyBDC bdc;

        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="SurveyFacade"/> class.
        /// </summary>
        /// <param name="exceptionManager">The exception Manager.</param>
        /// <param name="bdc">The config Business Domain Component.</param>
        public SurveyFacade(IExceptionManager exceptionManager, ISurveyBDC bdc)
        {
            this.exceptionManager = exceptionManager;
            this.bdc = bdc;
        }

        /// <summary>
        /// Gets the survey list.
        /// </summary>
        /// <returns>Result of business operation.</returns>
        public OperationResult<IList<ISurveyDTO>> GetSurveyList()
        {
            OperationResult<IList<ISurveyDTO>> result;
            try
            {
                result = this.bdc.GetSurveyList();
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx);
                result = OperationResult<IList<ISurveyDTO>>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<IList<ISurveyDTO>>.CreateErrorResult(ex.Message, ex.StackTrace);
            }

            return result;
        }

        /// <summary>
        /// Gets the survey by id.
        /// </summary>
        /// <param name="id">The survey id.</param>
        /// <returns>Operation result.</returns>
        public OperationResult<ISurveyDTO> GetSurveyById(int id)
        {
            OperationResult<ISurveyDTO> result;
            try
            {
                result = this.bdc.GetSurveyById(id);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx);
                result = OperationResult<ISurveyDTO>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<ISurveyDTO>.CreateErrorResult(ex.Message, ex.StackTrace);
            }

            return result;
        }

        /// <summary>
        /// Insert a survey.
        /// </summary>
        /// <param name="survey">Customer data.</param>
        /// <returns>Operation result.</returns>
        public OperationResult<ISurveyDTO> InsertSurvey(ISurveyDTO survey)
        {
            OperationResult<ISurveyDTO> result;
            try
            {
                result = this.bdc.InsertSurvey(survey);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx);
                result = OperationResult<ISurveyDTO>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<ISurveyDTO>.CreateErrorResult(ex.Message, ex.StackTrace);
            }

            return result;
        }

        /// <summary>
        /// Update a survey.
        /// </summary>
        /// <param name="survey">Survey data.</param>
        /// <returns>Operation result.</returns>
        public OperationResult<ISurveyDTO> UpdateSurvey(ISurveyDTO survey)
        {
            OperationResult<ISurveyDTO> result;
            try
            {
                result = this.bdc.UpdateSurvey(survey);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx);
                result = OperationResult<ISurveyDTO>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<ISurveyDTO>.CreateErrorResult(ex.Message, ex.StackTrace);
            }

            return result;
        }

        /// <summary>
        /// Delete a survey.
        /// </summary>
        /// <param name="id">Survey id.</param>
        /// <returns>Operation result.</returns>
        public OperationResult<bool> DeleteSurveyById(int id)
        {
            OperationResult<bool> result;
            try
            {
                result = this.bdc.DeleteSurveyById(id);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx);
                result = OperationResult<bool>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<bool>.CreateErrorResult(ex.Message, ex.StackTrace);
            }

            return result;
        }
    }
}