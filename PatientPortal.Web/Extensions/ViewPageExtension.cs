using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace PatientPortal.Web.Utilities
{
    /// <summary>
    /// ViewPageExtensions Extension
    /// </summary>
    public static class ViewPageExtensions
    {
        private const string _JsModel = "  js{0}={1};";

        /// <summary>
        /// Write script block
        /// </summary>
        /// <param name="webPage"></param>
        /// <returns></returns>
        public static void GeneratedJSModel<T>(this WebViewPage webPage)
        {
            var type = typeof(T);
            var json = string.Format(_JsModel, type.Name, JsonConvert.SerializeObject(Activator.CreateInstance(type)));
            webPage.WriteLiteral(json);
        }

        /// <summary>
        /// Render the script on the page
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        public static IHtmlString RenderScript(this UrlHelper helper, params string[] paths)
        {
            string scripts = System.Web.Optimization.Scripts.Render(paths).ToHtmlString();
            string hostName = HttpContext.Current.Request.Url.Scheme + Uri.SchemeDelimiter + HttpContext.Current.Request.Url.Authority;
            string replaced = Regex.Replace(scripts, "src=\"/", "src=\"" + hostName + "/", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            return new HtmlString(replaced);
        }


        /// <summary>
        /// Render the style on the page
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        public static IHtmlString RenderStyle(this UrlHelper helper, params string[] paths)
        {
            string styles = System.Web.Optimization.Styles.Render(paths).ToHtmlString();
            string hostName = HttpContext.Current.Request.Url.Scheme + Uri.SchemeDelimiter + HttpContext.Current.Request.Url.Authority;
            string replaced = Regex.Replace(styles, "href=\"/", "href=\"" + hostName + "/", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            return new HtmlString(replaced);
        }



        /// <summary>
        /// Renders the view model.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="viewModelPath">The view model path.</param>
        /// <param name="bundle">The bundle.</param>
        /// <param name="moduleId">The module identifier.</param>
        /// <returns></returns>
        public static IHtmlString RenderViewModel(this UrlHelper helper, string viewModelPath, string bundle, string moduleId)
        {
            return new HtmlString(string.Format("<script data-module-id=\"{0}\" data-bundle=\"{1}\" data-view-model=\"{2}\"></script>",
                moduleId, bundle.Replace("~/", ""), viewModelPath.Replace("~/", "")));
        }

        /// <summary>
        /// Renders the require js application.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="appJsPath">The application js path.</param>
        /// <param name="bundle">The bundle.</param>
        /// <param name="requireJsPath">The require js path.</param>
        /// <returns></returns>
        public static IHtmlString RenderRequireJsApp(this UrlHelper helper, string appJsPath, string bundle, string requireJsPath)
        {
            //return new HtmlString(string.Format("<script data-main=\"{0}\" src=\"{1}\" ></script>",
            //    ConfigurationSettings.EnableOptimization ? bundle.Replace("~", "") + "?noext=1" : appJsPath, requireJsPath));

            return new HtmlString(string.Format("<script data-main=\"{0}\" src=\"{1}\" ></script>", appJsPath, requireJsPath));
        }

        
        public static MvcHtmlString RenderControl(this HtmlHelper helper, string Text, object htmlAttributes, string controlType)
        
        {
            controlType = EditorType.GetControlType(controlType);
            if (controlType == EditorType.SingleSelect || controlType == EditorType.MultiSelect
                || controlType == EditorType.TextBox)
            {
                TagBuilder controlTag = new TagBuilder("input");
                controlTag.MergeAttribute("type", controlType);
                return SetAtrributes(Text, htmlAttributes, controlTag);
            }
            else
            {
                TagBuilder controlTag = new TagBuilder(controlType);
                return SetAtrributes(Text, htmlAttributes, controlTag);
            }
        }

        private static MvcHtmlString SetAtrributes(string Text, object htmlAttributes, TagBuilder controlTag)
        {
            MvcHtmlString htmlString;
            controlTag.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            controlTag.SetInnerText(Text);
            if (controlTag.TagName.ToLower() == "input" && (controlTag.Attributes["type"].ToLower() == "radio" || controlTag.Attributes["type"].ToLower() == "checkbox"))
            {
                controlTag.MergeAttribute("name", "surveyAnswer");
                TagBuilder spanTag = new TagBuilder("span");
                spanTag.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
                htmlString = MvcHtmlString.Create(controlTag.ToString(TagRenderMode.Normal) + spanTag.ToString(TagRenderMode.Normal));
            }
            else
            {
                htmlString = MvcHtmlString.Create(controlTag.ToString(TagRenderMode.Normal));

            }
            return htmlString;
        }

        public static class EditorType
        {
            public const string SingleSelect = "radio";
            public const string MultiSelect = "checkbox";
            public const string TextBox = "textbox";
            public const string TextArea = "textarea";
            public const string NumberSlider = "NumberSlider";
            public static string GetControlType(string questiontype)
            {
                questiontype = questiontype.Replace("-", "");
                switch (questiontype)
                {
                    case "SingleSelect":
                        return SingleSelect;
                        break;
                    case "MultiSelect":
                        return MultiSelect;
                        break;
                    case "TextBox":
                        return TextBox;
                        break;
                    case "TextArea":
                        return TextArea;
                        break;
                    case "NumberSlider":
                        return NumberSlider;
                        break;
                    default:
                        return TextBox;
                        break;
                }
            }

        }


    }
}
