//-----------------------------------------------------------------------
// <copyright file="ICustomerDTO.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary> This is the ICustomerDTO class.</summary>
//-----------------------------------------------------------------------

namespace Company.Project.Shared
{
    using System.Diagnostics.CodeAnalysis;
    
    /// <summary>
    /// Defines a contract for department DTO.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface ICustomerDTO : IDTO
    {
        /// <summary>
        /// Gets or sets the customer id.
        /// </summary>
        /// <value>The customer id.</value>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the customer name.
        /// </summary>
        /// <value>The customer name.</value>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the category id.
        /// </summary>
        /// <value>The category id.</value>
        int CategoryId { get; set; }
    }
}