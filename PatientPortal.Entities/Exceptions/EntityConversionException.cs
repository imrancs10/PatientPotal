//-----------------------------------------------------------------------
// <copyright file="EntityConversionException.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the EntityConversionException.cs file.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Entities
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;
    using System.Threading;
    using PatientPortal.Shared;    

    /// <summary>
    /// Represents Entity conversion exception.
    /// </summary>
    [Serializable]
    public class EntityConversionException : BaseException
    {
        /// <summary>
        /// Message header.
        /// </summary>
        private const string MessageHeader = "Entity Conversion Exception: ";

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityConversionException"/> class.
        /// </summary>
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", Justification = "For now the formater is not set")]
        public EntityConversionException()
            : base(MessageHeader, null)
        {
            // this.DefaultExceptionPolicyType = ExceptionPolicyType.Basic;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityConversionException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", Justification = "For now the formater is not set")]
        public EntityConversionException(int errorCode)
        {
            this.ErrorCode = errorCode;
        }

        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", Justification = "For now the formater is not set")]

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityConversionException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public EntityConversionException(string message)
            : base(MessageHeader + message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityConversionException"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="type">The type of the exception.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", Justification = "Formater is not set")]
        public EntityConversionException(string propertyName, string type)
            : base(MessageHeader + string.Format(Thread.CurrentThread.CurrentCulture, "Property {0} not found in type {1}", propertyName, type))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityConversionException"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="type">The entity conversion exception type.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", Justification = "For now the formater is not set")]
        public EntityConversionException(string propertyName, object type)
            : this(propertyName, type == null ? string.Empty : type.ToString())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityConversionException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", Justification = "No need to format the string as per culture")]
        public EntityConversionException(string message, Exception innerException)
            : base(MessageHeader + message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityConversionException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info"/> parameter is null. </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0). </exception>
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", Justification = "For now the formater is not set")]
        protected EntityConversionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}