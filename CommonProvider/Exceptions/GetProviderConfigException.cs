using System;

namespace CommonProvider.Exceptions
{
    /// <summary>
    /// Represents an exception thats thrown when getting ProviderConfig.
    /// </summary>
    public class GetProviderConfigException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the GetProviderConfigException class.
        /// </summary>
        public GetProviderConfigException()
        {
        }

        /// <summary>
        /// Initializes a new instance of GetProviderConfigException with a specified error message.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        public GetProviderConfigException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of GetProviderConfigException with a specified error message 
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        /// <param name="innerException">The cause of the current exception.</param>
        public GetProviderConfigException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
