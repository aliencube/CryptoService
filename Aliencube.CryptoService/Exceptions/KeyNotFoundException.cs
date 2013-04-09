using System;
using System.Runtime.Serialization;

namespace Aliencube.CryptoService.Exceptions
{
	public class KeyNotFoundException : ApplicationException
	{
		public KeyNotFoundException()
			: base()
		{
		}

		public KeyNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		public KeyNotFoundException(string message)
			: base(message)
		{
		}

		public KeyNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
