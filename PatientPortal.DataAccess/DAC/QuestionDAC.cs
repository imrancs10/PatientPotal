//-----------------------------------------------------------------------
// <copyright file="QuestionDAC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the QuestionDAC.cs file.</summary>
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
    public class QuestionDAC : DACBase, IQuestionDAC
    {
        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionDAC"/> class.
        /// </summary>
        /// <param name="exceptionManager">Exception manager.</param>
        public QuestionDAC(IExceptionManager exceptionManager)
        {
            this.exceptionManager = exceptionManager;
        }

        /// <summary>
        /// Gets the question list.
        /// </summary>
        /// <returns>List of question.</returns>
        public IList<IQuestionDTO> GetQuestionList()
        {
            List<IQuestionDTO> questionList = new List<IQuestionDTO>();
            try
            {
                PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();
                IList<Question> questions = PatientPortalEntities.Questions.Include("QuestionType").Include("TextInputType").Include("Language").ToList<Question>();
                foreach (Question quest in questions)
                {
                    var question = Mapper.Map<Question, QuestionDTO>(quest);
                    question.IsMapped = CheckQuestionIsMapped(PatientPortalEntities, quest);
                    questionList.Add(question);
                }
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while fetching question list.", ex);
            }
            return questionList;
        }

        /// <summary>
        /// Check if this question is mapped with any survey
        /// </summary>
        /// <param name="PatientPortalEntities">PatientPortalEntity Object</param>
        /// <param name="questR>questionEntity Object</param>
        /// <returns>return trure or false</returns>
        private bool CheckQuestionIsMapped(PatientPortalEntities PatientPortalEntities, Question question)
        {
            bool isMapped = false;
            SurveyQuestionAnswer surveyQuestionAnswer = PatientPortalEntities.SurveyQuestionAnswers.Where(c => c.QuestionId == question.Id).FirstOrDefault();
            if (surveyQuestionAnswer != null)
            {
                isMapped = true;
            }
            return isMapped;
        }



        /// <summary>
        /// Gets the question list.
        /// </summary>
        /// <returns>List of question.</returns>
        public IList<IQuestionDTO> GetSurveyQuestionList(int languageId)
        {
            List<IQuestionDTO> questionList = new List<IQuestionDTO>();
            try
            {
                PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();
                IList<Question> questions = PatientPortalEntities.Questions.Include("Answers").ToList<Question>().Where(c => c.LanguageId == languageId && c.IsActive == true).ToList();
                foreach (Question quest in questions)
                {
                    var question = Mapper.Map<Question, QuestionDTO>(quest);
                    questionList.Add(question);
                }
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while fetching question list.", ex);
            }
            return questionList;
        }

        /// <summary>
        /// Gets the question by id.
        /// </summary>
        /// <param name="id">Question id.</param>
        /// <returns>Question information.</returns>
        public IQuestionDTO GetQuestionById(int id)
        {
            IQuestionDTO questionDTO = null;
            try
            {
                PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();
                Question question = PatientPortalEntities.Questions.Include("QuestionType").Include("TextInputType").Include("Language").Where(c => c.Id == id).FirstOrDefault();
                if (question != null && question.Id > 0)
                {
                   List<AnswerDTO> answers = PatientPortalEntities.Answers.Where(c => c.QuestionId == question.Id).Select(c => new AnswerDTO { Id = c.Id, QuestionId = c.QuestionId, Title = c.Title, ToolTip = c.ToolTip, ImagePath = c.ImagePath }).ToList<AnswerDTO>();
                   if (answers.Count > 0)
                   {
                       foreach (AnswerDTO answer in answers)
                       {
                           var answerObj = Mapper.Map<AnswerDTO, Answer>(answer);
                           question.Answers.Add(answerObj);
                       }
                   }
                    questionDTO = Mapper.Map<Question, QuestionDTO>(question);
                    questionDTO.IsMapped = CheckQuestionIsMapped(PatientPortalEntities, question);
                }
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while fetching question list.", ex);
            }

            return questionDTO;
        }

        /// <summary>
        /// Add a question.
        /// </summary>
        /// <param name="questionDTO">Question data.</param>
        /// <returns>Flag indicating if the operation succeed.</returns>
        public bool InsertQuestion(IQuestionDTO questionDTO)
        {
            Question questionEntity = new Question();
            questionDTO.CreatedDate = DateTime.UtcNow;
            questionDTO.ModifiedDate = DateTime.UtcNow;
            try
            {
                PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();
                questionEntity = Mapper.Map<IQuestionDTO, Question>(questionDTO);
                PatientPortalEntities.AddToQuestions(questionEntity);

                if (questionDTO.IAnswerDTOList.Count() > 0)
                {
                    // Save Answer of relevant question
                    AnswerDAC answerDAC = new AnswerDAC(this.exceptionManager);
                    foreach (IAnswerDTO answer in questionDTO.IAnswerDTOList)
                    {
                        answer.QuestionId = questionEntity.Id;
                        answer.CreatedDate = DateTime.UtcNow;
                        answer.ModifiedDate = DateTime.UtcNow;
                        var answerEntity = Mapper.Map<IAnswerDTO, Answer>(answer);
                        PatientPortalEntities.AddToAnswers(answerEntity);
                    }
                }
                PatientPortalEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while insert question.", ex);
            }

            return true;
        }

        /// <summary>
        /// Update a question.
        /// </summary>
        /// <param name="questionDTO">Question data.</param>
        /// <returns>Flag indicating if the operation succeed.</returns>
        public IQuestionDTO UpdateQuestion(IQuestionDTO questionDTO)
        {
            questionDTO.ModifiedDate = DateTime.UtcNow;
            try
            {
                List<IAnswerDTO> answerList = new List<IAnswerDTO>();
                PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();
                Question questionEntity = PatientPortalEntities.Questions.Include("Answers").AsQueryable<Question>().Where(c => c.Id == questionDTO.Id).FirstOrDefault();
                questionDTO.CreatedDate = questionEntity.CreatedDate;


                var newAnswerList = (from e in questionDTO.IAnswerDTOList
                                     select new
                                     {
                                         answerId = e.Id
                                     }).ToList();

                var oldAnswerList = (from e in questionEntity.Answers
                                     select new
                                     {
                                         answerId = e.Id
                                     }).ToList();

                // Find all the answers to be removed
                var answersToBeRemoved = oldAnswerList.Except(newAnswerList).ToArray();

                // Find all answers to be updated
                var AnswersToBeUpdated = oldAnswerList.Except(answersToBeRemoved).ToArray();

                // Answers removed
                if (answersToBeRemoved.Count() > 0)
                {
                    foreach (var answer in answersToBeRemoved)
                    {
                        if (questionEntity.Answers.Any(c => c.Id == answer.answerId))
                        {
                            Answer answerToBeRemove = questionEntity.Answers.Where(t => t.Id == answer.answerId).FirstOrDefault();
                            PatientPortalEntities.Answers.DeleteObject(answerToBeRemove);
                            var answerObj = Mapper.Map<Answer, AnswerDTO>(answerToBeRemove);
                            answerList.Add(answerObj);
                        }
                    }
                }

                // Add answer for a question
                if (questionDTO.IAnswerDTOList != null && questionDTO.IAnswerDTOList.Count() > 0)
                {
                    // Save Answer of relevant question
                    foreach (var answer in questionDTO.IAnswerDTOList)
                    {
                        if (answer.Id <= 0)
                        {
                            // Insert new record
                            Answer answerEntity = new Answer();
                            answer.QuestionId = questionEntity.Id;
                            answer.CreatedDate = DateTime.UtcNow;
                            answer.ModifiedDate = DateTime.UtcNow;

                            answerEntity = Mapper.Map<IAnswerDTO, Answer>(answer);
                            PatientPortalEntities.AddToAnswers(answerEntity);
                        }
                        else if (AnswersToBeUpdated.Any(x => x.answerId == answer.Id))
                        {
                            // Update existing records
                            Answer answerToBeUpdate = questionEntity.Answers.FirstOrDefault(t => t.Id == answer.Id);
                            this.UpdateAnswer(answerToBeUpdate, answer);
                        }
                    }
                }

                Mapper.Map<IQuestionDTO, Question>(questionDTO, questionEntity);
                PatientPortalEntities.SaveChanges();

                questionDTO.IAnswerDTOList.Clear();
                if (answerList.Count() > 0)
                {
                    foreach (IAnswerDTO answerObj in answerList)
                    {
                        questionDTO.IAnswerDTOList.Add(answerObj);
                    }
                }
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while update question.", ex);
            }

            return questionDTO;
        }

        /// <summary>
        /// Update answer properties
        /// </summary>
        /// <param name="oldAnswer">Old answer object</param>
        /// <param name="answerDTO">New answer object</param>
        public void UpdateAnswer(Answer oldAnswer, IAnswerDTO answerDTO)
        {
            oldAnswer.Title = answerDTO.Title;
            oldAnswer.ToolTip = answerDTO.ToolTip;
            oldAnswer.ImagePath = answerDTO.ImagePath;
            oldAnswer.ModifiedDate = DateTime.UtcNow;
        }


        /// <summary>
        /// Delete a question.
        /// </summary>
        /// <param name="id">Question data.</param>
        /// <returns>Flag indicating if the operation succeed.</returns>
        public IList<IAnswerDTO> DeleteQuestionById(int id)
        {
            List<IAnswerDTO> answerList = new List<IAnswerDTO>();
            try
            {
                PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();
                Question question = PatientPortalEntities.Questions.Include("Answers").AsQueryable<Question>().Where(c => c.Id == id).FirstOrDefault();
                if (question.Answers.Count() > 0)
                {
                    AnswerDAC answerDAC = new AnswerDAC(this.exceptionManager);
                    foreach (Answer answer in question.Answers.ToList())
                    {
                        var answerObj = Mapper.Map<Answer, AnswerDTO>(answer);
                        answerList.Add(answerObj);
                        PatientPortalEntities.Answers.DeleteObject(answer);
                    }
                }

                PatientPortalEntities.Questions.DeleteObject(question);
                PatientPortalEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while delete question.", ex);
            }
            return answerList;
        }
    }
}