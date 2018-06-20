//-----------------------------------------------------------------------
// <copyright file="UserDetailDTO.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the UserDetailDTO.cs file.</summary>
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
    /// Represents the DTO class for user detail.
    /// </summary>
    [Serializable]
    [DataContract]
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public class UserDetailDTO : DTOBase, IUserDetailDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserDetailDTO"/> class.
        /// </summary>
        public UserDetailDTO()
        {
        }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The id of the user.</value>
        [DataMember]
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or sets the business name.
        /// </summary>
        /// <value>The business name.</value>
        [DataMember]
        public string BusinessName { get; set; }
        
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name field.</value>
        [DataMember]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name field.</value>
        [DataMember]
        public string LastName { get; set; }
        
        /// <summary>
        /// Gets or sets the state id.
        /// </summary>
        /// <value>The state id field.</value>
        [DataMember]
        public int StateId { get; set; }
        
        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        /// <value>The zip code field.</value>
        [DataMember]
        public string ZipCode { get; set; }
        
        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>The phone number field.</value>
        [DataMember]
        public string PhoneNumber { get; set; }
        
        /// <summary>
        /// Gets or sets the email id.
        /// </summary>
        /// <value>The email id field.</value>
        [DataMember]
        public string EmailId { get; set; }

        /// <summary>
        /// Gets or sets the Password.
        /// </summary>
        /// <value>The Password field.</value>
        [DataMember]
        public string Password { get; set; }

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
        /// Gets or sets the promotional.
        /// </summary>
        /// <value>The prmmotinal field.</value>
        [DataMember]
        public bool IsReqForPromotions { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>The molified date.</value>
        [DataMember]
        public bool IsPDFDownload { get; set; }

        /// <summary>
        /// Gets or sets the promotional.
        /// </summary>
        /// <value>The prmmotinal field.</value>
        [DataMember]
        public bool IsAdvisorToContact { get; set; }

        /// <summary>
        /// Gets or sets the state Name.
        /// </summary>
        /// <value>The state Name field.</value>
        [DataMember]
        public string StateName { get; set; }

        /// <summary>
        /// Gets or sets the state Name.
        /// </summary>
        /// <value>The state Name field.</value>
        [DataMember]
        public string SurveyName { get; set; }

        /// <summary>
        /// Gets or sets the IUserDTO.
        /// </summary>
        /// <value>The IUserDTO.</value>
        [DataMember]
        public IUserDTO UserDTO { get; set; }

        [DataMember]
        public List<string> TerritoryEmails { get; set; }

        /// <summary>
        /// Gets or sets the PDFFileName.
        /// </summary>
        /// <value>The PDFFileName.</value>
        [DataMember]
        public string PDFFileName { get; set; }

        /// <summary>
        /// Gets or sets the error code
        /// </summary>
        [DataMember]
        public override int ErrorCode { get; set; }
    }
}