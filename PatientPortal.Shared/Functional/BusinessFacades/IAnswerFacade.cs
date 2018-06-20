//-----------------------------------------------------------------------
// <copyright file="IAnswerFacade.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the IAnswerFacade class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines the contract for survey Facade.
    /// </summary>
    public interface IAnswerFacade : IFacade
    {
        /// <summary>
        /// Delete a answer.
        /// </summary>
        /// <param name="id">Delete data.</param>
        /// <returns>Answer object</returns>
        OperationResult<IAnswerDTO> DeleteAnswerById(int id);
    }
}