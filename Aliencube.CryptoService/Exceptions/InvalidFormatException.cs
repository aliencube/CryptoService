using System;
using System.Runtime.Serialization;

namespace Aliencube.CryptoService.Exceptions
{
	public class InvalidFormatException : ApplicationException
	{
		public InvalidFormatException()
			: base()
		{
		}

		public InvalidFormatException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		public InvalidFormatException(string message)
			: base(message)
		{
		}

		public InvalidFormatException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
