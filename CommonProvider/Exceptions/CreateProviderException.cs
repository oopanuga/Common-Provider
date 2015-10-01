using System;

namespace CommonProvider.Exceptions
{
    /// <summary>
    /// Represents an exception thats thrown during the creation of a provider.
    /// </summary>
    public class CreateProviderException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the CreateProviderException class.
        /// </summary>
        public CreateProviderException()
        {
        }

        /// <summary>
        /// Initializes a new instance of CreateProviderException with a specified error message.
        /// </summary>
        /// <param name="message">A message that describes the error</param>
        public CreateProviderException(string message): base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of CreateProviderException with a specified error message 
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">A message that describes the error</param>
        /// <param name="innerException">The cause of the current exception</param>
        public CreateProviderException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
