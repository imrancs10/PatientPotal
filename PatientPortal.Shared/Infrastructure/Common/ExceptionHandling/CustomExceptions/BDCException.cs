//-----------------------------------------------------------------------
// <copyright file="BDCException.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2013.
// </copyright>
// <summary>This is the BDCException class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents Business Domain Component exception.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "BDC",
        Justification = "Represents BDC exception")]
    [Serializable]
    public class BDCException : BaseException
    {
        /// <summary>
        /// Holds the header text to be used prefixed in error messages.
        /// </summary>
        private const string MessageHeader = "BDC Exception: ";

        /// <summary>
        /// Initializes a new instance of the <see cref="BDCException" /> class.
        /// </summary>
        public BDCException()
            : base(MessageHeader, null)
        {
            // this.DefaultExceptionPolicyType = ExceptionPolicyType.Basic;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BDCException" /> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        public BDCException(int errorCode)
        {
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BDCException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public BDCException(string message)
            : base(MessageHeader + message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BDCException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public BDCException(string message, Exception innerException)
            : base(MessageHeader + message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BDCException" /> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">The
        /// <paramref name="info" />
        /// parameter is null.</exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or
        /// <see cref="P:System.Exception.HResult" />
        /// is zero (0).</exception>
        protected BDCException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}