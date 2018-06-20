//-----------------------------------------------------------------------
// <copyright file="SurveyBDC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the SurveyBDC.cs file.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Business
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using PatientPortal.Shared;

    /// <summary>
    /// Represents the business domain component for survey data.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public class SurveyBDC : BDCBase, ISurveyBDC
    {
        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Instance of Survey DAC.
        /// </summary>
        private ISurveyDAC dac;

        /// <summary>
        /// Initializes a new instance of the <see cref="SurveyBDC"/> class.
        /// </summary>
        /// <param name="exceptionManager">The exception manager.</param>
        /// <param name="userDac">The user data access Component.</param>
        public SurveyBDC(IExceptionManager exceptionManager, ISurveyDAC userDac)
        {
            this.exceptionManager = exceptionManager;
            this.dac = userDac;
        }

        /// <summary>
        /// Gets the survey list.
        /// </summary>
        /// <returns>Result of the operation.</returns>
        [SuppressMessage("Microsoft.Design", "CA1031", Justification = "Generic exception caught after specifying specic exception", Scope = "Just for this method")]
        public OperationResult<IList<ISurveyDTO>> GetSurveyList()
        {
            OperationResult<IList<ISurveyDTO>> result;
            try
            {
                IList<ISurveyDTO> surveyList = this.dac.GetSurveyList();

                if (surveyList == null)
                {
                    result = OperationResult<IList<ISurveyDTO>>.CreateFailureResult("The object containing survey list is NULL !");
                }
                else
                {
                    result = OperationResult<IList<ISurveyDTO>>.CreateSuccessResult(surveyList, "Survey list fetched successfully!");
                }
            }
            catch (DACException dacEx)
            {
                this.exceptionManager.HandleException(dacEx, dacEx.Message);
                result = OperationResult<IList<ISurveyDTO>>.CreateErrorResult(dacEx.Message, dacEx.StackTrace);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx, bdcEx.Message);
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
        /// Gets the survey.
        /// </summary>
        /// <param name="id">Survey id.</param>
        /// <returns>Survey data with operation result.</returns>
        [SuppressMessage("Microsoft.Design", "CA1031", Justification = "Generic exception caught after specifying specic exception", Scope = "Just for this method")]
        public OperationResult<ISurveyDTO> GetSurveyById(int id)
        {
            OperationResult<ISurveyDTO> result;
            try
            {
                ISurveyDTO surveyDTO = this.dac.GetSurveyById(id);
                if (surveyDTO == null)
                {
                    result = OperationResult<ISurveyDTO>.CreateFailureResult("The object containing survey list is NULL !");
                }
                else
                {
                    result = OperationResult<ISurveyDTO>.CreateSuccessResult(surveyDTO, "Survey list fetched successfully!");
                }
            }
            catch (DACException dacEx)
            {
                this.exceptionManager.HandleException(dacEx, dacEx.Message);
                result = OperationResult<ISurveyDTO>.CreateErrorResult(dacEx.Message, dacEx.StackTrace);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx, bdcEx.Message);
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
        /// Inserts a survey.
        /// </summary>
        /// <param name="surveyDTO">Survey data.</param>
        /// <returns>Operation result.</returns>
        [SuppressMessage("Microsoft.Design", "CA1031", Justification = "Generic exception caught after specifying specic exception", Scope = "Just for this method")]
        public OperationResult<ISurveyDTO> InsertSurvey(ISurveyDTO surveyDTO)
        {
            OperationResult<ISurveyDTO> result;
            try
            {
                
                bool surveyResult = this.dac.InsertSurvey(surveyDTO);
                if (surveyResult == false)
                {
                    result = OperationResult<ISurveyDTO>.CreateFailureResult("The survey could not be added !");
                }
                else
                {
                    result = OperationResult<ISurveyDTO>.CreateSuccessResult(surveyDTO, "Survey added successfully!");
                }
            }
            catch (DACException dacEx)
            {
                this.exceptionManager.HandleException(dacEx, dacEx.Message);
                result = OperationResult<ISurveyDTO>.CreateErrorResult(dacEx.Message, dacEx.StackTrace);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx, bdcEx.Message);
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
        /// <param name="surveyDTO">Update data.</param>
        /// <returns>Operation result.</returns>
        [SuppressMessage("Microsoft.Design", "CA1031", Justification = "Generic exception caught after specifying specic exception", Scope = "Just for this method")]
        public OperationResult<ISurveyDTO> UpdateSurvey(ISurveyDTO surveyDTO)
        {
            OperationResult<ISurveyDTO> result;
            try
            {
                surveyDTO.CreatedDate = DateTime.UtcNow;
                surveyDTO.ModifiedDate = DateTime.UtcNow;
                bool surveyResult = this.dac.UpdateSurvey(surveyDTO);
                if (surveyResult == false)
                {
                    result = OperationResult<ISurveyDTO>.CreateFailureResult("The survey could not be updated !");
                }
                else
                {
                    result = OperationResult<ISurveyDTO>.CreateSuccessResult(surveyResult, "Survey updated successfully!");
                }
            }
            catch (DACException dacEx)
            {
                this.exceptionManager.HandleException(dacEx, dacEx.Message);
                result = OperationResult<ISurveyDTO>.CreateErrorResult(dacEx.Message, dacEx.StackTrace);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx, bdcEx.Message);
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
        /// <param name="id">Delete data.</param>
        /// <returns>Operation result.</returns>
        [SuppressMessage("Microsoft.Design", "CA1031", Justification = "Generic exception caught after specifying specic exception", Scope = "Just for this method")]
        public OperationResult<bool> DeleteSurveyById(int id)
        {
            OperationResult<bool> result;
            try
            {
                bool surveyResult = this.dac.DeleteSurveyById(id);
                if (surveyResult == false)
                {
                    result = OperationResult<bool>.CreateFailureResult("The survey could not be updated !");
                }
                else
                {
                    result = OperationResult<bool>.CreateSuccessResult(true, "Survey deleted successfully!");
                }
            }
            catch (DACException dacEx)
            {
                this.exceptionManager.HandleException(dacEx, dacEx.Message);
                result = OperationResult<bool>.CreateErrorResult(dacEx.Message, dacEx.StackTrace);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx, bdcEx.Message);
                result = OperationResult<bool>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<bool>.CreateErrorResult(ex.Message, ex.StackTrace);
            }

            return result;
        }


        public OperationResult<IList<ISurveyQuestionMapDTO>> GetSurveyQuestionList()
        {
            OperationResult<IList<ISurveyQuestionMapDTO>> result;
            try
            {
                IList<ISurveyQuestionMapDTO> surveyList = this.dac.GetSurveyQuestionList();

                if (surveyList == null)
                {
                    result = OperationResult<IList<ISurveyQuestionMapDTO>>.CreateFailureResult("The object containing survey question list is NULL !");
                }
                else
                {
                    result = OperationResult<IList<ISurveyQuestionMapDTO>>.CreateSuccessResult(surveyList, "Survey question list fetched successfully!");
                }
            }
            catch (DACException dacEx)
            {
                this.exceptionManager.HandleException(dacEx, dacEx.Message);
                result = OperationResult<IList<ISurveyQuestionMapDTO>>.CreateErrorResult(dacEx.Message, dacEx.StackTrace);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx, bdcEx.Message);
                result = OperationResult<IList<ISurveyQuestionMapDTO>>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<IList<ISurveyQuestionMapDTO>>.CreateErrorResult(ex.Message, ex.StackTrace);
            }

            return result;
        }

    }
}