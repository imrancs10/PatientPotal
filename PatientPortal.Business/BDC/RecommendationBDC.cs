//-----------------------------------------------------------------------
// <copyright file="RecommendationBDC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the RecommendationBDC file.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Business
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using PatientPortal.Shared;

    /// <summary>
    /// Represents the business domain component for Recommendation.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public class RecommendationBDC : BDCBase, IRecommendationBDC
    {
        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Instance of Recommendation DAC.
        /// </summary>
        private IRecommendationDAC dac;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecommendationBDC"/> class.
        /// </summary>
        /// <param name="exceptionManager">The exception manager.</param>
        /// <param name="userDac">The user data access Component.</param>
        public RecommendationBDC(IExceptionManager exceptionManager, IRecommendationDAC userDac)
        {
            this.exceptionManager = exceptionManager;
            this.dac = userDac;
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
                IList<IRecommendedProductDTO> surveyList = this.dac.GetRecommendationList(Guid);

                if (surveyList == null)
                {
                    result = OperationResult<IList<IRecommendedProductDTO>>.CreateFailureResult("The object containing survey recommendation list is NULL !");
                }
                else
                {
                    result = OperationResult<IList<IRecommendedProductDTO>>.CreateSuccessResult(surveyList, "Survey question list fetched successfully!");
                }
            }
            catch (DACException dacEx)
            {
                this.exceptionManager.HandleException(dacEx, dacEx.Message);
                result = OperationResult<IList<IRecommendedProductDTO>>.CreateErrorResult(dacEx.Message, dacEx.StackTrace);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx, bdcEx.Message);
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