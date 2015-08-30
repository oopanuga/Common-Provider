using System;

namespace CommonProvider.Exceptions
{
    /// <summary>
    /// Represents an exception thats thrown when providers are loaded.
    /// </summary>
    public class ProviderLoadException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the ProviderLoadException class.
        /// </summary>
        public ProviderLoadException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of ProviderLoadException with a specified error message.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        public ProviderLoadException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of ProviderLoadException with a specified error message 
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        /// <param name="innerException">The cause of the current exception.</param>
        public ProviderLoadException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
