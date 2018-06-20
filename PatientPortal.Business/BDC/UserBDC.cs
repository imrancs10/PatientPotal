//-----------------------------------------------------------------------
// <copyright file="UserBDC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the UserBDC.cs file.</summary>
//-----------------------------------------------------------------------
namespace PatientPortal.Business
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using PatientPortal.Shared;

    /// <summary>
    /// Represents the business domain component for customer data.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public class UserBDC : BDCBase, IUserBDC
    {
        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Instance of Country DAC.
        /// </summary>
        private IUserDAC dac;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerBDC"/> class.
        /// </summary>
        /// <param name="exceptionManager">The exception manager.</param>
        /// <param name="customerDac">The customer data access Component.</param>
        public UserBDC(IExceptionManager exceptionManager, IUserDAC userDac)
        {
            this.exceptionManager = exceptionManager;
            this.dac = userDac;
        }

        /// <summary>
        /// Gets the user list.
        /// </summary>
        /// <returns>Result of the operation.</returns>
        [SuppressMessage("Microsoft.Design", "CA1031", Justification = "Generic exception caught after specifying specic exception", Scope = "Just for this method")]
        public OperationResult<IList<IUserDTO>> GetAll()
        {
            OperationResult<IList<IUserDTO>> result;
            try
            {
                IList<IUserDTO> userList = this.dac.GetAll();
                if (userList == null)
                {
                    result = OperationResult<IList<IUserDTO>>.CreateFailureResult("The object containing customer list is NULL !");
                }
                else
                {
                    result = OperationResult<IList<IUserDTO>>.CreateSuccessResult(userList, "Customer list fetched successfully!");
                }
            }
            catch (DACException dacEx)
            {
                this.exceptionManager.HandleException(dacEx, dacEx.Message);
                result = OperationResult<IList<IUserDTO>>.CreateErrorResult(dacEx.Message, dacEx.StackTrace);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx, bdcEx.Message);
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
        /// Gets the user.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>User data with operation result.</returns>
        [SuppressMessage("Microsoft.Design", "CA1031", Justification = "Generic exception caught after specifying specic exception", Scope = "Just for this method")]
        public OperationResult<IUserDTO> GetById(int id)
        {
            OperationResult<IUserDTO> result;
            try
            {
                IUserDTO user = this.dac.GetById(id);
                if (user == null)
                {
                    result = OperationResult<IUserDTO>.CreateFailureResult("The object containing customer list is NULL !");
                }
                else
                {
                    result = OperationResult<IUserDTO>.CreateSuccessResult(user, "Customer list fetched successfully!");
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
        /// Inserts a user.
        /// </summary>
        /// <param name="userDTO">User data.</param>
        /// <returns>User result.</returns>
        [SuppressMessage("Microsoft.Design", "CA1031", Justification = "Generic exception caught after specifying specic exception", Scope = "Just for this method")]
        public OperationResult<IUserDTO> InsertCustomer(IUserDTO userDTO)
        {
            OperationResult<IUserDTO> result;
            try
            {
                bool customerAddOperationResult = this.dac.Add(userDTO);
                if (customerAddOperationResult == false)
                {
                    result = OperationResult<IUserDTO>.CreateFailureResult("The customer could not be added !");
                }
                else
                {
                    result = OperationResult<IUserDTO>.CreateSuccessResult(userDTO, "Customer list fetched successfully!");
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
        /// Update a survey.
        /// </summary>
        /// <param name="surveyDTO">Update data.</param>
        /// <returns>Operation result.</returns>
        [SuppressMessage("Microsoft.Design", "CA1031", Justification = "Generic exception caught after specifying specic exception", Scope = "Just for this method")]
        public OperationResult<IUserDTO> UpdateUser(IUserDTO userDTO)
        {
            OperationResult<IUserDTO> result;
            try
            {
                userDTO.CreatedDate = DateTime.UtcNow;
                userDTO.ModifiedDate = DateTime.UtcNow;
                bool userResult = this.dac.UpdateUser(userDTO);
                if (userResult == false)
                {
                    result = OperationResult<IUserDTO>.CreateFailureResult("The user could not be updated !");
                }
                else
                {
                    result = OperationResult<IUserDTO>.CreateSuccessResult(userResult, "User updated successfully!");
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
        /// Authenticate user.
        /// </summary>
        /// <param name="userDTO">User data.</param>
        /// <returns>User result.</returns>
        [SuppressMessage("Microsoft.Design", "CA1031", Justification = "Generic exception caught after specifying specic exception", Scope = "Just for this method")]
        public OperationResult<IUserDTO> CheckAuthenticUser(IUserDTO userDTO)
        {

            OperationResult<IUserDTO> result;
            try
            {
                IUserDTO user = this.dac.CheckAuthenticUser(userDTO);
                if (user == null)
                {
                    result = OperationResult<IUserDTO>.CreateFailureResult("The object containing customer list is NULL !");
                }
                else
                {
                    result = OperationResult<IUserDTO>.CreateSuccessResult(user, "User fetched successfully!");
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
        /// Authenticate user.
        /// </summary>
        /// <param name="userDTO">User data.</param>
        /// <returns>User result.</returns>
        [SuppressMessage("Microsoft.Design", "CA1031", Justification = "Generic exception caught after specifying specic exception", Scope = "Just for this method")]
        public OperationResult<IUserDTO> ForgetPassword(IUserDTO userDTO)
        {

            OperationResult<IUserDTO> result;
            try
            {
                IUserDTO user = this.dac.ForgetPassword(userDTO);
                if (user == null)
                {
                    result = OperationResult<IUserDTO>.CreateFailureResult("The object containing customer list is NULL !");
                }
                else
                {
                    result = OperationResult<IUserDTO>.CreateSuccessResult(user, "User fetched successfully!");
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
    }
}