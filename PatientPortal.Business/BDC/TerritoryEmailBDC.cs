//-----------------------------------------------------------------------
// <copyright file="TerritoryEmailBDC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the TerritoryEmailBDC.cs file.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Business
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using PatientPortal.Shared;

    /// <summary>
    /// Represents the business domain component for TerritoryEmail.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public class TerritoryEmailBDC : BDCBase, ITerritoryEmailBDC
    {
        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Instance of TerritoryEmail DAC.
        /// </summary>
        private ITerritoryEmailDAC dac;

        /// <summary>
        /// Initializes a new instance of the <see cref="TerritoryEmailBDC"/> class.
        /// </summary>
        /// <param name="exceptionManager">The exception manager.</param>
        /// <param name="userDac">The user data access Component.</param>
        public TerritoryEmailBDC(IExceptionManager exceptionManager, ITerritoryEmailDAC userDac)
        {
            this.exceptionManager = exceptionManager;
            this.dac = userDac;
        }

        /// <summary>
        /// Get Territoryn email on state wise
        /// </summary>
        /// <param name="StateId">object of type int</param>
        /// <returns>object of type IList<ITerritoriesEmailDTO></returns>
        public OperationResult<IList<ITerritoriesEmailDTO>>  GetTerritoryEmails(int StateId)
        {
            OperationResult<IList<ITerritoriesEmailDTO>> result;
            try
            {
                IList<ITerritoriesEmailDTO> stateList = this.dac.GetTerritoryEmails(StateId);

                if (stateList == null)
                {
                    result = OperationResult<IList<ITerritoriesEmailDTO>>.CreateFailureResult("The object containing state list is NULL !");
                }
                else
                {
                    result = OperationResult<IList<ITerritoriesEmailDTO>>.CreateSuccessResult(stateList, "State fetched successfully!");
                }
            }
            catch (DACException dacEx)
            {
                this.exceptionManager.HandleException(dacEx, dacEx.Message);
                result = OperationResult<IList<ITerritoriesEmailDTO>>.CreateErrorResult(dacEx.Message, dacEx.StackTrace);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx, bdcEx.Message);
                result = OperationResult<IList<ITerritoriesEmailDTO>>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<IList<ITerritoriesEmailDTO>>.CreateErrorResult(ex.Message, ex.StackTrace);
            }

            return result;
        }

    }
}