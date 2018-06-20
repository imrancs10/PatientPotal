//-----------------------------------------------------------------------
// <copyright file="TerritoriesEmailDTO.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the TerritoriesEmailDTO.cs file.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.DTOModel
{
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;
    using PatientPortal.Entities;
    using PatientPortal.Shared;
    using System;

    /// <summary>
    /// Represents the DTO class for territories email.
    /// </summary>
    [Serializable]
    [DataContract]
    public class TerritoriesEmailDTO : DTOBase, ITerritoriesEmailDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TerritoriesEmailDTO"/> class.
        /// </summary>
        public TerritoriesEmailDTO()
        {
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the StateId.
        /// </summary>
        /// <value>The StateId.</value>
        [DataMember]
        public int StateId { get; set; }

        /// <summary>
        /// Gets or sets the EmailId.
        /// </summary>
        /// <value>The EmailId.</value>
        [DataMember]
        public string EmailId { get; set; }

        /// <summary>
        /// Gets or sets the ErrorCode.
        /// </summary>
        /// <value>The ErrorCode.</value>
        public override int ErrorCode { get; set; }

    }
}