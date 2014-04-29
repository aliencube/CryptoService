using Aliencube.CryptoService.Interfaces;
using System;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace Aliencube.CryptoService
{
    /// <summary>
    /// This represents a hash service entity that provides the one-way encryption service for text.
    /// </summary>
    public class HashService : CryptoServiceBase, IHashService
    {
        private readonly HashProvider _hashProvider;
        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the HashService class.
        /// </summary>
        /// <param name="provider">Hash service provider name.</param>
        /// <exception cref="ArgumentNullException">Thrown when the provider value is not provided.</exception>
        public HashService(string provider)
        {
            if (String.IsNullOrWhiteSpace(provider))
                throw new ArgumentNullException("provider");

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
        /// <exception cref="ArgumentNullException">Thrown when the provider value is not provided.</exception>
        private HashProvider GetHashProvider(string provider)
        {
            if (String.IsNullOrWhiteSpace(provider))
                throw new ArgumentNullException("provider");

            HashProvider result;
            var hashProvider = Enum.TryParse(provider, true, out result) ? result : HashProvider.None;
            return hashProvider;
        }

        /// <summary>
        /// Gets the hash algorithm instance.
        /// </summary>
        /// <param name="provider">Hash service provider name.</param>
        /// <returns>Returns the hash algorithm instance.</returns>
        /// <exception cref="InvalidEnumArgumentException">Thrown when the <c>HashProvider</c> is <c>None</c>.</exception>
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
                    throw new InvalidEnumArgumentException("Invalid HashProvider is provided");
            }
            return algorithm;
        }

        /// <summary>
        /// Hashes the value.
        /// </summary>
        /// <param name="value">Value to hash.</param>
        /// <returns>Returns the value hashed.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the value is not provided.</exception>
        public string Hash(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("value");

            return this.Transform(value, CryptoDirection.Hash);
        }

        /// <summary>
        /// Validates whether the hashed value is equal to the unhashed value.
        /// </summary>
        /// <param name="hashed">Hashed text value.</param>
        /// <param name="plainText">Unhashed plain text value.</param>
        /// <returns>Returns <c>True</c>, if both are equal to each other; otherwise returns <c>False</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when either hashed value or plain text value is not provided.</exception>
        public bool ValidateHashedStrings(string hashed, string plainText)
        {
            if (String.IsNullOrWhiteSpace(hashed))
                throw new ArgumentNullException("hashed");

            if (String.IsNullOrWhiteSpace(plainText))
                throw new ArgumentNullException("plainText");

            var validated = hashed == this.Hash(plainText);
            return validated;
        }

        /// <summary>
        /// Transforms the value.
        /// </summary>
        /// <param name="value">Value to transform.</param>
        /// <param name="direction">Direction to transform.</param>
        /// <returns>Returns the value transformed.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the value to transform is not provided.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the <c>CryptoDirection</c> is not <c>Hash</c>.</exception>
        public override string Transform(string value, CryptoDirection direction)
        {
            if (String.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("value");

            if (direction != CryptoDirection.Hash)
                throw new InvalidEnumArgumentException("Only CryptoDirection.Hash is accepted");

            var buffer = Encoding.UTF8.GetBytes(value);
            var computed = this.HashAlgorithm.ComputeHash(buffer);
            var hashed = Convert.ToBase64String(computed);

            return hashed;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();

            if (this._disposed)
                return;

            if (this._hashAlgorithm != null)
                this._hashAlgorithm.Dispose();

            this._disposed = true;
        }
    }
}