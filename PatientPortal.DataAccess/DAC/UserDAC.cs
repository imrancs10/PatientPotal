//-----------------------------------------------------------------------
// <copyright file="UserDAC.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the UserDAC.cs file.</summary>
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
    /// <summary>
    /// Represents Country Data Access Component.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "Done intentially.")]
    public class UserDAC : DACBase, IUserDAC
    {
        /// <summary>
        /// The exception manager.
        /// </summary>
        private readonly IExceptionManager exceptionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDAC"/> class.
        /// </summary>
        /// <param name="exceptionManager">Exception manager.</param>
        public UserDAC(IExceptionManager exceptionManager)
        {
            this.exceptionManager = exceptionManager;
        }

        /// <summary>
        /// Gets the department list.
        /// </summary>
        /// <returns>List of customers.</returns>
        public IList<IUserDTO> GetAll()
        {
            IList<IUserDTO> userList = new List<IUserDTO>();
            try
            {
                PatientPortalEntities testEntities = new PatientPortalEntities();
                IQueryable<User> users = testEntities.Users.AsQueryable<User>();
                foreach (User userObj in users)
                {
                    IUserDTO userDTO = new UserDTO();
                    userDTO = Mapper.Map<User, UserDTO>(userObj);
                    userList.Add(userDTO);
                }
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while fetching customer list.", ex);
            }

            return userList;
        }

        /// <summary>
        /// Gets the department by id.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>User information.</returns>
        public IUserDTO GetById(int id)
        {
            IUserDTO userDTO = new UserDTO();

            try
            {
                PatientPortalEntities testEntities = new PatientPortalEntities();
                User user = testEntities.Users.AsQueryable<User>().Where(c => c.Id == id).FirstOrDefault();

                userDTO = Mapper.Map<User, UserDTO>(user); 
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while fetching user list.", ex);
            }

            return userDTO;
        }

        /// <summary>
        /// Add a user.
        /// </summary>
        /// <param name="user">User data.</param>
        /// <returns>Flag indicating if the operation succeed.</returns>
        public bool Add(IUserDTO user)
        {
            User userEntity = new User();

            try
            {
                PatientPortalEntities testEntities = new PatientPortalEntities();
                EntityConverter.FillEntityFromDto(user, userEntity);

                testEntities.AddToUsers(userEntity);
                testEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while adding user.", ex);
            }

            return true;
        }

        /// <summary>
        /// Update a user.
        /// </summary>
        /// <param name="user">User data.</param>
        /// <returns>Flag indicating if the operation succeed.</returns>
        public bool UpdateUser(IUserDTO user)
        {
            user.ModifiedDate = DateTime.UtcNow;
            PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();
            User userObj = PatientPortalEntities.Users.AsQueryable<User>().Where(c => c.UserName == user.UserName).FirstOrDefault();
            if (userObj.Id > 0)
            {
                userObj.Password = user.Password;
            }

            PatientPortalEntities.SaveChanges();

            return true;
        }

        /// <summary>
        /// Gets the authentic user.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>User information.</returns>
        public IUserDTO CheckAuthenticUser(IUserDTO userDTO)
        {
            IUserDTO userObj = null;
            try
            {
                PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();
                IQueryable<User> users = PatientPortalEntities.Users.Include("Role").Include("UserDetails").AsQueryable<User>();
                users = users.Where(user => user.UserName == userDTO.UserName && user.Password == userDTO.Password && user.IsActive == true && user.IsDeleted == false);
                if (users.Count() > 0)
                {
                    userObj = Mapper.Map<User, UserDTO>(users.FirstOrDefault());
                }
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while fetching user.", ex);
            }
            return userObj;
        }

        /// <summary>
        /// To get authenticated user to send email for password change request.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>User information.</returns>
        public IUserDTO ForgetPassword(IUserDTO userDTO)
        {
            IUserDTO userObj = null;
            try
            {
                PatientPortalEntities PatientPortalEntities = new PatientPortalEntities();
                IQueryable<User> users = PatientPortalEntities.Users.Include("UserDetails").AsQueryable<User>();
                users = users.Where(user => user.UserName == userDTO.UserName && user.IsActive == true && user.IsDeleted == false);
                if (users.Count() > 0)
                {
                    userObj = Mapper.Map<User, UserDTO>(users.FirstOrDefault());
                }
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while fetching user.", ex);
            }

            return userObj;
        }

        public bool UpdateUserId(string guid, int userId)
        {
            try
            {
                AdditionalInfoDAC additionalInfoDAC = new AdditionalInfoDAC(exceptionManager);
                additionalInfoDAC.UpdateUserId(guid, userId);
            }
            catch (Exception ex)
            {
                this.exceptionManager.HandleException(ex, ex.Message);
                throw new DACException("An exception occurred while insert question.", ex);
            }

            return true;
        }
    }
}