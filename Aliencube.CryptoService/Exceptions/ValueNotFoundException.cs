using System;
using System.Runtime.Serialization;

namespace Aliencube.CryptoService.Exceptions
{
    /// <summary>
    /// This represents an exception entity to be thrown when a value is not found.
    /// </summary>
    public class ValueNotFoundException : ApplicationException
    {
        /// <summary>
        /// Initialises a new instance of the ValueNotFoundException class.
        /// </summary>
        public ValueNotFoundException()
            : base()
        {
        }

        /// <summary>
        /// Initialises a new instance of the ValueNotFoundException class.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        public ValueNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Initialises a new instance of the ValueNotFoundException class.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        public ValueNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initialises a new instance of the ValueNotFoundException class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception. If the innerException parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
        public ValueNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}