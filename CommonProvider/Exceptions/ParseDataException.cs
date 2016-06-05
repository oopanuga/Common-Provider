using System;

namespace CommonProvider.Exceptions
{
    /// <summary>
    /// Represents an exception thats thrown during a data parse operation.
    /// </summary>
    public class ParseDataException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the ParseDataException class.
        /// </summary>
        public ParseDataException()
        {
        }

        /// <summary>
        /// Initializes a new instance of ParseDataException with a specified error message.
        /// </summary>
        /// <param name="message">A message that describes the error</param>
        public ParseDataException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of ParseDataException with a specified error message 
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">A message that describes the error</param>
        /// <param name="innerException">The cause of the current exception</param>
        public ParseDataException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
