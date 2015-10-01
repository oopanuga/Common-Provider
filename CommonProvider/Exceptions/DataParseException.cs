using System;

namespace CommonProvider.Exceptions
{
    /// <summary>
    /// Represents an exception thats thrown during a data parse operation.
    /// </summary>
    public class DataParseException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the DataParseException class.
        /// </summary>
        public DataParseException()
        {
        }

        /// <summary>
        /// Initializes a new instance of DataParseException with a specified error message.
        /// </summary>
        /// <param name="message">A message that describes the error</param>
        public DataParseException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of DataParseException with a specified error message 
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">A message that describes the error</param>
        /// <param name="innerException">The cause of the current exception</param>
        public DataParseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
