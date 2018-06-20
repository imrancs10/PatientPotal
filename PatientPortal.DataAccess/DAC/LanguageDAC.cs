//-----------------------------------------------------------------------
// <copyright file="LanguageDAC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the LanguageDAC.cs file.</summary>
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
    public class LanguageDAC : DACBase, ILanguageDAC
    {
        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageDAC"/> class.
        /// </summary>
        /// <param name="exceptionManager">Exception manager.</param>
        public LanguageDAC(IExceptionManager exceptionManager)
        {
            this.exceptionManager = exceptionManager;
        }

        /// <summary>
        /// Gets the language list.
        /// </summary>
        /// <returns>List of language.</returns>
        public IList<ILanguageDTO> GetLanguageList()
        {
            IList<ILanguageDTO> surveyList = new List<ILanguageDTO>();

            try
            {
                PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();
                IQueryable<Language> languages = PatientPortalEntities.Languages.AsQueryable<Language>();
                foreach (Language language in languages)
                {
                    ILanguageDTO languageDTO = new LanguageDTO();
                    languageDTO = Mapper.Map<Language, LanguageDTO>(language);
                    surveyList.Add(languageDTO);
                }
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while fetching survey list.", ex);
            }

            return surveyList;
        }

     
    }
}