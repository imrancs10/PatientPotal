//-----------------------------------------------------------------------
// <copyright file="AdditionalInfoFacade.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the AdditionalInfoFacade.cs file.</summary>
//-----------------------------------------------------------------------
 

namespace PatientPortal.Facade
{
    using System;
    using System.Collections.Generic;
    using PatientPortal.Shared;

    /// <summary>
    /// Represents the facade for survey data management module.
    /// </summary>
    public class AdditionalInfoFacade : FacadeBase, IAdditionalInfoFacade
    {
        /// <summary>
        /// Instance of AdditionalInfo BDC.
        /// </summary>
        private readonly IAdditionalInfoBDC bdc;

        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdditionalInfoFacade"/> class.
        /// </summary>
        /// <param name="exceptionManager">The exception Manager.</param>
        /// <param name="bdc">The config Business Domain Component.</param>
        public AdditionalInfoFacade(IExceptionManager exceptionManager, IAdditionalInfoBDC bdc)
        {
            this.exceptionManager = exceptionManager;
            this.bdc = bdc;
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
                result = this.bdc.SaveAdditionalInformation(userDetail);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx);
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
                result = this.bdc.CheckUserEmail(emailId);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx);
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
                result = this.bdc.UpdateUserId(guid, userId);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx);
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
        /// <returns>object of type IUserDetailDTO</returns>
        public OperationResult<IUserDetailDTO> GetAdditionalInformation(int userId)
        {
            OperationResult<IUserDetailDTO> result;
            try
            {
                result = this.bdc.GetAdditionalInformation(userId);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx);
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