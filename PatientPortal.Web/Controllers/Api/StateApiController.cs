//-----------------------------------------------------------------------
// <copyright file="StateApiController.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2015.
// </copyright>
// <summary>This is the StateApiController.cs file.</summary>
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
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.IO;

namespace PatientPortal.Web
{
    public class StateApiController : BaseApiController
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public StateApiController()
        {
        }
        /// <summary>
        /// CTOR for initialize IStateFacade
        /// </summary>
        /// <param name="facade">object of type IStateFacade</param>
        public StateApiController(IStateFacade facade)
        {
            this.Facade = facade;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StateApiController"/> class.
        /// </summary>
        /// <param name="facade">Facade object.</param>
        /// <param name="exceptionManager">Exception manager.</param>
        public StateApiController(IStateFacade facade, IExceptionManager exceptionManager)
            : base(exceptionManager)
        {
            this.Facade = facade;
        }

        /// <summary>
        /// Gets or sets a facade object.
        /// </summary>
        private IStateFacade Facade { get; set; }

        /// <summary>
        /// Get state list
        /// </summary>
        /// <returns>object of type IList<IStateDTO></returns>
        [HttpGet]
        public IList<IStateDTO> GetStateList()
        {
            var surveyQuestionList = this.Facade.GetStateList();
            if (surveyQuestionList.IsValid())
            {
                return surveyQuestionList.Data;
            }
            else
            {
                return null;
            }
        }
    }
}
