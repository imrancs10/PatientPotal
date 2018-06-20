//-----------------------------------------------------------------------
// <copyright file="ICustomerDTO.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary> This is the ICustomerDTO class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines a contract for department DTO.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface IUserDTO : IDTO
    {
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        /// <value>The user name.</value>
        string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password id.</value>
        string Password { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type field.</value>
        int RoleId { get; set; }


        /// <summary>
        /// Gets or sets the active.
        /// </summary>
        /// <value>The active field.</value>
        bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the delete.
        /// </summary>
        /// <value>The delete field.</value>
        bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>The createde date.</value>
        DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>The molified date.</value>
        DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the IRoleDTO.
        /// </summary>
        /// <value>The IRoleDTO.</value>
        IRoleDTO RoleDTO { get; set; }

    }
}