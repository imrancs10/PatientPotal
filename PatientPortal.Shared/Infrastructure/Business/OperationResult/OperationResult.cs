//-----------------------------------------------------------------------
// <copyright file="OperationResult.cs" company="Company" author="Nagarro">
//  All rights reserved. Copyright (c) 2016.
// </copyright>
// <summary>This is the OperationResult class.</summary>
//-----------------------------------------------------------------------

namespace PatientPortal.Shared
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Represents a result of an business operation (method call).
    /// </summary>
    /// <typeparam name="T">Represent the actual return type of the operation/method.</typeparam>
    [Serializable]
    public sealed class OperationResult<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationResult{T}" /> class.
        /// Initializes a new instance of the <see cref="OperationResult&lt;T&gt;" /> class.
        /// </summary>
        /// <param name="data">The data for operation.</param>
        /// <param name="resultType">Type of the result.</param>
        /// <param name="message">The message.</param>
        /// <param name="errorCode">The error code.</param>
        /// <param name="stackTrace">The stack trace.</param>
        private OperationResult(
            T data, OperationResultType resultType, string message, int errorCode, string stackTrace)
        {
            this.Data = data;
            this.ResultType = resultType;
            this.Message = message;
            this.ErrorCode = errorCode;
            this.StackTrace = stackTrace;
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>The data value.</value>
        public T Data { get; private set; }

        /// <summary>
        /// Gets the error code.
        /// </summary>
        /// <value>The error code.</value>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; private set; }

        /// <summary>
        /// Gets the type of the result.
        /// </summary>
        /// <value>The type of the result.</value>
        public OperationResultType ResultType { get; private set; }

        /// <summary>
        /// Gets the stack trace.
        /// </summary>
        /// <value>The stack trace.</value>
        public string StackTrace { get; private set; }

        /// <summary>
        /// Creates the error result.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="stackTrace">The stack trace.</param>
        /// <returns>Operation Result.</returns>
        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes",
            Justification = "Do Not Declare Static Members On Generic Types")]
        public static OperationResult<T> CreateErrorResult(string errorMessage, string stackTrace)
        {
            return new OperationResult<T>(default(T), OperationResultType.Error, errorMessage, default(int), stackTrace);
        }

        /// <summary>
        /// Creates the error result.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="stackTrace">The stack trace.</param>
        /// <returns>Operation Result.</returns>
        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes",
            Justification = "Do Not Declare Static Members On Generic Types")]
        public static OperationResult<T> CreateErrorResult(int errorCode, string stackTrace)
        {
            return new OperationResult<T>(default(T), OperationResultType.Error, string.Empty, errorCode, stackTrace);
        }

        /// <summary>
        /// Creates the error result.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="message">The message.</param>
        /// <param name="stackTrace">The stack trace.</param>
        /// <returns>Operation Result.</returns>
        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes",
           Justification = "Do Not Declare Static Members On Generic Types")]
        public static OperationResult<T> CreateErrorResult(int errorCode, string message, string stackTrace)
        {
            return new OperationResult<T>(default(T), OperationResultType.Error, message, errorCode, stackTrace);
        }

        /// <summary>
        /// Creates the failure result.
        /// </summary>
        /// <param name="failureMessage">The failure message.</param>
        /// <returns>Operation Result.</returns>
        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes",
            Justification = "Do Not Declare Static Members On Generic Types")]
        public static OperationResult<T> CreateFailureResult(string failureMessage)
        {
            return new OperationResult<T>(
                default(T), OperationResultType.Failure, failureMessage, default(int), string.Empty);
        }

        /// <summary>
        /// Creates the failure result.
        /// </summary>
        /// <param name="validationErrorCode">The validation error code.</param>
        /// <returns>Operation Result.</returns>
        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes",
            Justification = "Do Not Declare Static Members On Generic Types")]
        public static OperationResult<T> CreateFailureResult(int validationErrorCode)
        {
            return new OperationResult<T>(
                default(T), OperationResultType.Failure, string.Empty, validationErrorCode, string.Empty);
        }

        /// <summary>
        /// Creates the success result.
        /// </summary>
        /// <param name="data">The data for operation.</param>
        /// <returns>Operation Result.</returns>
        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes",
            Justification = "Do Not Declare Static Members On Generic Types")]
        public static OperationResult<T> CreateSuccessResult(T data)
        {
            return new OperationResult<T>(data, OperationResultType.Success, string.Empty, default(int), string.Empty);
        }

        /// <summary>
        /// Creates the success result.
        /// </summary>
        /// <param name="data">The data for operation.</param>
        /// <returns>Operation Result.</returns>
        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes",
            Justification = "Do Not Declare Static Members On Generic Types")]
        public static OperationResult<T> CreateSuccessResult(bool isSucess, string sucessMessage)
        { 
            return new OperationResult<T>(default(T), OperationResultType.Success, sucessMessage, default(int), string.Empty);
        }

        /// <summary>
        /// Creates the success result.
        /// </summary>
        /// <param name="data">The data for operation.</param>
        /// <param name="successMessage">The success message.</param>
        /// <returns>Operation Result.</returns>
        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes",
            Justification = "Do Not Declare Static Members On Generic Types")]
        public static OperationResult<T> CreateSuccessResult(T data, string successMessage)
        {
            return new OperationResult<T>(data, OperationResultType.Success, successMessage, default(int), string.Empty);
        }

        /// <summary>
        /// Determines whether exception has occurred.
        /// </summary>
        /// <returns><c>true</c> if [has exception occurred]; otherwise, <c>false</c> .</returns>
        public bool HasExceptionOccurred()
        {
            bool result = false;

            if (this.ResultType == OperationResultType.Error)
            {
                if (this.ErrorCode != default(int) || !string.IsNullOrWhiteSpace(this.Message))
                {
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// Determines whether validation has failed.
        /// </summary>
        /// <returns><c>true</c> if [has validation failed]; otherwise, <c>false</c> .</returns>
        public bool HasValidationFailed()
        {
            bool result = false;

            if (this.ResultType == OperationResultType.Failure)
            {
                if (this.ErrorCode != default(int) || !string.IsNullOrWhiteSpace(this.Message))
                {
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// Determines whether this instance is valid.
        /// </summary>
        /// <returns><c>true</c> if this instance is valid; otherwise, <c>false</c> .</returns>
        public bool IsValid()
        {
            bool result = true;

            if (this.Data == null || this.HasValidationFailed() || this.HasExceptionOccurred())
            {
                result = false;
            }

            return result;
        }
    }
}