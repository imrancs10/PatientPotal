//-----------------------------------------------------------------------
// <copyright file="ILanguageFacade.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the ILanguageFacade class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines the contract for language Facade.
    /// </summary>
    public interface ILanguageFacade : IFacade
    {
        /// <summary>
        /// Gets the language list.
        /// </summary>
        /// <returns>List of survey.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Done intentially.")]
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Done intentially.")]
        OperationResult<IList<ILanguageDTO>> GetLanguageList();
    }
}