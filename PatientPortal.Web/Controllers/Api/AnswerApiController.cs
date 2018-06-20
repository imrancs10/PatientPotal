//-----------------------------------------------------------------------
// <copyright file="AnswerApiController.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2015.
// </copyright>
// <summary>This is the AnswerApiController.cs file.</summary>
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
    [RoutePrefix("api/AnswerApi")]
    [Authorize]
    public class AnswerApiController : BaseApiController
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public AnswerApiController()
        {

        }

        public AnswerApiController(IAnswerFacade facade)
        {
            this.Facade = facade;
        }

        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnswerApiController"/> class.
        /// </summary>
        /// <param name="facade">Facade object.</param>
        /// <param name="exceptionManager">Exception manager.</param>
        public AnswerApiController(IAnswerFacade facade, IExceptionManager exceptionManager)
            : base(exceptionManager)
        {
            this.exceptionManager = exceptionManager;
            this.Facade = facade;
        }

        /// <summary>
        /// Gets or sets a facade object.
        /// </summary>
        private IAnswerFacade Facade { get; set; }

        /// <summary>
        /// This method will use to delete the answer.
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public bool Delete(int id)
        {
            OperationResult<IAnswerDTO> answerObj = Utility.GetResultData(Utility.GetResultData(this.Facade.DeleteAnswerById(id)));
            if (answerObj != null && answerObj.Data != null)
            {
                this.DeleteAnswerImages(answerObj);
            }
            return true;
        }

        /// <summary>
        /// Delete images of answer
        /// </summary>
        /// <param name="answerDTO">Answer Object</param>
        [NonAction]
        public void DeleteAnswerImages(OperationResult<IAnswerDTO> answerDTO)
        {
            try
            {
                if (!string.IsNullOrEmpty(answerDTO.Data.ImagePath))
                {
                    System.IO.FileInfo file = new System.IO.FileInfo(System.Web.HttpContext.Current.Server.MapPath("~/" + answerDTO.Data.ImagePath));
                    if (file.Exists)
                    {
                        file.Delete();
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
