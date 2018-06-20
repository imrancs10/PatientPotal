//-----------------------------------------------------------------------
// <copyright file="SurveyApiController.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2015.
// </copyright>
// <summary>This is the SurveyApiController.cs file.</summary>
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

namespace PatientPortal.Web
{
    [Authorize]
    [RoutePrefix("api/SurveyApi")]
    public class SurveyApiController : BaseApiController
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public SurveyApiController()
        {

        }

        public SurveyApiController(ISurveyFacade facade)
        {
            this.Facade = facade;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SurveyApiController"/> class.
        /// </summary>
        /// <param name="facade">Facade object.</param>
        /// <param name="exceptionManager">Exception manager.</param>
        public SurveyApiController(ISurveyFacade facade, IExceptionManager exceptionManager)
            : base(exceptionManager)
        {
            this.Facade = facade;
        }

        /// <summary>
        /// Gets or sets a facade object.
        /// </summary>
        private ISurveyFacade Facade { get; set; }

        /// <summary>
        /// This method will use to get the complete survey list.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IList<ISurveyDTO> Get()
        {
            string baseURL = string.Empty;
            baseURL = PatientPortalConstants.ConfigurationKeys.baseHref;
            var surveyList = this.Facade.GetSurveyList();
            foreach (ISurveyDTO survey in surveyList.Data)
            {
                survey.EncryptSurveyIdLanguageId = baseURL + "/surveyuser/" + this.EncryptSurveyIdLanguageId(survey.Id, survey.LanguageId);
            }
            return Utility.GetResultData(surveyList.Data as IList<ISurveyDTO>);
        }

        /// <summary>
        /// Get survey by id
        /// </summary>
        /// <param name="id">survey Id</param>
        /// <returns>Survey Object</returns>
        [HttpGet]
        public ISurveyDTO Get(int id)
        {
            var survey = this.Facade.GetSurveyById(id);
            if (survey.Data != null)
            {
                survey.Data.EncryptSurveyIdLanguageId = this.EncryptSurveyIdLanguageId(survey.Data.Id, survey.Data.LanguageId);
            }
            return Utility.GetResultData(survey.Data as ISurveyDTO);
        }

        /// <summary>
        /// This method will use to post the survey.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public OperationResult<ISurveyDTO> Post(SurveyDTO surveyDTO)
        {
            ISurveyDTO iSurveyDTO = new SurveyDTO();
            iSurveyDTO = Mapper.Map<ISurveyDTO, SurveyDTO>(surveyDTO);
            foreach (SurveyQuestionMapDTO surveyQuestionMapDTO in surveyDTO.SurveyQuestionMapList.ToList().OrderBy(c => c.QuestionOrderNumber))
            {
                iSurveyDTO.ISurveyQuestionMapList.Add(surveyQuestionMapDTO);
            }
            OperationResult<ISurveyDTO> surveyDTOObj = Utility.GetResultData(this.Facade.InsertSurvey(iSurveyDTO));
            if (surveyDTOObj.IsValid())
            {
                Logger.LogInfo(CustomLogger.SurveySuccessUpdate(surveyDTOObj.Data, PatientPortalConstants.ApplicationKeys.Create));
            }
            else
            {
                Logger.LogInfo(CustomLogger.SurveyFailed(PatientPortalConstants.ApplicationKeys.CreateFailed));
            }
            return surveyDTOObj;
        }

        /// <summary>
        /// This method will use to update the survey.
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public OperationResult<ISurveyDTO> Put(SurveyDTO surveyDTO)
        {
            ISurveyDTO iSurveyDTO = new SurveyDTO();
            iSurveyDTO = Mapper.Map<ISurveyDTO, SurveyDTO>(surveyDTO);
            foreach (SurveyQuestionMapDTO surveyQuestionMapDTO in surveyDTO.SurveyQuestionMapList.ToList().OrderBy(c => c.QuestionOrderNumber))
            {
                iSurveyDTO.ISurveyQuestionMapList.Add(surveyQuestionMapDTO);
            }
            OperationResult<ISurveyDTO> surveyDTOObj = Utility.GetResultData(this.Facade.UpdateSurvey(iSurveyDTO));
            if (surveyDTOObj.ResultType == OperationResultType.Success)
            {
                Logger.LogInfo(CustomLogger.SurveySuccessUpdate(surveyDTO, PatientPortalConstants.ApplicationKeys.Create));
            }
            else
            {
                Logger.LogInfo(CustomLogger.SurveyFailed(PatientPortalConstants.ApplicationKeys.UpdateFailed));
            }
            return surveyDTOObj;
        }

        /// <summary>
        /// This method will use to delete the survey.
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public OperationResult<bool> Delete(int id)
        {
            return Utility.GetResultData(this.Facade.DeleteSurveyById(id));
        }

        public string EncryptSurveyIdLanguageId(int surveyId, int languageId)
        {
            return (Utility.Encrypt(surveyId + PatientPortalConstants.ApplicationKeys.Backslash + languageId));
        }
    }
}
