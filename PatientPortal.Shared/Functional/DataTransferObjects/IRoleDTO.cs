//-----------------------------------------------------------------------
// <copyright file="ICustomerDTO.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary> This is the ICustomerDTO class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines a contract for department DTO.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface IRoleDTO : IDTO
    {
        /// <summary>
        /// Gets or sets the Role id.   
        /// </summary>
        /// <value>The Role id.</value>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the Type.
        /// </summary>
        /// <value>The Type.</value>
        string Type { get; set; }
    }
}