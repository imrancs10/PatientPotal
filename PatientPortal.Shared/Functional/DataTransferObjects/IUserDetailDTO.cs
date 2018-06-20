//-----------------------------------------------------------------------
// <copyright file="IUserDetailDTO.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary> This is the IUserDetailDTO class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines a contract for user detail DTO.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public interface IUserDetailDTO : IDTO
    {
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The id of the user.</value>
        int? UserId { get; set; }

        /// <summary>
        /// Gets or sets the business name.
        /// </summary>
        /// <value>The business name.</value>
        string BusinessName { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name field.</value>
        string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name field.</value>
        string LastName { get; set; }

        /// <summary>
        /// Gets or sets the state id.
        /// </summary>
        /// <value>The state id field.</value>
        int StateId { get; set; }

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        /// <value>The zip code field.</value>
        string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>The phone number field.</value>
        string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the email id.
        /// </summary>
        /// <value>The email id field.</value>
        string EmailId { get; set; }

        /// <summary>
        /// Gets or sets the Password.
        /// </summary>
        /// <value>The Password field.</value>
        string Password { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>The createde date.</value>
        DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>The molified date.</value>
        DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the promotional.
        /// </summary>
        /// <value>The prmmotinal field.</value>
        bool IsReqForPromotions { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>The molified date.</value>
        bool IsPDFDownload { get; set; }

        /// <summary>
        /// Gets or sets the PDFFileName.
        /// </summary>
        /// <value>The PDFFileName.</value>
        string PDFFileName { get; set; }

        List<string> TerritoryEmails { get; set; }
    }
}