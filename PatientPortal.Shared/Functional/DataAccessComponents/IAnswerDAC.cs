//-----------------------------------------------------------------------
// <copyright file="IAnswerDAC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the IAnswerDAC class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines a contract for Survey DAC.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface IAnswerDAC : IDataAccessComponent
    {

        /// <summary>
        /// Add a answer.
        /// </summary>
        /// <param name="answerDTO">Answer data component.</param>
        /// <returns>True or false.</returns>
        bool InsertAnswer(IAnswerDTO answerDTO);

        /// <summary>
        /// Delete a answer.
        /// </summary>
        /// <param name="id">Answer data component.</param>
        /// <returns>Answer DTO object</returns>
        IAnswerDTO DeleteAnswerById(int id);
    }
}