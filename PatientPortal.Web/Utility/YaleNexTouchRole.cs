using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace PatientPortal.Web
{
    public class PatientPortalRole : RoleProvider
    {

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get role from login page after user authenticated
        /// </summary>
        /// <param name="roleName">The role name from DB which is added to Data Provider</param>
        /// <returns>return an array of roles</returns>
        public override string[] GetRolesForUser(string roleName)
        {
            string[] roles = { "Admin" };
            roles[0] = roleName;
            return roles;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}