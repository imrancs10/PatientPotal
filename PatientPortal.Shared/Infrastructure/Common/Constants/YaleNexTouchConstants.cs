//-----------------------------------------------------------------------
// <copyright file="AppConstants.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the AppConstants file.</summary>
//-----------------------------------------------------------------------
namespace PatientPortal.Shared
{
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Contains application level constants.
    /// </summary>
    public static class PatientPortalConstants
    {
        /// <summary>
        /// Contains configuration keys constants.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Justification = "Nested types should not be visible")]
        [SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes", Justification = "Done intentially.")]
        public struct ConfigurationKeys
        {
            /// <summary>
            /// Constant representing the assemblies' output bin folder name.
            /// </summary>
            public static readonly string OutputBinFolderName = ConfigurationManager.AppSettings["OutputBinFolderName"];

            /// <summary>
            /// Constant for SMTP server
            /// </summary>
            public static readonly string smtpServer = ConfigurationManager.AppSettings["SMTPServer"];

            /// <summary>
            /// Constant for SMTP server from email id
            /// </summary>
            public static readonly string fromEmailId = ConfigurationManager.AppSettings["FromEmailId"];

            /// <summary>
            /// Constant for SMTP server for sender mail name
            /// </summary>
            public static readonly string fromName = ConfigurationManager.AppSettings["FromName"];

            /// <summary>
            /// Constant for SMTP server password
            /// </summary>
            public static readonly string password = ConfigurationManager.AppSettings["Password"];

            /// <summary>
            /// Constant for SMTP server port
            /// </summary>
            public static readonly int port = System.Convert.ToInt16(ConfigurationManager.AppSettings["Port"].ToString());

            /// <summary>
            /// Reset password subject
            /// </summary>
            public static readonly string reSetPasswordSubject = ConfigurationManager.AppSettings["ResetPasswordSubject"];

            /// <summary>
            /// BaseHref subject
            /// </summary>
            public static readonly string baseHref = ConfigurationManager.AppSettings["BaseHref"];

            /// <summary>
            /// Security Advisor Mail Subject
            /// </summary>
            public static readonly string securityAdvisorMailSubject = ConfigurationManager.AppSettings["SecurityAdvisorMailSubject"];

            /// <summary>
            /// Default language for application
            /// </summary>
            public static readonly string defaultLanguage = ConfigurationManager.AppSettings["DefaultLanguage"];

            /// <summary>
            /// Encryption Key
            /// </summary>
            public static readonly string encryptionKey = ConfigurationManager.AppSettings["EncryptionKey"];

            /// <summary>
            /// PDF File Foler Key
            /// </summary>
            public static readonly string pdfFileFoler = ConfigurationManager.AppSettings["PDFFileFoler"];

            /// <summary>
            /// To address for mail in case of survey result
            /// </summary>
            public static readonly string surveyResultTo = ConfigurationManager.AppSettings["SurveyResultTo"];

            /// <summary>
            /// Subject of user survey result
            /// </summary>
            public static readonly string surveyResultSubject = ConfigurationManager.AppSettings["SurveyResultSubject"];

            /// <summary>
            /// Set custom error flag
            /// </summary>
            public static readonly string infoLog = ConfigurationManager.AppSettings["InfoLog"];

            /// <summary>
            /// Set debug log flag
            /// </summary>
            public static readonly string debugLog = ConfigurationManager.AppSettings["DebugLog"];

            /// <summary>
            /// Set error log flag
            /// </summary>
            public static readonly string errorLog = ConfigurationManager.AppSettings["ErrorLog"];

            /// <summary>
            /// Set error log flag
            /// </summary>
            public static readonly string warningLog = ConfigurationManager.AppSettings["WarningLog"];
        }

        /// <summary>
        /// Format constants.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes", Justification = "Done intentially.")]
        [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Justification = "Done intentially.")]
        public struct FormatConstants
        {
            /// <summary>
            /// The date time format in database.
            /// </summary>
            public const string DBDateTimeFormat = "yyyy-MM-ddThh:mm:ssZ";

            /// <summary>
            /// The date format in database.
            /// </summary>
            public const string DBDateFormat = "yyyy-MM-dd";

            /// <summary>
            /// The date time format for application.
            /// </summary>
            public const string UIDateTimeFormat = "dd-MM-yyyy HH:mm:ss";

            /// <summary>
            /// The UI date format.
            /// </summary>
            public const string UIDateFormat = "dd-MM-yyyy";

            /// <summary>
            /// The date picker format.
            /// </summary>
            public const string JQueryDatePickerFormat = "dd-mm-yy";

            /// <summary>
            /// Visible character max length.
            /// </summary>
            public const int VisibleCharLength = 20;

            /// <summary>
            /// Character max length.
            /// </summary>
            public const int CharLength = 5;
        }

        /// <summary>
        /// Contains constants to represents available containers in Unity.config.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes", Justification = "Done intentially.")]
        [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Justification = "Done intentially.")]
        public struct UnityContainers
        {
            /// <summary>
            /// Main unity Container.
            /// </summary>
            public const string MainContainer = "MainContainer";

            /// <summary>
            /// Test unity container.
            /// </summary>
            public const string TestContainer = "TestContainer";
        }

        public struct ApplicationKeys
        {
            /// <summary>
            /// ErrorCode.
            /// </summary>
            public const string ErrorCode = "ErrorCode";

            /// <summary>
            /// Results for text.
            /// </summary>
            public const string ResultsFor = " - Results for ";

            /// <summary>
            /// UnderScore.
            /// </summary>
            public const string Underscore = "_";

            /// <summary>
            /// Hypen.
            /// </summary>
            public const string Hypen = "-";

            /// <summary>
            /// SingleSpace.
            /// </summary>
            public const string SingleSpace = " ";

            /// <summary>
            /// Backslash.
            /// </summary>
            public const string Backslash = "/";

            /// <summary>
            /// Create.
            /// </summary>
            public const string Create = "Create";

            /// <summary>
            /// Update.
            /// </summary>
            public const string Update = "Update";

            // <summary>
            /// Failed.
            /// </summary>
            public const string Failed = "Failed";

            // <summary>
            /// Success.
            /// </summary>
            public const string Success = "SUCCESS";

            /// <summary>
            /// Failed.
            /// </summary>
            public const string Load = "Load";

            /// <summary>
            /// CreateFailed.
            /// </summary>
            public const string CreateFailed = "Create Failed";

            /// <summary>
            /// UpdateFailed.
            /// </summary>
            public const string UpdateFailed = "Update Failed";

            /// <summary>
            /// LoadFailed.
            /// </summary>
            public const string LoadFailed = "Load Failed";
        }

        public struct Messages
        {
            /// <summary>
            /// File not found.
            /// </summary>
            public const string FileNotExisted = " does not found";

            /// <summary>
            /// Recommendation list null.
            /// </summary>
            public const string NullRecommendationList = " recommendation list is null";
        }

        public struct FilePath
        {
            /// <summary>
            /// PDF File Path.
            /// </summary>
            public const string PDFFileFolder = "~/Images/PDFFiles/";

            /// <summary>
            /// Product Folder Path.
            /// </summary>
            public const string ProductImages = "~/Images/ProductImages/";

            /// <summary>
            /// Product Folder Path.
            /// </summary>
            public const string ProductThumbnails = "~/Images/ProductImages/ProductThumbnails/";

            /// <summary>
            /// Product Folder Path.
            /// </summary>
            public const string AnswerImages = "~/Images/AnswerImages/";

            /// <summary>
            /// Survey result body template for PDF File Path.
            /// </summary>
            public const string SurveyResultBodyTemplate = "~/Views/Shared/SurveyResultBodyTemplate.html";

            /// <summary>
            /// Advisor for PDF File Path.
            /// </summary>
            public const string AdvisorEmailTemplate = "~/Views/Shared/AdvisorEmailTemplate.html";

            /// <summary>
            /// Forget password body template for PDF File Path.
            /// </summary>
            public const string ForgetPasswordTemplate = "~/Views/Shared/ForgetPassword.html";
        }

        public struct FileName
        {
            /// <summary>
            /// Recommended product pdf file name.
            /// </summary>
            public const string RecommendedProduct = "Yale Multi-Family Housing Product Recommendation-";

            /// <summary>
            /// Survey Result pdf file name.
            /// </summary>
            public const string SurveyResult = "_SurveyResult";
        }
    }
}