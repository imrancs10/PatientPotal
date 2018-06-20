//-----------------------------------------------------------------------
// <copyright file="ICustomerFacade.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the ICustomerFacade class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines the contract for customer Facade.
    /// </summary>
    public interface IUserFacade : IFacade
    {
        /// <summary>
        /// Gets the user list.
        /// </summary>
        /// <returns>List of users.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Done intentially.")]
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Done intentially.")]
        OperationResult<IList<IUserDTO>> GetUserList();

        /// <summary>
        /// Gets the user by id.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>User data.</returns>
        OperationResult<IUserDTO> GetUserId(int id);

        /// <summary>
        /// Insert a user.
        /// </summary>
        /// <param name="user">User data.</param>
        /// <returns>Operation result.</returns>
        OperationResult<IUserDTO> InsertUser(IUserDTO user);

        /// <summary>
        /// Update a user.
        /// </summary>
        /// <param name="user">User data.</param>
        /// <returns>Operation result.</returns>
        OperationResult<IUserDTO> UpdateUser(IUserDTO user);

        /// <summary>
        /// Check Authentic user
        /// </summary>
        /// <param name="userDTO">user object</param>
        /// <returns>return user object</returns>
        OperationResult<IUserDTO> CheckAuthenticUser(IUserDTO userDTO);

        /// <summary>
        /// To get authenticated user to send email for password change request.
        /// </summary>
        /// <param name="userDTO">user object</param>
        /// <returns>return user object</returns>
        OperationResult<IUserDTO> ForgetPassword(IUserDTO userDTO);

        OperationResult<IUserDTO> UpdateUserId(string guid, int userId);
    }
}