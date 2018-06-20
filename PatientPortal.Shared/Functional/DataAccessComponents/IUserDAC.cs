//-----------------------------------------------------------------------
// <copyright file="IUserDAC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the IUserDAC class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines a contract for Department DAC.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface IUserDAC : IDataAccessComponent
    {
        /// <summary>
        /// Get list of users.
        /// </summary>
        /// <returns>List of users data component.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Done intentially.")]
        IList<IUserDTO> GetAll();

        /// <summary>
        /// Get user data by user id.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>User data component.</returns>
        IUserDTO GetById(int id);

        /// <summary>
        /// Add a user.
        /// </summary>
        /// <param name="user">User data component.</param>
        /// <returns>True or false.</returns>
        bool Add(IUserDTO user);

        /// <summary>
        /// Update a user.
        /// </summary>
        /// <param name="user">User data component.</param>
        /// <returns>True or false.</returns>
        bool UpdateUser(IUserDTO user);

        /// <summary>
        /// Authenticate  user.
        /// </summary>
        /// <param name="userDTO">User object.</param>
        /// <returns>return user object</returns>
        IUserDTO CheckAuthenticUser(IUserDTO userDTO);

        /// <summary>
        /// To get authenticated user to send email for password change request.
        /// </summary>
        /// <param name="userDTO">User object.</param>
        /// <returns>return user object</returns>
        IUserDTO ForgetPassword(IUserDTO userDTO);

        /// <summary>
        /// Update user id based on GUID
        /// </summary>
        /// <param name="guid">User specific guid</param>
        /// <param name="userId">User Id</param>
        /// <returns>Return true if success</returns>
        bool UpdateUserId(string guid, int userId);
    }
}