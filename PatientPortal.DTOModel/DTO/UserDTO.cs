//-----------------------------------------------------------------------
// <copyright file="UserDTO.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the UserDTO.cs file.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.DTOModel
{
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;
    using PatientPortal.Entities;
    using PatientPortal.Shared;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the DTO class for user.
    /// </summary>
    [Serializable]
    [DataContract]
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public class UserDTO : DTOBase, IUserDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserDTO"/> class.
        /// </summary>
        public UserDTO()
        {
            this.UserDetailDTO = new UserDetailDTO();
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
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password id.</value>
        [DataMember]
        public string Password { get; set; }

        [DataMember]
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type field.</value>
        public int RoleId { get; set; }

        /// <summary>
        /// Gets or sets the active.
        /// </summary>
        /// <value>The active field.</value>
        [DataMember]
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the delete.
        /// </summary>
        /// <value>The delete field.</value>
        [DataMember]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>The createde date.</value>
        [DataMember]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>The molified date.</value>
        [DataMember]
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        ///  Gets or sets the IsForgetPassword.
        /// </summary>
        /// <value>The flag to maintain whether request coming for forget password or for login while post data.</value>
        [DataMember]
        public bool IsForgetPassword { get; set; }

        /// <summary>
        /// Gets or sets the IRoleDTO.
        /// </summary>
        /// <value>The IRoleDTO.</value>
        [DataMember]
        public IRoleDTO RoleDTO { get; set; }

        /// <summary>
        /// Gets or sets the IUserDetailDTO.
        /// </summary>
        /// <value>The IUserDetailDTO.</value>
        [DataMember]
        public IUserDetailDTO UserDetailDTO { get; set; }

        /// <summary>
        /// Gets or sets the Error Code
        /// </summary>
        /// <value>The ErrorCode.</value>
        [DataMember]
        public override int ErrorCode { get; set; }
    }
}