using AutoMapper;
using Microsoft.Practices.Unity;
//-----------------------------------------------------------------------
// <copyright file="Registration.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2015.
// </copyright>
// <summary>This is the Registration class.</summary>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientPortal.Web
{
    public class Registration
    {

        protected IUnityContainer container;

        public Registration(IUnityContainer unitycontainer)
        {
            if (unitycontainer == null)
            {
                throw new ArgumentNullException("container");
            }

            container = unitycontainer;

        }

        public IUnityContainer RegisterObjects()
        {
            // User
            container.RegisterType<PatientPortal.Shared.IUserDTO, PatientPortal.DTOModel.UserDTO>();
            container.RegisterType<PatientPortal.Shared.IUserFacade, PatientPortal.Facade.UserFacade>();
            container.RegisterType<PatientPortal.Shared.IUserBDC, PatientPortal.Business.UserBDC>();
            container.RegisterType<PatientPortal.Shared.IUserDAC, PatientPortal.DataAccess.UserDAC>();

            // Survey
            container.RegisterType<PatientPortal.Shared.ISurveyDTO, PatientPortal.DTOModel.SurveyDTO>();
            container.RegisterType<PatientPortal.Shared.ISurveyFacade, PatientPortal.Facade.SurveyFacade>();
            container.RegisterType<PatientPortal.Shared.ISurveyBDC, PatientPortal.Business.SurveyBDC>();
            container.RegisterType<PatientPortal.Shared.ISurveyDAC, PatientPortal.DataAccess.SurveyDAC>();

            // Survey User
            container.RegisterType<PatientPortal.Shared.ISurveyQuestionAnswerDTO, PatientPortal.DTOModel.SurveyQuestionAnswerDTO>();
            container.RegisterType<PatientPortal.Shared.ISurveyUserFacade, PatientPortal.Facade.SurveyUserFacade>();
            container.RegisterType<PatientPortal.Shared.ISurveyUserBDC, PatientPortal.Business.SurveyUserBDC>();
            container.RegisterType<PatientPortal.Shared.ISurveyUserDAC, PatientPortal.DataAccess.SurveyUserDAC>();

            // Recommendation
            container.RegisterType<PatientPortal.Shared.IRecommendationFacade, PatientPortal.Facade.RecommendationFacade>();
            container.RegisterType<PatientPortal.Shared.IRecommendationBDC, PatientPortal.Business.RecommendationBDC>();
            container.RegisterType<PatientPortal.Shared.IRecommendationDAC, PatientPortal.DataAccess.RecommendationDAC>();

            // PastSurvey
            container.RegisterType<PatientPortal.Shared.IPastSurveyFacade, PatientPortal.Facade.PastSurveyFacade>();
            container.RegisterType<PatientPortal.Shared.IPastSurveyBDC, PatientPortal.Business.PastSurveyBDC>();
            container.RegisterType<PatientPortal.Shared.IPastSurveyDAC, PatientPortal.DataAccess.PastSurveyDAC>();

            // State
            container.RegisterType<PatientPortal.Shared.IStateFacade, PatientPortal.Facade.StateFacade>();
            container.RegisterType<PatientPortal.Shared.IStateBDC, PatientPortal.Business.StateBDC>();
            container.RegisterType<PatientPortal.Shared.IStateDAC, PatientPortal.DataAccess.StateDAC>();

            // Additional Info
            container.RegisterType<PatientPortal.Shared.IAdditionalInfoFacade, PatientPortal.Facade.AdditionalInfoFacade>();
            container.RegisterType<PatientPortal.Shared.IAdditionalInfoBDC, PatientPortal.Business.AdditionalInfoBDC>();
            container.RegisterType<PatientPortal.Shared.IAdditionalInfoDAC, PatientPortal.DataAccess.AdditionalInfoDAC>();

            // Territory Email
            container.RegisterType<PatientPortal.Shared.ITerritoryEmailFacade, PatientPortal.Facade.TerritoryEmailFacade>();
            container.RegisterType<PatientPortal.Shared.ITerritoryEmailBDC, PatientPortal.Business.TerritoryEmailBDC>();
            container.RegisterType<PatientPortal.Shared.ITerritoryEmailDAC, PatientPortal.DataAccess.TerritoryEmailDAC>();

            // Question
            container.RegisterType<PatientPortal.Shared.IQuestionDTO, PatientPortal.DTOModel.QuestionDTO>();
            container.RegisterType<PatientPortal.Shared.IQuestionFacade, PatientPortal.Facade.QuestionFacade>();
            container.RegisterType<PatientPortal.Shared.IQuestionBDC, PatientPortal.Business.QuestionBDC>();
            container.RegisterType<PatientPortal.Shared.IQuestionDAC, PatientPortal.DataAccess.QuestionDAC>();

            // Answer
            container.RegisterType<PatientPortal.Shared.IAnswerDTO, PatientPortal.DTOModel.AnswerDTO>();
            container.RegisterType<PatientPortal.Shared.IAnswerFacade, PatientPortal.Facade.AnswerFacade>();
            container.RegisterType<PatientPortal.Shared.IAnswerBDC, PatientPortal.Business.AnswerBDC>();
            container.RegisterType<PatientPortal.Shared.IAnswerDAC, PatientPortal.DataAccess.AnswerDAC>();

            // Question Type
            container.RegisterType<PatientPortal.Shared.IQuestionTypeDTO, PatientPortal.DTOModel.QuestionTypeDTO>();
            container.RegisterType<PatientPortal.Shared.IQuestionTypeFacade, PatientPortal.Facade.QuestionTypeFacade>();
            container.RegisterType<PatientPortal.Shared.IQuestionTypeBDC, PatientPortal.Business.QuestionTypeBDC>();
            container.RegisterType<PatientPortal.Shared.IQuestionTypeDAC, PatientPortal.DataAccess.QuestionTypeDAC>();

            // Language
            container.RegisterType<PatientPortal.Shared.ILanguageDTO, PatientPortal.DTOModel.LanguageDTO>();
            container.RegisterType<PatientPortal.Shared.ILanguageFacade, PatientPortal.Facade.LanguageFacade>();
            container.RegisterType<PatientPortal.Shared.ILanguageBDC, PatientPortal.Business.LanguageBDC>();
            container.RegisterType<PatientPortal.Shared.ILanguageDAC, PatientPortal.DataAccess.LanguageDAC>();

            // Product
            container.RegisterType<PatientPortal.Shared.IProductDTO, PatientPortal.DTOModel.ProductDTO>();
            container.RegisterType<PatientPortal.Shared.IProductFacade, PatientPortal.Facade.ProductFacade>();
            container.RegisterType<PatientPortal.Shared.IProductBDC, PatientPortal.Business.ProductBDC>();
            container.RegisterType<PatientPortal.Shared.IProductDAC, PatientPortal.DataAccess.ProductDAC>();

            // Product Images
            container.RegisterType<PatientPortal.Shared.IProductImagesDTO, PatientPortal.DTOModel.ProductImagesDTO>();
            container.RegisterType<PatientPortal.Shared.IProductImagesFacade, PatientPortal.Facade.ProductImagesFacade>();
            container.RegisterType<PatientPortal.Shared.IProductImagesBDC, PatientPortal.Business.ProductImagesBDC>();
            container.RegisterType<PatientPortal.Shared.IProductImagesDAC, PatientPortal.DataAccess.ProductImagesDAC>();

            // TextInputType
            container.RegisterType<PatientPortal.Shared.ITextInputTypeDTO, PatientPortal.DTOModel.TextInputTypeDTO>();
            container.RegisterType<PatientPortal.Shared.ITextInputTypeFacade, PatientPortal.Facade.TextInputTypeFacade>();
            container.RegisterType<PatientPortal.Shared.ITextInputTypeBDC, PatientPortal.Business.TextInputTypeBDC>();
            container.RegisterType<PatientPortal.Shared.ITextInputTypeDAC, PatientPortal.DataAccess.TextInputTypeDAC>();

            // Introduction
            container.RegisterType<PatientPortal.Shared.IIntroductionDTO, PatientPortal.DTOModel.IntroductionDTO>();
            container.RegisterType<PatientPortal.Shared.IIntroductionFacade, PatientPortal.Facade.IntroductionFacade>();
            container.RegisterType<PatientPortal.Shared.IIntroductionBDC, PatientPortal.Business.IntroductionBDC>();
            container.RegisterType<PatientPortal.Shared.IIntroductionDAC, PatientPortal.DataAccess.IntroductionDAC>();

            // SurveyQuestionMap
            container.RegisterType<PatientPortal.Shared.ISurveyQuestionMapDAC, PatientPortal.DataAccess.SurveyQuestionMapDAC>();

            // PatientPortalEntities
            container.RegisterType<PatientPortal.Entities.Entities.PatientPortalEntities, PatientPortal.Entities.Entities.PatientPortalEntities>();

            // IExceptionManager
            container.RegisterType<PatientPortal.Shared.IExceptionManager, PatientPortal.Shared.ExceptionManager>();

            // ILogger
            container.RegisterType<PatientPortal.Shared.Infrastructure.Common.Logging.ILogger, PatientPortal.Plugin.Log4net.Log4netLogger>();

            return container;

        }

    }
}