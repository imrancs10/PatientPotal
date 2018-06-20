//-----------------------------------------------------------------------
// <copyright file="QuestionApiController.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2015.
// </copyright>
// <summary>This is the QuestionApiController.cs file.</summary>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PatientPortal.Shared;
using PatientPortal.DTOModel;

namespace PatientPortal.Web
{
    [Authorize]
    [RoutePrefix("api/QuestionApi")]
    public class QuestionApiController : BaseApiController
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public QuestionApiController()
        {

        }

        public QuestionApiController(IQuestionFacade facade)
        {
            this.Facade = facade;
        }

        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionApiController"/> class.
        /// </summary>
        /// <param name="facade">Facade object.</param>
        /// <param name="exceptionManager">Exception manager.</param>
        public QuestionApiController(IQuestionFacade facade, IExceptionManager exceptionManager)
            : base(exceptionManager)
        {
            this.exceptionManager = exceptionManager;
            this.Facade = facade;
        }

        /// <summary>
        /// Gets or sets a facade object.
        /// </summary>
        private IQuestionFacade Facade { get; set; }

        /// <summary>
        /// This method will use to get the complete question list.
        /// </summary>
        /// <returns>Question list</returns>
        [HttpGet]
        public IList<IQuestionDTO> Get()
        {
            var questionList = this.Facade.GetQuestionList();
            return Utility.GetResultData(questionList.Data as IList<IQuestionDTO>);
        }

        /// <summary>
        /// This method will use to get the particular question by passing its ID.
        /// </summary>
        /// <param name="id">Question id</param>
        /// <returns>Question object</returns>
        [HttpGet]
        public IQuestionDTO Get(int id)
        {
            var question = this.Facade.GetQuestionById(id);
            return Utility.GetResultData(question.Data as IQuestionDTO);
        }


        /// <summary>
        /// This method will use to get the particular question by passing its ID.
        /// </summary>
        /// <param name="id">Question id</param>
        /// <returns>Question object</returns>
        [HttpGet]
        public IList<IQuestionDTO> GetSurveyQuestionList(int id)
        {
            var questionList = this.Facade.GetSurveyQuestionList(id);
            return Utility.GetResultData(questionList.Data as IList<IQuestionDTO>);
        }
        /// <summary>
        /// This method will use to post the question.
        /// </summary>
        /// <returns>Return updated object</returns>
        [HttpPost]
        public OperationResult<IQuestionDTO> Post(QuestionDTO questionDTO)
        {
            IQuestionDTO iQuestionDTO = new QuestionDTO();
            iQuestionDTO = Mapper.Map<IQuestionDTO, QuestionDTO>(questionDTO);

            foreach (AnswerDTO answerDTO in questionDTO.AnswerDTOList)
            {
                iQuestionDTO.IAnswerDTOList.Add(answerDTO);
            }

            OperationResult<IQuestionDTO> questionDTOObj = Utility.GetResultData(this.Facade.InsertQuestion(iQuestionDTO));

            if (questionDTOObj.IsValid())
            {
                Logger.LogInfo(CustomLogger.QuestionSuccessUpdate(questionDTOObj.Data, PatientPortalConstants.ApplicationKeys.Create));
            }
            else
            {
                Logger.LogInfo(CustomLogger.QuestionFailed(PatientPortalConstants.ApplicationKeys.CreateFailed));
            }

            return questionDTOObj;
        }

        /// <summary>
        /// This method will use to update the question.
        /// </summary>
        /// <returns>Return updated object</returns>
        [HttpPut]
        public bool Put(QuestionDTO questionDTO)
        {
            IQuestionDTO iQuestionDTO = new QuestionDTO();
            iQuestionDTO = Mapper.Map<IQuestionDTO, QuestionDTO>(questionDTO);
            foreach (AnswerDTO answerDTO in questionDTO.AnswerDTOList)
            {
                iQuestionDTO.IAnswerDTOList.Add(answerDTO);
            }
            OperationResult<IQuestionDTO> questionDTOObj = Utility.GetResultData(this.Facade.UpdateQuestion(iQuestionDTO));
            if (questionDTOObj.IsValid())
            {
                Logger.LogInfo(CustomLogger.QuestionSuccessUpdate(questionDTOObj.Data, PatientPortalConstants.ApplicationKeys.Update));
            }
            else
            {
                Logger.LogInfo(CustomLogger.QuestionFailed(PatientPortalConstants.ApplicationKeys.UpdateFailed));
            }

            DeleteAnswerImages(questionDTOObj);
            return true;
        }

        [NonAction]
        private void DeleteAnswerImages(OperationResult<IQuestionDTO> questionDTOObj)
        {
            try
            {
                if (questionDTOObj != null && questionDTOObj.Data != null && questionDTOObj.Data.IAnswerDTOList.Count() > 0)
                {
                    this.DeleteAnswerImages(questionDTOObj.Data.IAnswerDTOList);
                }
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
            }
        }

        /// <summary>
        /// This method will use to delete the question.
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public bool Delete(int id)
        {
            try
            {
                OperationResult<IList<IAnswerDTO>> answerList = Utility.GetResultData(this.Facade.DeleteQuestionById(id));
                if (answerList != null && answerList.Data != null && answerList.Data.Count() > 0)
                {
                    this.DeleteAnswerImages(answerList);
                }
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
            }
            return true;
        }

        /// <summary>
        /// Delete images of answers
        /// </summary>
        /// <param name="answerList">List of answers</param>
        [NonAction]
        public void DeleteAnswerImages(IList<IAnswerDTO> answerList)
        {
            try
            {
                foreach (IAnswerDTO answerDTO in answerList)
                {
                    if (!string.IsNullOrEmpty(answerDTO.ImagePath))
                    {
                        System.IO.FileInfo file = new System.IO.FileInfo(System.Web.HttpContext.Current.Server.MapPath("~/" + answerDTO.ImagePath));
                        if (file.Exists)
                        {
                            file.Delete();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
            }
        }

        /// <summary>
        /// Delete images of answers
        /// </summary>
        /// <param name="answerList">List of answers</param>
        [NonAction]
        public void DeleteAnswerImages(OperationResult<IList<IAnswerDTO>> answerList)
        {
            try
            {
                foreach (IAnswerDTO answerDTO in answerList.Data)
                {
                    if (!string.IsNullOrEmpty(answerDTO.ImagePath))
                    {
                        System.IO.FileInfo file = new System.IO.FileInfo(System.Web.HttpContext.Current.Server.MapPath("~/" + answerDTO.ImagePath));
                        if (file.Exists)
                        {
                            file.Delete();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
            }
        }

        /// <summary>
        /// Save answer image
        /// </summary>
        public void SaveAnswerImage()
        {
            string allFileNames = string.Empty;
            string virtualFolder = PatientPortalConstants.FilePath.AnswerImages, physicalFolder = string.Empty, fileName = string.Empty;

            try
            {
                var httpRequest = System.Web.HttpContext.Current.Request;
                System.Web.HttpFileCollection myFileCollection = System.Web.HttpContext.Current.Request.Files;

                if (myFileCollection != null)
                {
                    for (int i = 0; i < myFileCollection.Count; i++)
                    {
                        System.Web.HttpPostedFile file = myFileCollection[i];
                        physicalFolder = System.Web.HttpContext.Current.Server.MapPath(virtualFolder);
                        fileName = myFileCollection.Keys[i];
                        file.SaveAs(physicalFolder + fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
            }
        }
    }
}
