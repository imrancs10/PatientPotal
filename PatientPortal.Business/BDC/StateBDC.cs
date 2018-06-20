//-----------------------------------------------------------------------
// <copyright file="StateBDC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the StateBDC.cs file.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Business
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using PatientPortal.Shared;

    /// <summary>
    /// Represents the business domain component for State.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public class StateBDC : BDCBase, IStateBDC
    {
        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Instance of State DAC.
        /// </summary>
        private IStateDAC dac;

        /// <summary>
        /// Initializes a new instance of the <see cref="StateBDC"/> class.
        /// </summary>
        /// <param name="exceptionManager">The exception manager.</param>
        /// <param name="userDac">The user data access Component.</param>
        public StateBDC(IExceptionManager exceptionManager, IStateDAC userDac)
        {
            this.exceptionManager = exceptionManager;
            this.dac = userDac;
        }

        /// <summary>
        /// Get state list
        /// </summary>
        /// <returns>object of type IList<IStateDTO></returns>
        public OperationResult<IList<IStateDTO>> GetStateList()
        {
            OperationResult<IList<IStateDTO>> result;
            try
            {
                IList<IStateDTO> stateList = this.dac.GetStateList();

                if (stateList == null)
                {
                    result = OperationResult<IList<IStateDTO>>.CreateFailureResult("The object containing state list is NULL !");
                }
                else
                {
                    result = OperationResult<IList<IStateDTO>>.CreateSuccessResult(stateList, "State fetched successfully!");
                }
            }
            catch (DACException dacEx)
            {
                this.exceptionManager.HandleException(dacEx, dacEx.Message);
                result = OperationResult<IList<IStateDTO>>.CreateErrorResult(dacEx.Message, dacEx.StackTrace);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx, bdcEx.Message);
                result = OperationResult<IList<IStateDTO>>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<IList<IStateDTO>>.CreateErrorResult(ex.Message, ex.StackTrace);
            }

            return result;
        }

    }
}