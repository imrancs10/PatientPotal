//-----------------------------------------------------------------------
// <copyright file="IProductImagesFacade.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the IProductImagesFacade class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines the contract for survey Facade.
    /// </summary>
    public interface IProductImagesFacade : IFacade
    {
        /// <summary>
        /// Delete a answer.
        /// </summary>
        /// <param name="id">Delete data.</param>
        /// <returns>Answer object</returns>
        OperationResult<IProductImagesDTO> DeleteProductImagesById(int id);
    }
}