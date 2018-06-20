//-----------------------------------------------------------------------
// <copyright file="TextInputTypeDTO.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the TextInputTypeDTO.cs file.</summary>
//-----------------------------------------------------------------------
 
namespace PatientPortal.DTOModel
{
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;
    using PatientPortal.Entities;
    using PatientPortal.Shared;
    using System;

    /// <summary>
    /// Represents the DTO class for question type.
    /// </summary>
    [Serializable]
    [DataContract]
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public class TextInputTypeDTO : DTOBase, ITextInputTypeDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextInputTypeDTO"/> class.
        /// </summary>
        public TextInputTypeDTO()
        {
        }

        /// <summary>
        /// Gets or sets the text input type id.
        /// </summary>
        /// <value>The text input type id.</value>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The type of text input type</value>
        [DataMember]
        public string InputType { get; set; }

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>The ErrorCode</value>
        public override int ErrorCode { get; set; }
    }
}