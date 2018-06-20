using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientPortal.Global
{
    public static class Enums
    {
        public enum LoginMessage
        {
            Authenticated=1,
            InvalidCreadential=2,
            LoginFailed,
            UserDeleted,
            UserInactive,
            UserBlocked,
            NoResponse
        }
    }
}