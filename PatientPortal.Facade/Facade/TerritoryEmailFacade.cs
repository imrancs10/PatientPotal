//-----------------------------------------------------------------------
// <copyright file="TerritoryEmailFacade.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the TerritoryEmailFacade.cs file.</summary>
//-----------------------------------------------------------------------
 

namespace PatientPortal.Facade
{
    using System;
    using System.Collections.Generic;
    using PatientPortal.Shared;

    /// <summary>
    /// Represents the facade for TerritoryEmailFacade.
    /// </summary>
    public class TerritoryEmailFacade : FacadeBase, ITerritoryEmailFacade
    {
        /// <summary>
        /// Instance of TerritoryEmail BDC.
        /// </summary>
        private readonly ITerritoryEmailBDC bdc;

        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="TerritoryEmailFacade"/> class.
        /// </summary>
        /// <param name="exceptionManager">The exception Manager.</param>
        /// <param name="bdc">The config Business Domain Component.</param>
        public TerritoryEmailFacade(IExceptionManager exceptionManager, ITerritoryEmailBDC bdc)
        {
            this.exceptionManager = exceptionManager;
            this.bdc = bdc;
        }

        /// <summary>
        /// Get Territoryn email on state wise
        /// </summary>
        /// <param name="StateId">object of type int</param>
        /// <returns>object of type IList<ITerritoriesEmailDTO></returns>
        public OperationResult<IList<ITerritoriesEmailDTO>> GetTerritoryEmails(int StateId)
        {
            OperationResult<IList<ITerritoriesEmailDTO>> result;
            try
            {
                result = this.bdc.GetTerritoryEmails(StateId);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx);
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