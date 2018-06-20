//-----------------------------------------------------------------------
// <copyright file="RoleDTO.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the RoleDTO.cs file.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.DTOModel
{
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;
    using PatientPortal.Entities;
    using PatientPortal.Shared;
    using System;

    /// <summary>
    /// Represents the DTO class for role.
    /// </summary>
    [Serializable]
    [DataContract]
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public class RoleDTO : DTOBase, IRoleDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleDTO"/> class.
        /// </summary>
        public RoleDTO()
        {
        }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name of customer.</value>
        [DataMember]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>The Error Code.</value>
        public override int ErrorCode { get; set; }
    }
}