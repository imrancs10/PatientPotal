//-----------------------------------------------------------------------
// <copyright file="IRecommendationDAC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the IRecommendationDAC class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines a contract for IRecommendation DAC.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface IRecommendationDAC : IDataAccessComponent
    {
        /// <summary>
        /// Get Recommendation list for the filled survey
        /// </summary>
        /// <returns>object of type IList<IRecommendedProductDTO></returns>
        IList<IRecommendedProductDTO> GetRecommendationList(string Guid);
    }
}