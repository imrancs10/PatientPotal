//-----------------------------------------------------------------------
// <copyright file="StateDTO.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the StateDTO.cs file.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.DTOModel
{
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;
    using PatientPortal.Entities;
    using PatientPortal.Shared;
    using System;

    /// <summary>
    /// Represents the DTO class for customer.
    /// </summary>
    [Serializable]
    [DataContract]
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public class StateDTO : DTOBase, IStateDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateDTO"/> class.
        /// </summary>
        public StateDTO()
        {
        }

        /// <summary>
        /// Gets or sets the state id.
        /// </summary>
        /// <value>The state id.</value>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the state code.
        /// </summary>
        /// <value>The state code.</value>
        [DataMember]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>The error code.</value>
        public override int ErrorCode { get; set; }

    }
}