//-----------------------------------------------------------------------
// <copyright file="ILanguageDTO.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary> This is the ILanguageDTO class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    
    /// <summary>
    /// Defines a contract for language DTO.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface ILanguageDTO : IDTO
    {
        /// <summary>
        /// Gets or sets the language id.
        /// </summary>
        /// <value>The language id.</value>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>The language.</value>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The culture code field.</value>
        string CultureCode { get; set; }
    }
}