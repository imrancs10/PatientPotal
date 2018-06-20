//-----------------------------------------------------------------------
// <copyright file="AdditionalInfoBDC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the AdditionalInfoBDC file.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Business
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using PatientPortal.Shared;

    /// <summary>
    /// Represents the business domain component for AdditionalInfo.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public class AdditionalInfoBDC : BDCBase, IAdditionalInfoBDC
    {
        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Instance of IAdditionalInfo DAC.
        /// </summary>
        private IAdditionalInfoDAC dac;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdditionalInfoBDC"/> class.
        /// </summary>
        /// <param name="exceptionManager">The exception manager.</param>
        /// <param name="userDac">The user data access Component.</param>
        public AdditionalInfoBDC(IExceptionManager exceptionManager, IAdditionalInfoDAC Dac)
        {
            this.exceptionManager = exceptionManager;
            this.dac = Dac;
        }

        /// <summary>
        /// save additional information
        /// </summary>
        /// <param name="userDetail">object of type UserDetailDTO</param>
        /// <returns>object of type IUserDTO</returns>
        public OperationResult<IUserDTO> SaveAdditionalInformation(IUserDetailDTO userDetail)
        {
            OperationResult<IUserDTO> result;
            try
            {
                IUserDTO surveyResult = this.dac.SaveAdditionalInformation(userDetail);
                if (surveyResult == null)
                {
                    result = OperationResult<IUserDTO>.CreateFailureResult("The Additional Information could not be added !");
                }
                else
                {
                    result = OperationResult<IUserDTO>.CreateSuccessResult(surveyResult, "Additional Information successfully!");
                }
            }
            catch (DACException dacEx)
            {
                this.exceptionManager.HandleException(dacEx, dacEx.Message);
                result = OperationResult<IUserDTO>.CreateErrorResult(dacEx.Message, dacEx.StackTrace);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx, bdcEx.Message);
                result = OperationResult<IUserDTO>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<IUserDTO>.CreateErrorResult(ex.Message, ex.StackTrace);
            }

            return result;
        }

        /// <summary>
        /// check user email id 
        /// </summary>
        /// <param name="emailId">object of type IUserDTO</param>
        /// <returns></returns>
        public OperationResult<IUserDTO> CheckUserEmail(string emailId)
        {
            OperationResult<IUserDTO> result;
            try
            {
                IUserDTO userDTO = this.dac.CheckUserEmail(emailId);

                if (userDTO == null)
                {
                    result = OperationResult<IUserDTO>.CreateFailureResult("The object containing User is NULL !");
                }
                else
                {
                    result = OperationResult<IUserDTO>.CreateSuccessResult(userDTO, "User fetched successfully!");
                }
            }
            catch (DACException dacEx)
            {
                this.exceptionManager.HandleException(dacEx, dacEx.Message);
                result = OperationResult<IUserDTO>.CreateErrorResult(dacEx.Message, dacEx.StackTrace);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx, bdcEx.Message);
                result = OperationResult<IUserDTO>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<IUserDTO>.CreateErrorResult(ex.Message, ex.StackTrace);
            }

            return result;
        }

        /// <summary>
        /// Update User Id when login
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public OperationResult<IUserDTO> UpdateUserId(string guid, int userId)
        {
            OperationResult<IUserDTO> result;
            try
            {

                bool questionResult = this.dac.UpdateUserId(guid, userId);
                if (questionResult == false)
                {
                    result = OperationResult<IUserDTO>.CreateFailureResult("The Additional Information could not be added !");
                }
                else
                {
                    result = OperationResult<IUserDTO>.CreateSuccessResult(true, "Additional Information successfully!");
                }
            }
            catch (DACException dacEx)
            {
                this.exceptionManager.HandleException(dacEx, dacEx.Message);
                result = OperationResult<IUserDTO>.CreateErrorResult(dacEx.Message, dacEx.StackTrace);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx, bdcEx.Message);
                result = OperationResult<IUserDTO>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<IUserDTO>.CreateErrorResult(ex.Message, ex.StackTrace);
            }

            return result;
        }

        /// <summary>
        /// Get Additional information for a user
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>object of type IUserDetailDTO</returns>
        public OperationResult<IUserDetailDTO> GetAdditionalInformation(int userId)
        {
            OperationResult<IUserDetailDTO> result;
            try
            {
                IUserDetailDTO stateList = this.dac.GetAdditionalInformation(userId);

                if (stateList == null)
                {
                    result = OperationResult<IUserDetailDTO>.CreateFailureResult("The object containing state list is NULL !");
                }
                else
                {
                    result = OperationResult<IUserDetailDTO>.CreateSuccessResult(stateList, "State fetched successfully!");
                }
            }
            catch (DACException dacEx)
            {
                this.exceptionManager.HandleException(dacEx, dacEx.Message);
                result = OperationResult<IUserDetailDTO>.CreateErrorResult(dacEx.Message, dacEx.StackTrace);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx, bdcEx.Message);
                result = OperationResult<IUserDetailDTO>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<IUserDetailDTO>.CreateErrorResult(ex.Message, ex.StackTrace);
            }
            return result;
        }
    }
}