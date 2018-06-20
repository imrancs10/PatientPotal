//-----------------------------------------------------------------------
// <copyright file="IRecommendationFacade.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the IRecommendationFacade class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines the contract for RecommendationFacade.
    /// </summary>
    public interface IRecommendationFacade : IFacade
    {
        /// <summary>
        /// Get Recommendation list for the filled survey
        /// </summary>
        /// <returns>object of type IList<IRecommendedProductDTO></returns>
        OperationResult<IList<IRecommendedProductDTO>> GetRecommendationList(string Guid);
    }
}