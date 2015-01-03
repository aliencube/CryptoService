﻿using System;
using System.Runtime.Serialization;

namespace Aliencube.CryptoService.Exceptions
{
    /// <summary>
    /// This represents an exception entity to be thrown when invalid format is detected.
    /// </summary>
    public class InvalidDataLengthException : ApplicationException
    {
        /// <summary>
        /// Initialises a new instance of the InvalidDataLengthException class.
        /// </summary>
        public InvalidDataLengthException()
            : base()
        {
        }

        /// <summary>
        /// Initialises a new instance of the InvalidDataLengthException class.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        public InvalidDataLengthException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Initialises a new instance of the InvalidDataLengthException class.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        public InvalidDataLengthException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initialises a new instance of the InvalidDataLengthException class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception. If the innerException parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
        public InvalidDataLengthException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}