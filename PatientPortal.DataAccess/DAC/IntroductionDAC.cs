//-----------------------------------------------------------------------
// <copyright file="IntroductionDAC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the IntroductionDAC.cs file.</summary>
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
    public class IntroductionDAC : DACBase, IIntroductionDAC
    {
        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntroductionDAC"/> class.
        /// </summary>
        /// <param name="exceptionManager">Exception manager.</param>
        public IntroductionDAC(IExceptionManager exceptionManager)
        {
            this.exceptionManager = exceptionManager;
        }

        /// <summary>
        /// Gets the introduction by language id.
        /// </summary>
        /// <param name="languageId">Introduction id.</param>
        /// <returns>Introduction information.</returns>
        public IIntroductionDTO GetIntroductionByLanguageId(int languageId)
        {
            IIntroductionDTO introductionDTO = null;
            try
            {
                PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();
                Introduction introduction = PatientPortalEntities.Introductions.Where(c => c.LanguageId == languageId).FirstOrDefault();
                introductionDTO = Mapper.Map<Introduction, IntroductionDTO>(introduction);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while fetching introduction.", ex);
            }

            return introductionDTO;
        }
    }
}