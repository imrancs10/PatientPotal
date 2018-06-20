using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientPortal.Web
{
    /// <summary>
    /// Constants for PatientPortal Web.
    /// </summary>
    public enum EnumMessage
    {
        /// <summary>
        /// Success Message
        /// </summary>
        Success = 1001,

        /// <summary>
        /// Error Message
        /// </summary>
        Error = 1002,

        /// <summary>
        /// Invalid Node delete for Tree View
        /// </summary>
        InvalidNodeDelete = 1003,

        /// <summary>
        /// Invalid Node drag and drop for Tree View
        /// </summary>
        InvalidNodeDragDrop = 1004
    }
}