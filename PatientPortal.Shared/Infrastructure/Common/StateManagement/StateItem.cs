//-----------------------------------------------------------------------
// <copyright file="StateItem.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the StateItem class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared.Infrastructure.Common.StateManagement
{
    using System;
    
    /// <summary>
    /// Represents the abstract base class for state manageable entities.
    /// </summary>
    [Serializable]
    public abstract class StateItem: IStateItem
    {
        #region Ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="StateItem"/> class.
        /// </summary>
        protected StateItem()
        {
        }
        #endregion

        ///// <summary>
        ///// Gets or sets the key.
        ///// </summary>
        ///// <value>
        ///// The key.
        ///// </value>
        //public string Key
        //{
        //    get;
        //    set;
        //}
    }
}
