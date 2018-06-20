//-----------------------------------------------------------------------
// <copyright file="TextInputTypeDAC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the TextInputTypeDAC.cs file.</summary>
//-----------------------------------------------------------------------
 

namespace PatientPortal.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using PatientPortal.Entities.Entities;
    using PatientPortal.Shared;
    using PatientPortal.DTOModel;
    using PatientPortal.Entities;
    using AutoMapper;          

    /// <summary>
    /// Represents Country Data Access Component.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public class TextInputTypeDAC : DACBase, ITextInputTypeDAC
    {
        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextInputTypeDAC"/> class.
        /// </summary>
        /// <param name="exceptionManager">Exception manager.</param>
        public TextInputTypeDAC(IExceptionManager exceptionManager)
        {
            this.exceptionManager = exceptionManager;
        }

        /// <summary>
        /// Gets the text input type list.
        /// </summary>
        /// <returns>List of text input type.</returns>
        public IList<ITextInputTypeDTO> GetTextInputTypeList()
        {
            IList<ITextInputTypeDTO> textInputTypeList = new List<ITextInputTypeDTO>();

            try
            {
                PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();
                IQueryable<TextInputType> questionTypeListObj = PatientPortalEntities.TextInputTypes.AsQueryable<TextInputType>();
                foreach (TextInputType textInputType in questionTypeListObj)
                {
                    ITextInputTypeDTO textInputTypeDTO = new TextInputTypeDTO();
                    var textInputTypeObj = Mapper.Map<TextInputType, TextInputTypeDTO>(textInputType);
                    textInputTypeList.Add(textInputTypeObj);
                }
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while fetching text input list.", ex);
            }

            return textInputTypeList;
        }
    }
}