//-----------------------------------------------------------------------
// <copyright file="IntroductionBDC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the IntroductionBDC.cs file.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Business
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using PatientPortal.Shared;

    /// <summary>
    /// Represents the business domain component for question data.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public class IntroductionBDC : BDCBase, IIntroductionBDC
    {
        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Instance of Introduction DAC.
        /// </summary>
        private IIntroductionDAC dac;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntroductionBDC"/> class.
        /// </summary>
        /// <param name="exceptionManager">The exception manager.</param>
        /// <param name="userDac">The user data access Component.</param>
        public IntroductionBDC(IExceptionManager exceptionManager, IIntroductionDAC userDac)
        {
            this.exceptionManager = exceptionManager;
            this.dac = userDac;
        }
        
        /// <summary>
        /// Gets the introduction object.
        /// </summary>
        /// <param name="id">Language id.</param>
        /// <returns>Introduction data with operation result.</returns>
        [SuppressMessage("Microsoft.Design", "CA1031", Justification = "Generic exception caught after specifying specic exception", Scope = "Just for this method")]
        public OperationResult<IIntroductionDTO> GetIntroductionByLanguageId(int id)
        {
            OperationResult<IIntroductionDTO> result;
            try
            {
                IIntroductionDTO introductionDTO = this.dac.GetIntroductionByLanguageId(id);
                if (introductionDTO == null)
                {
                    result = OperationResult<IIntroductionDTO>.CreateFailureResult("The object containing introduction list is NULL !");
                }
                else
                {
                    result = OperationResult<IIntroductionDTO>.CreateSuccessResult(introductionDTO, "Introduction list fetched successfully!");
                }
            }
            catch (DACException dacEx)
            {
                this.exceptionManager.HandleException(dacEx, dacEx.Message);
                result = OperationResult<IIntroductionDTO>.CreateErrorResult(dacEx.Message, dacEx.StackTrace);
            }
            catch (BDCException bdcEx)
            {
                this.exceptionManager.HandleException(bdcEx, bdcEx.Message);
                result = OperationResult<IIntroductionDTO>.CreateErrorResult(bdcEx.Message, bdcEx.StackTrace);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                result = OperationResult<IIntroductionDTO>.CreateErrorResult(ex.Message, ex.StackTrace);
            }

            return result;
        }
    }
}