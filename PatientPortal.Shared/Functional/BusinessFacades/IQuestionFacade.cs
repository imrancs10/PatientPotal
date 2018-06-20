//-----------------------------------------------------------------------
// <copyright file="IQuestionFacade.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the IQuestionFacade class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines the contract for survey Facade.
    /// </summary>
    public interface IQuestionFacade : IFacade
    {
        /// <summary>
        /// Gets the question list.
        /// </summary>
        /// <returns>List of question.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Done intentially.")]
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Done intentially.")]
        OperationResult<IList<IQuestionDTO>> GetQuestionList();
        
        /// <summary>
        /// Gets the question by id.
        /// </summary>
        /// <param name="id">Question id.</param>
        /// <returns>Question data.</returns>
        OperationResult<IQuestionDTO> GetQuestionById(int id);

        /// <summary>
        /// Gets the question list.
        /// </summary>
        /// <returns>List of question.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Done intentially.")]
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Done intentially.")]
        OperationResult<IList<IQuestionDTO>> GetSurveyQuestionList(int languageId);

        /// <summary>
        /// Insert a question.
        /// </summary>
        /// <param name="question">Question data.</param>
        OperationResult<IQuestionDTO> InsertQuestion(IQuestionDTO question);

        /// <summary>
        /// Update a question.
        /// </summary>
        /// <param name="question">Update data.</param>
        OperationResult<IQuestionDTO> UpdateQuestion(IQuestionDTO question);

        /// <summary>
        /// Delete a question.
        /// </summary>
        /// <param name="id">Delete data.</param>
        /// <returns>True or False with message</returns>
        OperationResult<IList<IAnswerDTO>> DeleteQuestionById(int id);
    }
}