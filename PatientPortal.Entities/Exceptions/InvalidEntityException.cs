using System;

namespace Company.Project.EntityDataModel
{
    /// <summary>
    /// Represents an invalid entity exception,
    /// Author : Nagarro     
    /// </summary>
    [Serializable]
    public class InvalidEntityException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidEntityException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public InvalidEntityException(string message)
            : base(message)
        {
        }
    }
}
