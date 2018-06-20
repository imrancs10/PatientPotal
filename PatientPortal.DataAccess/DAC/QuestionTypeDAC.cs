//-----------------------------------------------------------------------
// <copyright file="QuestionTypeDAC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the QuestionTypeDAC.cs file.</summary>
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
    public class QuestionTypeDAC : DACBase, IQuestionTypeDAC
    {
        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionTypeDAC"/> class.
        /// </summary>
        /// <param name="exceptionManager">Exception manager.</param>
        public QuestionTypeDAC(IExceptionManager exceptionManager)
        {
            this.exceptionManager = exceptionManager;
        }

        /// <summary>
        /// Gets the question type list.
        /// </summary>
        /// <returns>List of question type.</returns>
        public IList<IQuestionTypeDTO> GetQuestionTypeList()
        {
            IList<IQuestionTypeDTO> questionTypeList = new List<IQuestionTypeDTO>();

            try
            {
                PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();
                IQueryable<QuestionType> questionTypeListObj = PatientPortalEntities.QuestionTypes.AsQueryable<QuestionType>();
                foreach (QuestionType questionType in questionTypeListObj)
                {
                    IQuestionTypeDTO questionTypeDTO = new QuestionTypeDTO();
                    questionTypeDTO = Mapper.Map<QuestionType, QuestionTypeDTO>(questionType);
                    questionTypeList.Add(questionTypeDTO);
                }
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while fetching question list.", ex);
            }

            return questionTypeList;
        }
    }
}