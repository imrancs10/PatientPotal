//-----------------------------------------------------------------------
// <copyright file="IStateDAC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the IStateDAC class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines a contract for IState DAC.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface IStateDAC : IDataAccessComponent
    {
        /// <summary>
        /// Get state list
        /// </summary>
        /// <returns>object of type IList<IStateDTO></returns>
        IList<IStateDTO> GetStateList();
    }
}