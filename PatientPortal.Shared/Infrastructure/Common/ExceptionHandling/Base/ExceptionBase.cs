//-----------------------------------------------------------------------
// <copyright file="ExceptionBase.cs / BaseException" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the BaseException file.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents abstract base class for custom exceptions.
    /// </summary>
    [Serializable]
    public abstract class BaseException : Exception
    {
        /// <summary>
        /// Serialization Information Key For Error Code.
        /// </summary>
        private const string SerializationInfoKeyForErrorCode = "ErrorCode";

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseException" /> class.
        /// </summary>
        protected BaseException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseException" /> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        protected BaseException(int errorCode)
        {
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        protected BaseException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        protected BaseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseException" /> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">The
        /// <paramref name="info" />
        /// parameter is null.</exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or
        /// <see cref="P:System.Exception.HResult" />
        /// is zero (0).</exception>
        protected BaseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info != null)
            {
                this.ErrorCode = (int)info.GetValue(SerializationInfoKeyForErrorCode, typeof(int));
            }
        }

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>The error code.</value>
        public int ErrorCode { get; protected set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text message.</value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        /// <value>The exception.</value>
        public Exception Exception { get; set; }

        /// <summary>
        /// When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">The
        /// <paramref name="info" />
        /// parameter is a null reference (Nothing in Visual Basic).</exception>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            if (info != null)
            {
                info.AddValue(SerializationInfoKeyForErrorCode, this.ErrorCode);
            }
        }
    }
}