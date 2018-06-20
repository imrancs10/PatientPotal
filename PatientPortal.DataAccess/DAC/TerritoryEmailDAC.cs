//-----------------------------------------------------------------------
// <copyright file="TerritoryEmailDAC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the TerritoryEmailDAC.cs file.</summary>
//-----------------------------------------------------------------------


namespace PatientPortal.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using PatientPortal.Entities.Entities;
    using PatientPortal.Shared;
    using PatientPortal.DTOModel;
    using PatientPortal.Entities;
    using AutoMapper;
    using System.Globalization;
    /// <summary>
    /// Represents Country Data Access Component.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public class TerritoryEmailDAC : DACBase, ITerritoryEmailDAC
    {
        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="TerritoryEmailDAC"/> class.
        /// </summary>
        /// <param name="exceptionManager">Exception manager.</param>
        public TerritoryEmailDAC(IExceptionManager exceptionManager)
        {
            this.exceptionManager = exceptionManager;
        }

        /// <summary>
        /// Get Territory email on state wise
        /// </summary>
        /// <param name="StateId">object of type int</param>
        /// <returns>object of type IList<ITerritoriesEmailDTO></returns>
        public IList<ITerritoriesEmailDTO> GetTerritoryEmails(int StateId)
        {
            IList<ITerritoriesEmailDTO> stateList = new List<ITerritoriesEmailDTO>();
            try
            {
                PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();
                var states = PatientPortalEntities.TerritoriesEmails.AsQueryable<TerritoriesEmail>().Where(x => x.StateId == StateId);
                stateList = Mapper.Map<List<TerritoriesEmail>, List<ITerritoriesEmailDTO>>(states.ToList());
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while fetching survey list.", ex);
            }

            return stateList;
        }

    }
}