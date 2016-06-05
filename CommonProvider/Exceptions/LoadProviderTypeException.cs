using System;

namespace CommonProvider.Exceptions
{
    /// <summary>
    /// Represents an exception thats thrown when loading provider types from an assembly.
    /// </summary>
    public class LoadProviderTypeException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the LoadProviderTypeException class.
        /// </summary>
        public LoadProviderTypeException()
        {
        }

        /// <summary>
        /// Initializes a new instance of LoadProviderTypeException with a specified error message.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        public LoadProviderTypeException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of LoadProviderTypeException with a specified error message 
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        /// <param name="innerException">The cause of the current exception.</param>
        public LoadProviderTypeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
