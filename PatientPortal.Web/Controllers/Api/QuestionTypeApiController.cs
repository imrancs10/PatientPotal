//-----------------------------------------------------------------------
// <copyright file="QuestionTypeApiController.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2015.
// </copyright>
// <summary>This is the QuestionTypeApiController.cs file.</summary>
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
    [RoutePrefix("api/QuestionTypeApi")]
    [Authorize]
    public class QuestionTypeApiController : BaseApiController
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public QuestionTypeApiController()
        {

        }

        public QuestionTypeApiController(IQuestionTypeFacade facade)
        {
            this.Facade = facade;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionTypeApiController"/> class.
        /// </summary>
        /// <param name="facade">Facade object.</param>
        /// <param name="exceptionManager">Exception manager.</param>
        public QuestionTypeApiController(IQuestionTypeFacade facade, IExceptionManager exceptionManager)
            : base(exceptionManager)
        {
            this.Facade = facade;
        }

        /// <summary>
        /// Gets or sets a facade object.
        /// </summary>
        private IQuestionTypeFacade Facade { get; set; }

        /// <summary>
        /// This method will use to get the complete question type list.
        /// </summary>
        /// <returns>Question type list</returns>
        public IList<IQuestionTypeDTO> Get()
        {
            var questionTypeList = this.Facade.GetQuestionTypeList();
            if (questionTypeList.IsValid())
            {
                return questionTypeList.Data;
            }
            else
            {
                return null;
            }
        }
    }
}
