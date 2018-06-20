//-----------------------------------------------------------------------
// <copyright file="UserFacade.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the UserFacade.cs file.</summary>
//-----------------------------------------------------------------------


namespace PatientPortal.Facade
{
    using System;
    using System.Collections.Generic;
    using PatientPortal.Shared;

    /// <summary>
    /// Represents the facade for customer data management module.
    /// </summary>
    public class UserFacade : FacadeBase, IUserFacade
    {
        /// <summary>
        /// Instance of customer BDC.
        /// </summary>
        private readonly IUserBDC bdc;

        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserFacade"/> class.
        /// </summary>
        /// <param name="exceptionManager">The exception Manager.</param>
        /// <param name="bdc">The config Business Domain Component.</param>
        public UserFacade(IExceptionManager exceptionManager, IUserBDC bdc)
        {
            this.exceptionManager = exceptionManager;
            this.bdc = bdc;
        }

        /// <summary>
        /// Gets the user list.
        /// </summary>
        /// <returns>Result of business operation.</returns>
        public OperationResult<IList<IUserDTO>> GetUserList()
        {
            OperationResult<IList<IUserDTO>> result;
            try
            {
                result = this.bdc.GetAll();
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx);
                result = OperationResult<IList<IUserDTO>>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<IList<IUserDTO>>.CreateErrorResult(ex.Message, ex.StackTrace);
            }
            return result;
        }

        /// <summary>
        /// Gets the user by id.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <returns>Operation result.</returns>
        public OperationResult<IUserDTO> GetUserId(int id)
        {
            OperationResult<IUserDTO> result;
            try
            {
                result = this.bdc.GetById(id);
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
        /// Insert a user.
        /// </summary>
        /// <param name="user">User data.</param>
        /// <returns>Operation result.</returns>
        public OperationResult<IUserDTO> InsertUser(IUserDTO user)
        {
            OperationResult<IUserDTO> result;
            try
            {
                result = this.bdc.InsertCustomer(user);
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
        /// Update a user.
        /// </summary>
        /// <param name="user">Update data.</param>
        /// <returns>Operation result.</returns>
        public OperationResult<IUserDTO> UpdateUser(IUserDTO user)
        {
            OperationResult<IUserDTO> result = null;
            try
            {
                result = this.bdc.UpdateUser(user);
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
        /// Authenticate user
        /// </summary>
        /// <param name="user">User data.</param>
        /// <returns>Operation result.</returns>
        public OperationResult<IUserDTO> CheckAuthenticUser(IUserDTO user)
        {
            OperationResult<IUserDTO> result;
            try
            {
                result = this.bdc.CheckAuthenticUser(user);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx);
                result = OperationResult<IUserDTO>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception  ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<IUserDTO>.CreateErrorResult(ex.Message, ex.StackTrace);
            }
            return result;
        }

        /// <summary>
        /// To get authenticated user to send email for password change request.
        /// </summary>
        /// <param name="user">User data.</param>
        /// <returns>Operation result.</returns>
        public OperationResult<IUserDTO> ForgetPassword(IUserDTO user)
        {
            OperationResult<IUserDTO> result;
            try
            {
                result = this.bdc.ForgetPassword(user);
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

    }
}