//-----------------------------------------------------------------------
// <copyright file="IntroductionApiController.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2015.
// </copyright>
// <summary>This is the IntroductionApiController.cs file.</summary>
//-----------------------------------------------------------------------
using System.Web.Http;
using PatientPortal.Shared;
using PatientPortal.DTOModel;
using System.Web.Security;
using System.Collections.Generic;

namespace PatientPortal.Web
{
    [RoutePrefix("api/IntroductionApi")]
    public class IntroductionApiController : BaseApiController
    {
        #region Private Member
        /// <summary>
        /// Gets or sets a facade object.
        /// </summary>
        private IIntroductionFacade Facade { get; set; }

        #endregion

        #region Constructor
        public IntroductionApiController() { }

        public IntroductionApiController(IIntroductionFacade facade)
        {
            this.Facade = facade;
        }


        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntroductionApiController"/> class.
        /// </summary>
        /// <param name="facade">Facade object.</param>
        /// <param name="exceptionManager">Exception manager.</param>
        public IntroductionApiController(IIntroductionFacade facade, IExceptionManager exceptionManager)
            : base(exceptionManager)
        {
            this.exceptionManager = exceptionManager;
            this.Facade = facade;
        }
        #endregion

        #region Public Member

        /// <summary>
        /// This method will use to get a particular introduction based on language id
        /// </summary>
        /// <returns>return object of introduction DTO</returns>
        [HttpGet]
        public IIntroductionDTO GetIntroductionByLanguageId(string id)
        {
            var descryptId = Utility.Decrypt(id).Split('/')[1];
            var introductionDTO = this.Facade.GetIntroductionByLanguageId(System.Convert.ToInt16(descryptId));
            return Utility.GetResultData(introductionDTO.Data as IIntroductionDTO);
        }
       
        #endregion
    }
}
