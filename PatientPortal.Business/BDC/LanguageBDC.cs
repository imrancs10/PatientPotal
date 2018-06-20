//-----------------------------------------------------------------------
// <copyright file="LanguageBDC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the LanguageBDC.cs file.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Business
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using PatientPortal.Shared;

    /// <summary>
    /// Represents the business domain component for language data.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public class LanguageBDC : BDCBase, ILanguageBDC
    {
        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Instance of Survey DAC.
        /// </summary>
        private ILanguageDAC dac;

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageBDC"/> class.
        /// </summary>
        /// <param name="exceptionManager">The exception manager.</param>
        /// <param name="languageDac">The user data access Component.</param>
        public LanguageBDC(IExceptionManager exceptionManager, ILanguageDAC languageDac)
        {
            this.exceptionManager = exceptionManager;
            this.dac = languageDac;
        }

        /// <summary>
        /// Gets the language list.
        /// </summary>
        /// <returns>Result of the operation.</returns>
        [SuppressMessage("Microsoft.Design", "CA1031", Justification = "Generic exception caught after specifying specic exception", Scope = "Just for this method")]
        public OperationResult<IList<ILanguageDTO>> GetLanguageList()
        {
            OperationResult<IList<ILanguageDTO>> result;
            try
            {
                IList<ILanguageDTO> languageList = this.dac.GetLanguageList();

                if (languageList == null)
                {
                    result = OperationResult<IList<ILanguageDTO>>.CreateFailureResult("The object containing survey list is NULL !");
                }
                else
                {
                    result = OperationResult<IList<ILanguageDTO>>.CreateSuccessResult(languageList, "Language list fetched successfully!");
                }
            }
            catch (DACException dacEx)
            {
                this.exceptionManager.HandleException(dacEx, dacEx.Message);
                result = OperationResult<IList<ILanguageDTO>>.CreateErrorResult(dacEx.Message, dacEx.StackTrace);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx, bdcEx.Message);
                result = OperationResult<IList<ILanguageDTO>>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<IList<ILanguageDTO>>.CreateErrorResult(ex.Message, ex.StackTrace);
            }

            return result;
        }
    }
}