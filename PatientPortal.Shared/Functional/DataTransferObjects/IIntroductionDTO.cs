//-----------------------------------------------------------------------
// <copyright file="IIntroductionDTO.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary> This is the IIntroductionDTO class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    
    /// <summary>
    /// Defines a contract for introduction DTO.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface IIntroductionDTO : IDTO
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The user id.</value>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the language id.
        /// </summary>
        /// <value>The text.</value>
        int LanguageId { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The text.</value>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets the image path.
        /// </summary>
        /// <value>The image path of introduction</value>
        string ImagePath { get; set; }
              
        
    }
}