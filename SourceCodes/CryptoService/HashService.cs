using Aliencube.CryptoService.Interfaces;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Aliencube.CryptoService
{
    /// <summary>
    /// This represents a hash service entity that provides the one-way encryption service for text.
    /// </summary>
    public class HashService : IHashService
    {
        private const string KEY_LOWER = "abcdefghijklmnopqrstuvwxyz";
        private const string KEY_UPPER = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string KEY_NUMBER = "0123456789";
        private const string KEY_SPECIAL = "`~!@#$%^&*()-_=+[{]}\\|;:'\",<.>/?";

        private readonly HashProvider _hashProvider;
        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the HashService class.
        /// </summary>
        /// <param name="provider">Hash service provider name.</param>
        public HashService(string provider)
        {
            this._hashProvider = this.GetHashProvider(provider);
        }

        /// <summary>
        /// Initialises a new instance of the HashService class.
        /// </summary>
        /// <param name="provider">Hash service provider name.</param>
        public HashService(HashProvider provider)
        {
            this._hashProvider = provider;
        }

        private HashAlgorithm _hashAlgorithm;

        /// <summary>
        /// Gets the hash algorithm.
        /// </summary>
        private HashAlgorithm HashAlgorithm
        {
            get
            {
                if (this._hashAlgorithm == null)
                    this._hashAlgorithm = this.GetHashAlgorithm(this._hashProvider);

                return this._hashAlgorithm;
            }
        }

        /// <summary>
        /// Gets the hash provider.
        /// </summary>
        /// <param name="provider">Hash service provider name.</param>
        /// <returns>Returns the hash provider.</returns>
        private HashProvider GetHashProvider(string provider)
        {
            HashProvider result;
            var hashProvider = Enum.TryParse(provider, true, out result) ? result : HashProvider.None;
            return hashProvider;
        }

        /// <summary>
        /// Gets the hash algorithm instance.
        /// </summary>
        /// <param name="provider">Hash service provider name.</param>
        /// <returns>Returns the hash algorithm instance.</returns>
        private HashAlgorithm GetHashAlgorithm(HashProvider provider)
        {
            HashAlgorithm algorithm;
            switch (provider)
            {
                case HashProvider.MD5:
                    algorithm = new MD5CryptoServiceProvider();
                    break;

                case HashProvider.SHA1:
                    algorithm = new SHA1CryptoServiceProvider();
                    break;

                case HashProvider.SHA256:
                    algorithm = new SHA256CryptoServiceProvider();
                    break;

                case HashProvider.SHA384:
                    algorithm = new SHA384CryptoServiceProvider();
                    break;

                case HashProvider.SHA512:
                    algorithm = new SHA512CryptoServiceProvider();
                    break;

                default:
                    throw new InvalidOperationException("No hash provider is provided");
            }
            return algorithm;
        }

        /// <summary>
        /// Generates the random string.
        /// </summary>
        /// <param name="length">Length of the string generated.</param>
        /// <returns>Returns the random string.</returns>
        public string GenerateRandomString(int length = 32)
        {
            string code;
            using (var rng = new RNGCryptoServiceProvider())
            {
                var buffer = new byte[length];
                rng.GetBytes(buffer);
                code = Convert.ToBase64String(buffer).Replace("/", "").Replace("+", "").Remove(length);
            }
            return code;
        }

        /// <summary>
        /// Hashes the value.
        /// </summary>
        /// <param name="value">Value to hash.</param>
        /// <returns>Returns the value hashed.</returns>
        public string Hash(string value)
        {
            var buffer = Encoding.UTF8.GetBytes(value);
            var computed = this.HashAlgorithm.ComputeHash(buffer);
            var hashed = Convert.ToBase64String(computed);
            return hashed;
        }

        /// <summary>
        /// Validates whether the hashed value is equal to the unhashed value.
        /// </summary>
        /// <param name="hashed">Hashed text value.</param>
        /// <param name="plainText">Unhashed plain text value.</param>
        /// <returns>Returns <c>True</c>, if both are equal to each other; otherwise returns <c>False</c>.</returns>
        public bool ValidateHashedStrings(string hashed, string plainText)
        {
            var validated = hashed == this.Hash(plainText);
            return validated;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this._disposed)
                return;

            if (this._hashAlgorithm != null)
                this._hashAlgorithm.Dispose();

            this._disposed = true;
        }
    }
}