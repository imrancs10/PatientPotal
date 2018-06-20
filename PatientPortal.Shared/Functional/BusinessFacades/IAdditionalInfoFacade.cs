//-----------------------------------------------------------------------
// <copyright file="IAdditionalInfoFacade.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the IAdditionalInfoFacade class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines the contract for AdditionalInfoFacade.
    /// </summary>
    public interface IAdditionalInfoFacade : IFacade
    {
        /// <summary>
        /// save additional information
        /// </summary>
        /// <param name="userDetail">object of type UserDetailDTO</param>
        /// <returns>object of type IUserDTO</returns>
        OperationResult<IUserDTO> SaveAdditionalInformation(IUserDetailDTO userDetail);

        /// <summary>
        /// check user email id 
        /// </summary>
        /// <param name="emailId">object of type IUserDTO</param>
        /// <returns></returns>
        OperationResult<IUserDTO> CheckUserEmail(string emailId);

        /// <summary>
        /// Get Additional information for a user
        /// </summary>
        /// <returns>object of type IUserDetailDTO</returns>
        OperationResult<IUserDetailDTO> GetAdditionalInformation(int userId);

        /// <summary>
        /// Update User Id when login
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        OperationResult<IUserDTO> UpdateUserId(string guid, int userId);

    }
}