using System;
using System.Runtime.Serialization;

namespace Aliencube.CryptoService.Exceptions
{
	public class VectorNotFoundException : ApplicationException
	{
		public VectorNotFoundException()
			: base()
		{
		}

		public VectorNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		public VectorNotFoundException(string message)
			: base(message)
		{
		}

		public VectorNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
