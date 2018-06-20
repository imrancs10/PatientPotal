//-----------------------------------------------------------------------
// <copyright file="OperationResultType.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the OperationResultType class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    /// <summary>
    /// Defines the types of business operation result.
    /// </summary>
    public enum OperationResultType
    {
        /// <summary>
        /// Indicates that operation has succeeded.
        /// </summary>
        Success = 0,

        /// <summary>
        /// Indicates that operation has failed due to some validation logic.
        /// </summary>
        Failure = 1,

        /// <summary>
        /// Indicates that operation has encountered an un-handled/custom exception.
        /// </summary>
        Error = 2
    }
}