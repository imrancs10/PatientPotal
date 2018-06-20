//-----------------------------------------------------------------------
// <copyright file="IQuestionTypeDTO.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary> This is the IQuestionTypeDTO class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines a contract for question type DTO.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface ITerritoriesEmailDTO : IDTO
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the StateId.
        /// </summary>
        /// <value>The StateId.</value>
        int StateId { get; set; }

        /// <summary>
        /// Gets or sets the EmailId.
        /// </summary>
        /// <value>The EmailId.</value>
        string EmailId { get; set; }

    }
}