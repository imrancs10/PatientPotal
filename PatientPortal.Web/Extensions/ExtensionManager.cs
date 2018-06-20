using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatientPortal.Web
{
    /// <summary>
    /// ExtensionManager class to create entesion method
    /// </summary>
    public static class ExtensionManager
    {
        public static string ToJson(this Object obj)
        {
            JsonSerializer js = JsonSerializer.Create(new JsonSerializerSettings());
            var jw = new StringWriter();
            js.Serialize(jw, obj);
            return jw.ToString();
        }
    }
}