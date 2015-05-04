using System;
using System.Reflection;
using System.Security.Cryptography;
using Aliencube.CryptoService.Interfaces;
using FluentAssertions;
using NUnit.Framework;

namespace Aliencube.CryptoService.UnitTests
{
    [TestFixture]
    public class SymmetricServiceTest
    {
        private ISymmetricService _symmetricService;

        [SetUp]
        public void Init()
        {
        }

        [TearDown]
        public void Dispose()
        {
            if (this._symmetricService != null)
            {
                this._symmetricService.Dispose();
            }
        }

        [Test]
        [TestCase("aes")]
        [TestCase("des")]
        [TestCase("rc2")]
        [TestCase("rijndael")]
        [TestCase("tripledes")]
        [TestCase("other")]
        public void GivenSymmetricProvider_Should_ReturnSymmetricAlgorithm(string provider)
        {
            SymmetricProvider result;
            var symmetricProvider = Enum.TryParse(provider, true, out result) ? result : SymmetricProvider.None;

            this._symmetricService = new SymmetricService(symmetricProvider);
            var type = this._symmetricService.GetType();
            var pi = type.GetProperty("SymmetricAlgorithm", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.IgnoreCase);

            pi.Should().NotBeNull();

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

            if (symmetricProvider != SymmetricProvider.None)
            {
                algorithm.Should().NotBeNull();
            }
            else
            {
                algorithm.Should().BeNull();
            }
        }

        [Test]
        [TestCase("aes", "Aes TEST")]
        [TestCase("des", "DES TEST")]
        [TestCase("rc2", "RC2 TEST")]
        [TestCase("rijndael", "Rijndael TEST")]
        [TestCase("tripledes", "TripleDES TEST")]
        public void GivenSymmetricProvideerAndPlainText_Should_ReturnEncryptedText(string provider, string text)
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

            encrypted.Should().NotBeNullOrWhiteSpace();

            var decrypted = this._symmetricService.Decrypt(encrypted);

            decrypted.Should().NotBeNullOrWhiteSpace();
            decrypted.Should().Be(text);
        }
    }
}