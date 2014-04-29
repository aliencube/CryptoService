using Aliencube.CryptoService.Interfaces;
using NUnit.Framework;
using System;
using System.Reflection;
using System.Security.Cryptography;

namespace Aliencube.CryptoService.UnitTests
{
    [TestFixture]
    public class HashServiceTest
    {
        #region SetUp / TearDown

        private IHashService _hashService;

        [SetUp]
        public void Init()
        {
        }

        [TearDown]
        public void Dispose()
        {
            this._hashService.Dispose();
        }

        #endregion SetUp / TearDown

        #region Tests

        [Test]
        [TestCase("md5", true)]
        [TestCase("sha1", true)]
        [TestCase("sha256", true)]
        [TestCase("sha384", true)]
        [TestCase("sha512", true)]
        [TestCase("other", false)]
        public void GetHashAlgorithm_GivenHashProvider_ReturnHashAlgorithm(string provider, bool expected)
        {
            this._hashService = new HashService(provider);
            var type = this._hashService.GetType();
            var pi = type.GetProperty("HashAlgorithm", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.IgnoreCase);
            Assert.IsNotNull(pi);

            HashAlgorithm algorithm = null;
            switch (provider)
            {
                case "md5":
                    algorithm = pi.GetValue(this._hashService) as MD5CryptoServiceProvider;
                    break;

                case "sha1":
                    algorithm = pi.GetValue(this._hashService) as SHA1CryptoServiceProvider;
                    break;

                case "sha256":
                    algorithm = pi.GetValue(this._hashService) as SHA256CryptoServiceProvider;
                    break;

                case "sha384":
                    algorithm = pi.GetValue(this._hashService) as SHA384CryptoServiceProvider;
                    break;

                case "sha512":
                    algorithm = pi.GetValue(this._hashService) as SHA512CryptoServiceProvider;
                    break;
            }
            Assert.AreEqual(expected, algorithm != null);
        }

        [Test]
        [TestCase(8)]
        [TestCase(20)]
        [TestCase(32)]
        public void GetRandomString_GivenLength_ReturnRandomString(int length)
        {
            this._hashService = new HashService("sha256");
            var result = this._hashService.GenerateHash(length);

            Assert.IsTrue(!String.IsNullOrWhiteSpace(result));
            Assert.AreEqual(length, result.Length);
        }

        [Test]
        [TestCase("test1")]
        [TestCase("1234567890")]
        public void GetHashedText_GivenPlainText_ReturnHashedText(string text)
        {
            this._hashService = new HashService("sha256");
            var result = this._hashService.Hash(text);
            var validated = this._hashService.ValidateHashedStrings(result, text);

            Assert.IsTrue(!String.IsNullOrWhiteSpace(result));
            Assert.IsTrue(validated);
        }

        #endregion Tests
    }
}