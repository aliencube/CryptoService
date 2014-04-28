using System;
using System.IO;
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

		[TestMethod]
		public void GenerateRandomValue_Generated()
		{
			var sb = new StringBuilder();
			var seed = DateTime.Now.Ticks;
			for (var i = 0; i < 10; i++)
				sb.AppendLine(CryptoService.GenerateRandomCode(seed + i));

			var filename = "codes.txt";
			File.WriteAllText(filename, sb.ToString());
			Assert.IsTrue(File.Exists(filename));
		}
	}
}
