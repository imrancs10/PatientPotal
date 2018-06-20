//-----------------------------------------------------------------------
// <copyright file="TerritoryEmailApiController.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2015.
// </copyright>
// <summary>This is the TerritoryEmailApiController.cs file.</summary>
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
    public class TerritoryEmailApiController : BaseApiController
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public TerritoryEmailApiController()
        {
        }
        /// <summary>
        /// CTOR for initialize ITerritoryEmailFacade
        /// </summary>
        /// <param name="facade">object of type ITerritoryEmailFacade</param>
        public TerritoryEmailApiController(ITerritoryEmailFacade facade)
        {
            this.Facade = facade;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TerritoryEmailApiController"/> class.
        /// </summary>
        /// <param name="facade">Facade object.</param>
        /// <param name="exceptionManager">Exception manager.</param>
        public TerritoryEmailApiController(ITerritoryEmailFacade facade, IExceptionManager exceptionManager)
            : base(exceptionManager)
        {
            this.Facade = facade;
        }

        /// <summary>
        /// Gets or sets a facade object.
        /// </summary>
        private ITerritoryEmailFacade Facade { get; set; }

        /// <summary>
        /// Get Territoryn email on state wise
        /// </summary>
        /// <param name="StateId">object of type int</param>
        /// <returns>object of type IList<ITerritoriesEmailDTO></returns>
        [HttpGet]
        public IList<ITerritoriesEmailDTO> GetTerritoryEmails(int StateId)
        {
            var surveyQuestionList = this.Facade.GetTerritoryEmails(StateId);
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
