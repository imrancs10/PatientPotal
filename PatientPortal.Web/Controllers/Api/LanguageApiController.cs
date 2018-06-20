//-----------------------------------------------------------------------
// <copyright file="LanguageApiController.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2015.
// </copyright>
// <summary>This is the LanguageApiController.cs file.</summary>
//-----------------------------------------------------------------------
using System.Web.Http;
using PatientPortal.Shared;
using PatientPortal.DTOModel;
using System.Web.Security;
using System.Collections.Generic;

namespace PatientPortal.Web
{
    [RoutePrefix("api/LanguageApi")]
    [Authorize]
    public class LanguageApiController : BaseApiController
    {
        #region Private Member
        /// <summary>
        /// Gets or sets a facade object.
        /// </summary>
        private ILanguageFacade Facade { get; set; }

        #endregion

        #region Ctor
        public LanguageApiController() { }

        public LanguageApiController(ILanguageFacade facade)
        {
            this.Facade = facade;
        }


        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageApiController"/> class.
        /// </summary>
        /// <param name="facade">Facade object.</param>
        /// <param name="exceptionManager">Exception manager.</param>
        public LanguageApiController(ILanguageFacade facade, IExceptionManager exceptionManager)
            : base(exceptionManager)
        {
            this.exceptionManager = exceptionManager;
            this.Facade = facade;
        }
        #endregion

        #region Public Member

        /// <summary>
        /// This method will use to get the complete language list.
        /// </summary>
        /// <returns>return language list</returns>
        public IList<ILanguageDTO> Get()
        {
            var languageList = this.Facade.GetLanguageList();
            if (languageList.IsValid())
            {
                return languageList.Data;
            }
            else
            {
                return null;
            }
        }
       
        #endregion
    }
}
