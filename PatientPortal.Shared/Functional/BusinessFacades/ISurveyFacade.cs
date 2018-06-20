//-----------------------------------------------------------------------
// <copyright file="ISurveyFacade.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the ISurveyFacade class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines the contract for survey Facade.
    /// </summary>
    public interface ISurveyFacade : IFacade
    {
        /// <summary>
        /// Gets the survey list.
        /// </summary>
        /// <returns>List of survey.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Done intentially.")]
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Done intentially.")]
        OperationResult<IList<ISurveyDTO>> GetSurveyList();

        /// <summary>
        /// Gets the survey by id.
        /// </summary>
        /// <param name="id">Survey id.</param>
        /// <returns>Survey data.</returns>
        OperationResult<ISurveyDTO> GetSurveyById(int id);

        /// <summary>
        /// Insert a survey.
        /// </summary>
        /// <param name="survey">Survey data.</param>
        OperationResult<ISurveyDTO> InsertSurvey(ISurveyDTO survey);

        /// <summary>
        /// Update a survey.
        /// </summary>
        /// <param name="survey">Update data.</param>
        OperationResult<ISurveyDTO> UpdateSurvey(ISurveyDTO survey);

        /// <summary>
        /// Delete a survey.
        /// </summary>
        /// <param name="id">Delete data.</param>
        /// <returns>True or False with message</returns>
        OperationResult<bool> DeleteSurveyById(int id);


    }
}