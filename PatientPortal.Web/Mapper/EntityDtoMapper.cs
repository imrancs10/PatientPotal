namespace PatientPortal.Web
{
    using AutoMapper;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using PatientPortal.DTOModel;
    using PatientPortal.Entities.Entities;
    using PatientPortal.Shared;

    /// <summary>
    /// Represents the entity to dto and vice versa mapper
    /// </summary>
    public static class EntityDtoMapper
    {
        /// <summary>
        /// Configures this instance.
        /// </summary>
        public static void Configure()
        {
            //map User entity to UserDTO
            Mapper.CreateMap<User, UserDTO>()
                .AfterMap((s, d) => d.RoleDTO = s.Role != null ? (new RoleDTO { Type = s.Role.Type, Id = s.Role.Id }) : null)
                .AfterMap((s, d) => d.UserDetailDTO = s.UserDetails != null && s.UserDetails.Count > 0 ? (new UserDetailDTO { Id = s.UserDetails.FirstOrDefault().Id, UserId = s.UserDetails.FirstOrDefault().UserId, BusinessName = s.UserDetails.FirstOrDefault().BusinessName, FirstName = s.UserDetails.FirstOrDefault().FirstName }) : null);

            Mapper.CreateMap<IQuestionDTO, QuestionDTO>();

            Mapper.CreateMap<IQuestionDTO, Question>();

            Mapper.CreateMap<Question, IQuestionDTO>();

            Mapper.CreateMap<Question, QuestionDTO>()
                                        .AfterMap((s, d) =>
                                            {
                                                foreach (Answer answerDTO in s.Answers)
                                                    d.AnswerDTOList.Add(new AnswerDTO { Id = answerDTO.Id, QuestionId = answerDTO.QuestionId, Title = answerDTO.Title, ToolTip = answerDTO.ToolTip, ImagePath = answerDTO.ImagePath });
                                            })
                                            .AfterMap((s, d) => d.IQuestionTypeDTO = s.QuestionType != null ? (new QuestionTypeDTO { Id = s.QuestionType.Id, Type = s.QuestionType.Type }) : null)
                                        .AfterMap((s, d) => d.ITextInputTypeDTO = (s.TextInputType != null) ? (new TextInputTypeDTO { Id = s.TextInputType.Id, InputType = s.TextInputType.InputType }) : null)
                                        .AfterMap((s, d) => d.ILanguageDTO = s.Language != null ? (new LanguageDTO { Id = s.Language.Id, Name = s.Language.Name }) : null);

            Mapper.CreateMap<IProductDTO, ProductDTO>();

            Mapper.CreateMap<IProductDTO, Product>();

            Mapper.CreateMap<IProductImagesDTO, ProductImage>();

            Mapper.CreateMap<ProductImage, ProductImagesDTO>();

            Mapper.CreateMap<Product, ProductDTO>().AfterMap((s, d) =>
            {
                foreach (ProductImage productImagesDTO in s.ProductImages)
                    d.ProductImagesDTOList.Add(new ProductImagesDTO { Id = productImagesDTO.Id, ProductId = productImagesDTO.ProductId, ThumbnailPath = productImagesDTO.ThumbnailPath, IsPrimary = productImagesDTO.IsPrimary, ImagePath = productImagesDTO.ImagePath });
            });

            Mapper.CreateMap<QuestionType, QuestionTypeDTO>();

            Mapper.CreateMap<IAnswerDTO, Answer>();

            Mapper.CreateMap<Answer, AnswerDTO>();

            Mapper.CreateMap<Answer, IAnswerDTO>();

            Mapper.CreateMap<SurveyQuestionMap, SurveyQuestionMapDTO>()
                .AfterMap((s, d) => d.IAnswerDTO = s.Answer != null ? (new AnswerDTO { Id = s.Answer.Id, Title = s.Answer.Title, ToolTip = s.Answer.ToolTip, ImagePath = s.Answer.ImagePath }) : null)
                 .AfterMap((s, d) => d.IProductDTO = s.Product != null ? (new ProductDTO { Id = s.Product.Id, Name = s.Product.Name }) : null)
                 .AfterMap((s, d) => d.IQuestionDTO = s.Question != null ? (new QuestionDTO { Id = s.Question.Id, Title = s.Question.Title, IsMandatary = s.Question.IsMandatary, LanguageId = s.Question.LanguageId, QuestionTypeId = s.Question.QuestionTypeId, TextInputTypeId = s.Question.TextInputTypeId }) : null)
                 .AfterMap((s, d) => d.ISurveyDTO = s.Survey != null ? (new SurveyDTO { Id = s.Survey.Id, Name = s.Survey.Name, IsActive = s.Survey.IsActive }) : null)
                 .AfterMap((s, d) =>  d.IQuestionDTO.IQuestionTypeDTO = s.Question != null && s.Question.QuestionType != null ? (new QuestionTypeDTO { Id = s.Question.QuestionType.Id, Type = s.Question.QuestionType.Type }) : null)
                 .AfterMap((s, d) =>
                 {
                     if (s.Question != null)
                         foreach (Answer answerDTO in s.Question.Answers)
                             d.IQuestionDTO.IAnswerDTOList.Add(new AnswerDTO { Id = answerDTO.Id, QuestionId = answerDTO.QuestionId, Title = answerDTO.Title, ToolTip = answerDTO.ToolTip, ImagePath = answerDTO.ImagePath, CreatedDate = answerDTO.CreatedDate, ModifiedDate = answerDTO.ModifiedDate });
                 });

            Mapper.CreateMap<SurveyQuestionAnswer, SurveyQuestionAnswerDTO>()
                .AfterMap((s, d) => d.IQuestionDTO = s.Question != null ? (new QuestionDTO { Id = s.Question.Id, Title = s.Question.Title, IsMandatary = s.Question.IsMandatary, LanguageId = s.Question.LanguageId, QuestionTypeId = s.Question.QuestionTypeId, TextInputTypeId = s.Question.TextInputTypeId }) : null)
                .AfterMap((s, d) => d.ISurveyDTO = s.Survey != null ? (new SurveyDTO { Id = s.Survey.Id, LanguageId = s.Survey.LanguageId, Name = s.Survey.Name, IsActive = s.Survey.IsActive }) : null)
                .AfterMap((s, d) => d.IQuestionDTO.IQuestionTypeDTO = s.Question != null && s.Question.QuestionType != null ? (new QuestionTypeDTO { Id = s.Question.QuestionType.Id, Type = s.Question.QuestionType.Type }) : null)
                             .AfterMap((s, d) =>
                             {
                                 if (s.Question != null)
                                     foreach (Answer answerDTO in s.Question.Answers)
                                         d.IQuestionDTO.IAnswerDTOList.Add(new AnswerDTO { Id = answerDTO.Id, QuestionId = answerDTO.QuestionId, Title = answerDTO.Title, ToolTip = answerDTO.ToolTip, ImagePath = answerDTO.ImagePath, CreatedDate = answerDTO.CreatedDate, ModifiedDate = answerDTO.ModifiedDate });
                             });

            Mapper.CreateMap<ISurveyDTO, SurveyDTO>();

            Mapper.CreateMap<ISurveyDTO, Survey>();

            Mapper.CreateMap<Survey, SurveyDTO>().ForMember(dest => dest.SurveyQuestionMapList, opt => opt.MapFrom(src => src.SurveyQuestionMaps)).AfterMap((s, d) => d.ILanguageDTO = (new LanguageDTO { Id = s.Language.Id, Name = s.Language.Name }));

            Mapper.CreateMap<ISurveyQuestionMapDTO, SurveyQuestionMap>();

            Mapper.CreateMap<TextInputType, TextInputTypeDTO>();

            Mapper.CreateMap<List<Product>, List<ProductDTO>>();

            Mapper.CreateMap<ISurveyQuestionAnswerDTO, SurveyQuestionAnswer>();
            Mapper.CreateMap<ISurveyQuestionAnswerDTO, SurveyQuestionAnswerDTO>();

            Mapper.CreateMap<List<State>, List<IStateDTO>>()
                        .AfterMap((s, d) =>
                            {
                                foreach (State state in s)
                                    d.Add(new StateDTO { Id = state.Id, Code = state.Code, Name = state.Name });
                            });

            Mapper.CreateMap<IUserDetailDTO, UserDetail>();

            Mapper.CreateMap<UserDetail, UserDetailDTO>();

            Mapper.CreateMap<UserDetail, IUserDetailDTO>()
                    .AfterMap((s, d) =>
                            d = (new UserDetailDTO() { BusinessName = s.BusinessName, CreatedDate = s.CreatedDate, EmailId = s.EmailId, FirstName = s.FirstName, LastName = s.LastName, PhoneNumber = s.PhoneNumber, StateId = s.StateId, UserId = s.UserId, ZipCode = s.ZipCode, Id = s.Id }));

            Mapper.CreateMap<Introduction, IntroductionDTO>();

            Mapper.CreateMap<List<TerritoriesEmail>, List<ITerritoriesEmailDTO>>()
                     .AfterMap((s, d) =>
                     {
                         foreach (TerritoriesEmail state in s)
                             d.Add(new TerritoriesEmailDTO { Id = state.Id, StateId = state.StateId, EmailId = state.EmailId.Replace("\r", "").Replace("\n", "") });
                     });


            Mapper.CreateMap<Language, LanguageDTO>();
            Mapper.CreateMap<TerritoriesEmail, TerritoriesEmailDTO>();
            Mapper.CreateMap<IRecommendedProductDTO, RecommendedProductDTO>();
            Mapper.CreateMap<IPastSurveyDTO, PastSurveyDTO>();
        }
    }


}
