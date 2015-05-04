using System;
using System.Reflection;
using System.Security.Cryptography;
using Aliencube.CryptoService.Interfaces;
using FluentAssertions;
using NUnit.Framework;

namespace Aliencube.CryptoService.UnitTests
{
    [TestFixture]
    public class HashServiceTest
    {
        private IHashService _hashService;

        [SetUp]
        public void Init()
        {
        }

        [TearDown]
        public void Dispose()
        {
            if (this._hashService != null)
            {
                this._hashService.Dispose();
            }
        }

        [Test]
        [TestCase("md5")]
        [TestCase("sha1")]
        [TestCase("sha256")]
        [TestCase("sha384")]
        [TestCase("sha512")]
        [TestCase("other")]
        public void GivenHashProvider_Should_ReturnHashAlgorithm(string provider)
        {
            HashProvider result;
            var hashProvider = Enum.TryParse(provider, true, out result) ? result : HashProvider.None;

            this._hashService = new HashService(hashProvider);

            var type = this._hashService.GetType();
            var pi = type.GetProperty("HashAlgorithm", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.IgnoreCase);
            pi.Should().NotBeNull();

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

            if (hashProvider != HashProvider.None)
            {
                algorithm.Should().NotBeNull();
            }
            else
            {
                algorithm.Should().BeNull();
            }
        }

        [Test]
        [TestCase("md5", 16)]
        [TestCase("sha1", 16)]
        [TestCase("sha256", 24)]
        [TestCase("sha384", 24)]
        [TestCase("sha512", 32)]
        public void GivenHashProviderAndLength_Should_ReturnRandomString(string provider, int length)
        {
            this._hashService = new HashService(provider);
            var result = this._hashService.GenerateHash(length);

            result.Should().NotBeNullOrWhiteSpace();
            result.Length.Should().Be(length);
        }

        [Test]
        [TestCase("md5", "Hello World")]
        [TestCase("sha1", "Hello World")]
        [TestCase("sha256", "Hello World")]
        [TestCase("sha384", "Hello World")]
        [TestCase("sha512", "Hello World")]
        public void GivenHashProviderAndPlainText_Should_ReturnHashedText(string provider, string text)
        {
            this._hashService = new HashService(provider);
            var result = this._hashService.Hash(text);

            result.Should().NotBeNullOrWhiteSpace();

            var validated = this._hashService.ValidateHash(result, text);

            validated.Should().BeTrue();
        }
    }
}