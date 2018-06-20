//-----------------------------------------------------------------------
// <copyright file="ICustomerBDC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the ICustomerBDC class.</summary>
//-----------------------------------------------------------------------
namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines the contract for Customer Data BDC.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface IUserBDC : IBusinessDomainComponent
    {
        /// <summary>
        /// Gets the user list.
        /// </summary>
        /// <returns>List of user data component.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Done intentially.")]
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Done intentially.")]
        OperationResult<IList<IUserDTO>> GetAll();

        /// <summary>
        /// Gets the user list.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>User data component.</returns>
        OperationResult<IUserDTO> GetById(int id);

        /// <summary>
        /// Insert a user record.
        /// </summary>
        /// <param name="}">User data.</param>
        /// <returns>Operation result.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
        OperationResult<IUserDTO> InsertCustomer(IUserDTO userDTO);

        /// <summary>
        /// Update a user record.
        /// </summary>
        /// <param name="userDTO">User data.</param>
        /// <returns>Operation result.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
        OperationResult<IUserDTO> UpdateUser(IUserDTO userDTO);

        /// <summary>
        /// Check authentication.
        /// </summary>
        /// <param name="userDTO">User data.</param>
        /// <returns>Operation result.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
        OperationResult<IUserDTO> CheckAuthenticUser(IUserDTO userDTO);

        /// <summary>
        /// To get authenticated user to send email for password change request.
        /// </summary>
        /// <param name="userDTO">User data.</param>
        /// <returns>Operation result.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
        OperationResult<IUserDTO> ForgetPassword(IUserDTO userDTO);

        OperationResult<IUserDTO> UpdateUserId(string guid, int userId);
    }
}