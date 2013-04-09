using System;
using System.Runtime.Serialization;

namespace Aliencube.CryptoService.Exceptions
{
	public class ValueNotFoundException : ApplicationException
	{
		public ValueNotFoundException()
			: base()
		{
		}

		public ValueNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		public ValueNotFoundException(string message)
			: base(message)
		{
		}

		public ValueNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
