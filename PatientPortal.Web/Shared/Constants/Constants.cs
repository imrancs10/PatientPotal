using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YaleNexTouch.Web
{
    /// <summary>
    /// Constants for YaleNexTouch Web.
    /// </summary>
    public static class Constants
    {
        #region ViewModelsPath
        /// <summary>
        /// knockout ViewModels paths
        /// </summary>
        public struct ViewModelsPath
        {
            public const string UserViewModel = "~/Scripts/Api/Admin/UserViewModel";
        } 
        #endregion

        #region Bundles
        /// <summary>
        /// MVC bundles
        /// </summary>
        public struct Bundles
        {
            public const string AdminScripts = "~/YaleNexTouch/AdminScripts";
        } 
        #endregion

        #region ModuleIds
        /// <summary>
        /// knockout ViewModels Require Js Ids
        /// </summary>
        public struct ModuleIds
        {
            public const string UserViewModel = "~/Scripts/Api/Admin/UserViewModel";
        } 
        #endregion
    }
}