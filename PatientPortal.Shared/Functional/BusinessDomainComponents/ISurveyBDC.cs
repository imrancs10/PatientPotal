//-----------------------------------------------------------------------
// <copyright file="ISurveyBDC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the ISurveyBDC class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines the contract for Survey Data BDC.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface ISurveyBDC : IBusinessDomainComponent
    {
        /// <summary>
        /// Gets the survey list.
        /// </summary>
        /// <returns>List of survey data component.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Done intentially.")]
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Done intentially.")]
        OperationResult<IList<ISurveyDTO>> GetSurveyList();

        /// <summary>
        /// Gets the survey list.
        /// </summary>
        /// <param name="id">Survey id.</param>
        /// <returns>Survey data component.</returns>
        OperationResult<ISurveyDTO> GetSurveyById(int id);

        /// <summary>
        /// Insert a survey record.
        /// </summary>
        /// <param name="surveyDTO">Survey data.</param>
        /// <returns>Operation result.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
        OperationResult<ISurveyDTO> InsertSurvey(ISurveyDTO surveyDTO);

        /// <summary>
        /// Update a survey record.
        /// </summary>
        /// <param name="surveyDTO">Survey data.</param>
        /// <returns>Operation result.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
        OperationResult<ISurveyDTO> UpdateSurvey(ISurveyDTO surveyDTO);

        /// <summary>
        /// Delete a survey record.
        /// </summary>
        /// <param name="id">Survey id.</param>
        /// <returns>Operation result.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
        OperationResult<bool> DeleteSurveyById(int id);

        OperationResult<IList<ISurveyQuestionMapDTO>> GetSurveyQuestionList();

    }
}