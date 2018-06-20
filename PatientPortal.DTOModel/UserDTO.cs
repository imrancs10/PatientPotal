//-----------------------------------------------------------------------
// <copyright file="CustomerDTO.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the CustomerDTO.cs file.</summary>
//-----------------------------------------------------------------------
 
namespace YaleNexTouch.DTOModel
{
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;
    using YaleNexTouch.Entities;
    using YaleNexTouch.Shared;
    using System;

    /// <summary>
    /// Represents the DTO class for customer.
    /// </summary>
    [Serializable]
    [DataContract]
    [EntityMapping("YaleNexTouch.Entities.Entities.User", MappingType.TotalExplicit)]
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public class UserDTO : DTOBase, IUserDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerDTO"/> class.
        /// </summary>
        public UserDTO()
        {
        }

        [DataMember]
        [EntityPropertyMapping(MappingDirectionType.Both, "Id")]

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        public int Id { get; set; }

        [DataMember]
        [EntityPropertyMapping(MappingDirectionType.Both, "UserName")]

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name of customer.</value>
        public string UserName { get; set; }

        [DataMember]
        [EntityPropertyMapping(MappingDirectionType.Both, "Password")]

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password id.</value>
        public string Password { get; set; }

        [DataMember]
        [EntityPropertyMapping(MappingDirectionType.Both, "Type")]

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type field.</value>
        public int RoleId { get; set; }

        [DataMember]
        [EntityPropertyMapping(MappingDirectionType.Both, "IsActive")]

        /// <summary>
        /// Gets or sets the active.
        /// </summary>
        /// <value>The active field.</value>
        public bool IsActive { get; set; }

        [DataMember]
        [EntityPropertyMapping(MappingDirectionType.Both, "IsDeleted")]

        /// <summary>
        /// Gets or sets the delete.
        /// </summary>
        /// <value>The delete field.</value>
        public bool IsDeleted { get; set; }

        [DataMember]
        [EntityPropertyMapping(MappingDirectionType.Both, "CreatedDate")]

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>The createde date.</value>
        public DateTime CreatedDate { get; set; }

        [DataMember]
        [EntityPropertyMapping(MappingDirectionType.Both, "ModifiedDate")]

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>The molified date.</value>
        public DateTime ModifiedDate { get; set; }
    }
}