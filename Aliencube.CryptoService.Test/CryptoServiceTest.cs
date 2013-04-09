using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aliencube.CryptoService.Test
{
	[TestClass]
	public class CryptoServiceTest
	{
		[TestMethod]
		public void Encrypt_ValueEncrypted()
		{
			var key = "12345678901234567890123456789012";
			var text = "Hello World!";

			var service = new CryptoService(key);
			var encrypted = service.Encrypt(text);
			Assert.IsTrue(!String.IsNullOrWhiteSpace(encrypted));
		}

		[TestMethod]
		public void Decrypt_ValueDecrypted()
		{
			var key = "12345678901234567890123456789012";
			var text = "Hello World!";

			var service = new CryptoService(key);
			var encrypted = service.Encrypt(text);
			var decrypted = service.Decrypt(encrypted);
			Assert.IsTrue(text == decrypted);
		}
	}
}
