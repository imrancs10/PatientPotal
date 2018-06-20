//-----------------------------------------------------------------------
// <copyright file="AnswerDAC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the AnswerDAC.cs file.</summary>
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
    public class AnswerDAC : DACBase, IAnswerDAC
    {
        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnswerDAC"/> class.
        /// </summary>
        /// <param name="exceptionManager">Exception manager.</param>
        public AnswerDAC(IExceptionManager exceptionManager)
        {
            this.exceptionManager = exceptionManager;
        }


        /// <summary>
        /// Add a answer.
        /// </summary>
        /// <param name="answerDTO">Answer data.</param>
        /// <returns>Flag indicating if the operation succeed.</returns>
        public bool InsertAnswer(IAnswerDTO answerDTO)
        {
            Answer answerEntity = new Answer();
            answerDTO.CreatedDate = DateTime.UtcNow;
            answerDTO.ModifiedDate = DateTime.UtcNow;
            try
            {
                PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();
                //EntityConverter.FillEntityFromDto(answerDTO, answerEntity);
                answerEntity = Mapper.Map<IAnswerDTO, Answer>(answerDTO);
                PatientPortalEntities.AddToAnswers(answerEntity);
                PatientPortalEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while insert answer.", ex);
            }

            return true;
        }

        /// <summary>
        /// Delete a answer.
        /// </summary>
        /// <param name="id">Answer id.</param>
        /// <returns>Flag indicating if the operation succeed.</returns>
        public IAnswerDTO DeleteAnswerById(int id)
        {
            IAnswerDTO answerDTO = new AnswerDTO();
            try
            {
                PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();
                Answer answer = PatientPortalEntities.Answers.AsQueryable<Answer>().Where(c => c.Id == id).FirstOrDefault();
                answerDTO = Mapper.Map<Answer, AnswerDTO>(answer);
                PatientPortalEntities.Answers.DeleteObject(answer);
                PatientPortalEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while delete answer.", ex);
            }

            return answerDTO;
        }
    }
}