//-----------------------------------------------------------------------
// <copyright file="IQuestionDAC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the IQuestionDAC class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines a contract for Survey DAC.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface IQuestionDAC : IDataAccessComponent
    {
        /// <summary>
        /// Get list of question.
        /// </summary>
        /// <returns>List of question data component.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Done intentially.")]
        IList<IQuestionDTO> GetQuestionList();

        /// <summary>
        /// Get list of question.
        /// </summary>
        /// <returns>List of question data component.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Done intentially.")]
        IList<IQuestionDTO> GetSurveyQuestionList(int languageId);

        /// <summary>
        /// Get question data by question id.
        /// </summary>
        /// <param name="id">Question id.</param>
        /// <returns>Question data component.</returns>
        IQuestionDTO GetQuestionById(int id);

        /// <summary>
        /// Add a question.
        /// </summary>
        /// <param name="questionDTO">Question data component.</param>
        /// <returns>True or false.</returns>
        bool InsertQuestion(IQuestionDTO questionDTO);

        /// <summary>
        /// Update a question.
        /// </summary>
        /// <param name="questionDTO">Question data component.</param>
        /// <returns>True or false.</returns>
        IQuestionDTO UpdateQuestion(IQuestionDTO questionDTO);

        /// <summary>
        /// Delete a question.
        /// </summary>
        /// <param name="id">Question data component.</param>
        /// <returns>True or false.</returns>
        IList<IAnswerDTO> DeleteQuestionById(int id);

    }
}