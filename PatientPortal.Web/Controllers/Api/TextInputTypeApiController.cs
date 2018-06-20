//-----------------------------------------------------------------------
// <copyright file="TextInputTypeApiController.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2015.
// </copyright>
// <summary>This is the TextInputTypeApiController.cs file.</summary>
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
    [RoutePrefix("api/TextInputTypeApi")]
    [Authorize]
    public class TextInputTypeApiController : BaseApiController
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public TextInputTypeApiController()
        {

        }

        public TextInputTypeApiController(ITextInputTypeFacade facade)
        {
            this.Facade = facade;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextInputTypeApiController"/> class.
        /// </summary>
        /// <param name="facade">Facade object.</param>
        /// <param name="exceptionManager">Exception manager.</param>
        public TextInputTypeApiController(ITextInputTypeFacade facade, IExceptionManager exceptionManager)
            : base(exceptionManager)
        {
            this.Facade = facade;
        }

        /// <summary>
        /// Gets or sets a facade object.
        /// </summary>
        private ITextInputTypeFacade Facade { get; set; }

        /// <summary>
        /// This method will use to get the all type of input type for questions.
        /// </summary>
        /// <returns>Text input type list</returns>
        public IList<ITextInputTypeDTO> Get()
        {
            var questionTypeList = this.Facade.GetTextInputTypeList();
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
