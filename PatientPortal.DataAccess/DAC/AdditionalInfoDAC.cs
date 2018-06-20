//-----------------------------------------------------------------------
// <copyright file="AdditionalInfoDAC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the AdditionalInfoDAC.cs file.</summary>
//-----------------------------------------------------------------------


namespace PatientPortal.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using PatientPortal.Entities.Entities;
    using PatientPortal.Shared;
    using PatientPortal.DTOModel;
    using PatientPortal.Entities;
    using AutoMapper;
    using System.Globalization;
    /// <summary>
    /// Represents Country Data Access Component.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public class AdditionalInfoDAC : DACBase, IAdditionalInfoDAC
    {
        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdditionalInfoDAC"/> class.
        /// </summary>
        /// <param name="exceptionManager">Exception manager.</param>
        public AdditionalInfoDAC(IExceptionManager exceptionManager)
        {
            this.exceptionManager = exceptionManager;
        }

        /// <summary>
        /// save additional information
        /// </summary>
        /// <param name="userDetail">object of type UserDetailDTO</param>
        /// <returns>object of type IUserDTO</returns>  
        public IUserDTO SaveAdditionalInformation(IUserDetailDTO userDetail)
        {
            UserDetail userDetailEntity = new UserDetail();
            User userEntity = new User();
            userDetail.CreatedDate = DateTime.Now;
            try
            {
                PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();

                //Add User entry if password is passed
                if (!string.IsNullOrEmpty(userDetail.Password))
                {
                    userEntity.UserName = userDetail.EmailId;
                    userEntity.Password = userDetail.Password;
                    userEntity.RoleId = 2;
                    userEntity.IsActive = true;
                    userEntity.IsDeleted = false;
                    userEntity.CreatedDate = DateTime.Now;
                    PatientPortalEntities.AddToUsers(userEntity);
                    PatientPortalEntities.SaveChanges();
                    userDetail.UserId = userEntity.Id;
                }
                userDetailEntity = Mapper.Map<IUserDetailDTO, UserDetail>(userDetail);
                PatientPortalEntities.AddToUserDetails(userDetailEntity);
                PatientPortalEntities.SaveChanges();

            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while insert survey.", ex);
            }

            return Mapper.Map<User, UserDTO>(userEntity);
        }

        /// <summary>
        /// check user email id 
        /// </summary>
        /// <param name="emailId">object of type IUserDTO</param>
        /// <returns></returns>
        public IUserDTO CheckUserEmail(string emailId)
        {
            IUserDTO userDTO = new UserDTO();
            try
            {
                PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();
                IQueryable<User> user = PatientPortalEntities.Users.AsQueryable<User>().Where(x => x.UserName == emailId);

                userDTO = Mapper.Map<User, UserDTO>(user.FirstOrDefault());
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while fetching survey list.", ex);
            }

            return userDTO;
        }

        /// <summary>
        /// Update User Id when login
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool UpdateUserId(string guid, int userId)
        {
            try
            {
                PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();
                IQueryable<SurveyQuestionAnswer> surveyanswerEntity = PatientPortalEntities.SurveyQuestionAnswers.AsQueryable<SurveyQuestionAnswer>().Where(c => c.Guid == new Guid(guid));

                if (surveyanswerEntity != null)
                {
                    foreach (SurveyQuestionAnswer surveyAnswer in surveyanswerEntity)
                    {
                        surveyAnswer.UserId = userId;
                        surveyAnswer.ModifiedDate = DateTime.UtcNow;
                    }
                }

                PatientPortalEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while insert question.", ex);
            }

            return true;
        }

        /// <summary>
        /// Get Additional information for a user
        /// </summary>
        /// <returns>object of type IUserDetailDTO</returns>
        public IUserDetailDTO GetAdditionalInformation(int userId)
        {
            IUserDetailDTO states = new UserDetailDTO();
            try
            {
                PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();
                IQueryable<UserDetail> userDetails = PatientPortalEntities.UserDetails.Include("User").AsQueryable<UserDetail>().Where(x => x.UserId == userId);
                states = Mapper.Map<UserDetail, UserDetailDTO>(userDetails.FirstOrDefault());
                if (states != null && states.StateId > 0)
                {
                    if (states.TerritoryEmails == null)
                    {
                        states.TerritoryEmails = new List<string>();
                    }
                    IQueryable<TerritoriesEmail> territoriesEmail = PatientPortalEntities.TerritoriesEmails.Where(c => c.StateId == states.StateId);
                    if (territoriesEmail != null && territoriesEmail.Count() > 0)
                    {
                        foreach (TerritoriesEmail territoriesEmailObj in territoriesEmail)
                        {

                            var email = Mapper.Map<TerritoriesEmail, TerritoriesEmailDTO>(territoriesEmailObj);
                            states.TerritoryEmails.Add(email.EmailId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while fetching state list.", ex);
            }

            return states;
        }

    }
}