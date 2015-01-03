using System;
using System.Reflection;
using System.Security.Cryptography;
using Aliencube.CryptoService.Interfaces;
using NUnit.Framework;

namespace Aliencube.CryptoService.UnitTests
{
    [TestFixture]
    public class SymmetricServiceTest
    {
        #region SetUp / TearDown

        private ISymmetricService _symmetricService;

        [SetUp]
        public void Init()
        {
        }

        [TearDown]
        public void Dispose()
        {
            this._symmetricService.Dispose();
        }

        #endregion SetUp / TearDown

        #region Tests

        [Test]
        [TestCase("aes", true)]
        [TestCase("des", true)]
        [TestCase("rc2", true)]
        [TestCase("rijndael", true)]
        [TestCase("tripledes", true)]
        [TestCase("other", false)]
        public void GetSymmetricAlgorithm_GivenSymmetricProvider_ReturnSymmetricAlgorithm(string provider, bool expected)
        {
            this._symmetricService = new SymmetricService(provider);
            var type = this._symmetricService.GetType();
            var pi = type.GetProperty("SymmetricAlgorithm", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.IgnoreCase);
            Assert.IsNotNull(pi);

            SymmetricAlgorithm algorithm = null;
            switch (provider)
            {
                case "aes":
                    algorithm = pi.GetValue(this._symmetricService) as AesCryptoServiceProvider;
                    break;

                case "des":
                    algorithm = pi.GetValue(this._symmetricService) as DESCryptoServiceProvider;
                    break;

                case "rc2":
                    algorithm = pi.GetValue(this._symmetricService) as RC2CryptoServiceProvider;
                    break;

                case "rijndael":
                    algorithm = pi.GetValue(this._symmetricService) as RijndaelManaged;
                    break;

                case "tripledes":
                    algorithm = pi.GetValue(this._symmetricService) as TripleDESCryptoServiceProvider;
                    break;
            }
            Assert.AreEqual(expected, algorithm != null);
        }

        [Test]
        [TestCase("aes", "Aes TEST")]
        [TestCase("des", "DES TEST")]
        [TestCase("rc2", "RC2 TEST")]
        [TestCase("rijndael", "Rijndael TEST")]
        [TestCase("tripledes", "TripleDES TEST")]
        public void GetEncryptedText_GivenPlainText_ReturnEncryptedText(string provider, string text)
        {
            var hash = new HashService(HashProvider.SHA256);
            this._symmetricService = new SymmetricService(provider);
            switch (provider)
            {
                case "aes":
                    this._symmetricService.Key = hash.GenerateHash(32);
                    this._symmetricService.Vector = hash.GenerateHash(16);
                    break;

                case "des":
                    this._symmetricService.Key = hash.GenerateHash(8);
                    this._symmetricService.Vector = hash.GenerateHash(8);
                    break;

                case "rc2":
                    this._symmetricService.Key = hash.GenerateHash(16);
                    this._symmetricService.Vector = hash.GenerateHash(8);
                    break;

                case "rijndael":
                    this._symmetricService.Key = hash.GenerateHash(32);
                    this._symmetricService.Vector = hash.GenerateHash(16);
                    break;

                case "tripledes":
                    this._symmetricService.Key = hash.GenerateHash(24);
                    this._symmetricService.Vector = hash.GenerateHash(8);
                    break;
            }
            var encrypted = this._symmetricService.Encrypt(text);

            Assert.IsTrue(!String.IsNullOrWhiteSpace(encrypted));

            var decrypted = this._symmetricService.Decrypt(encrypted);

            Assert.IsTrue(!String.IsNullOrWhiteSpace(decrypted));
            Assert.AreEqual(text, decrypted);
        }

        #endregion Tests
    }
}