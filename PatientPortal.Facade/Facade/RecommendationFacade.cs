//-----------------------------------------------------------------------
// <copyright file="RecommendationFacade.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the RecommendationFacade.cs file.</summary>
//-----------------------------------------------------------------------
 

namespace PatientPortal.Facade
{
    using System;
    using System.Collections.Generic;
    using PatientPortal.Shared;

    /// <summary>
    /// Represents the facade for Recommendation.
    /// </summary>
    public class RecommendationFacade : FacadeBase, IRecommendationFacade
    {
        /// <summary>
        /// Instance of Recommendation BDC.
        /// </summary>
        private readonly IRecommendationBDC bdc;

        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecommendationFacade"/> class.
        /// </summary>
        /// <param name="exceptionManager">The exception Manager.</param>
        /// <param name="bdc">The config Business Domain Component.</param>
        public RecommendationFacade(IExceptionManager exceptionManager, IRecommendationBDC bdc)
        {
            this.exceptionManager = exceptionManager;
            this.bdc = bdc;
        }

        /// <summary>
        /// Get Recommendation list for the filled survey
        /// </summary>
        /// <returns>object of type IList<IRecommendedProductDTO></returns>
        public OperationResult<IList<IRecommendedProductDTO>> GetRecommendationList(string Guid)
        {
            OperationResult<IList<IRecommendedProductDTO>> result;
            try
            {
                result = this.bdc.GetRecommendationList(Guid);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx);
                result = OperationResult<IList<IRecommendedProductDTO>>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<IList<IRecommendedProductDTO>>.CreateErrorResult(ex.Message, ex.StackTrace);
            }

            return result;
        }

    }
}