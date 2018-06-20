//-----------------------------------------------------------------------
// <copyright file="IQuestionTypeFacade.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the IQuestionTypeFacade class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines the contract for question type Facade.
    /// </summary>
    public interface IQuestionTypeFacade : IFacade
    {
        /// <summary>
        /// Gets the survey list.
        /// </summary>
        /// <returns>List of survey.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Done intentially.")]
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Done intentially.")]
        OperationResult<IList<IQuestionTypeDTO>> GetQuestionTypeList();
    }
}