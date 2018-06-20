//-----------------------------------------------------------------------
// <copyright file="IQuestionBDC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the IQuestionBDC class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines the contract for Survey Data BDC.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface IQuestionBDC : IBusinessDomainComponent
    {
        /// <summary>
        /// Gets the question list.
        /// </summary>
        /// <returns>List of question data component.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Done intentially.")]
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Done intentially.")]
        OperationResult<IList<IQuestionDTO>> GetQuestionList();

        /// <summary>
        /// Gets the question list.
        /// </summary>
        /// <returns>List of question data component.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Done intentially.")]
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Done intentially.")]
        OperationResult<IList<IQuestionDTO>> GetSurveyQuestionList(int languageId);

        /// <summary>
        /// Gets the question list.
        /// </summary>
        /// <param name="id">Question id.</param>
        /// <returns>Question data component.</returns>
        OperationResult<IQuestionDTO> GetQuestionById(int id);

        /// <summary>
        /// Insert a question record.
        /// </summary>
        /// <param name="questionDTO">Question data.</param>
        /// <returns>Operation result.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
        OperationResult<IQuestionDTO> InsertQuestion(IQuestionDTO questionDTO);

        /// <summary>
        /// Update a question record.
        /// </summary>
        /// <param name="questionDTO">Question data.</param>
        /// <returns>Operation result.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
        OperationResult<IQuestionDTO> UpdateQuestion(IQuestionDTO questionDTO);

        /// <summary>
        /// Delete a question record.
        /// </summary>
        /// <param name="id">Question id.</param>
        /// <returns>Operation result.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
        OperationResult<IList<IAnswerDTO>> DeleteQuestionById(int id);
    }
}