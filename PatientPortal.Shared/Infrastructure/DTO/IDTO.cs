//-----------------------------------------------------------------------
// <copyright file="IDTO.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the IDTO class.</summary>
//-----------------------------------------------------------------------
namespace PatientPortal.Shared
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines a contract for base DTO.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface IDTO : ICloneable, INotifyPropertyChanged, INotifyPropertyChanging
    {

        /// <summary>
        /// To get Error Message
        /// </summary>
        int ErrorCode { get; set; }
    }
}