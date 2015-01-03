using System;
using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Aliencube.CryptoService.Exceptions;
using Aliencube.CryptoService.Interfaces;

namespace Aliencube.CryptoService
{
    /// <summary>
    /// This represents a symmetric cryption service entity that provides two-way encryption/decryption service for text.
    /// </summary>
    public class SymmetricService : CryptoServiceBase, ISymmetricService
    {
        private readonly SymmetricProvider _symmetricProvider;
        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the SymmetricService class.
        /// </summary>
        /// <param name="provider">Cryption service provider name.</param>
        public SymmetricService(string provider)
        {
            this._symmetricProvider = this.GetSymmetricProvider(provider);
        }

        /// <summary>
        /// Initialises a new instance of the SymmetricService class.
        /// </summary>
        /// <param name="provider">Cryption service provider name.</param>
        public SymmetricService(SymmetricProvider provider)
        {
            this._symmetricProvider = provider;
        }

        private SymmetricAlgorithm _symmetricAlgorithm;

        /// <summary>
        /// Gets the symmetric algorithm.
        /// </summary>
        private SymmetricAlgorithm SymmetricAlgorithm
        {
            get
            {
                if (this._symmetricAlgorithm == null)
                    this._symmetricAlgorithm = this.GetSymmetricAlgorithm(this._symmetricProvider);

                return this._symmetricAlgorithm;
            }
        }

        private string _key;

        /// <summary>
        /// Gets or sets the key for encryption or decryption.
        /// </summary>
        public string Key
        {
            get { return this._key; }
            set
            {
                if (!String.IsNullOrWhiteSpace(value) && this.SymmetricAlgorithm.Key.Length != value.Length)
                    throw new InvalidDataLengthException(String.Format("Key must be length of {0}", this.SymmetricAlgorithm.Key.Length));

                this._key = value;
            }
        }

        private string _vector;

        /// <summary>
        /// Gets or sets the initialisation vector for encryption or decryption.
        /// </summary>
        public string Vector
        {
            get { return this._vector; }
            set
            {
                if (!String.IsNullOrWhiteSpace(value) && this.SymmetricAlgorithm.IV.Length != value.Length)
                    throw new InvalidDataLengthException(String.Format("Vector must be length of {0}", this.SymmetricAlgorithm.IV.Length));

                this._vector = value;
            }
        }

        /// <summary>
        /// Gets the cryption provider.
        /// </summary>
        /// <param name="provider">Cryption service provider name.</param>
        /// <returns>Returns the hash provider.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the provider value is not provided.</exception>
        private SymmetricProvider GetSymmetricProvider(string provider)
        {
            if (String.IsNullOrWhiteSpace(provider))
                throw new ArgumentNullException("provider");

            SymmetricProvider result;
            var cryptoProvider = Enum.TryParse(provider, true, out result) ? result : SymmetricProvider.None;
            return cryptoProvider;
        }

        /// <summary>
        /// Gets the hash algorithm instance.
        /// </summary>
        /// <param name="provider">Hash service provider name.</param>
        /// <returns>Returns the hash algorithm instance.</returns>
        /// <exception cref="InvalidEnumArgumentException">Thrown when the <c>SymmetricProvider</c> is <c>None</c>.</exception>
        private SymmetricAlgorithm GetSymmetricAlgorithm(SymmetricProvider provider)
        {
            SymmetricAlgorithm algorithm;
            switch (provider)
            {
                case SymmetricProvider.Aes:
                    algorithm = new AesCryptoServiceProvider();
                    break;

                case SymmetricProvider.DES:
                    algorithm = new DESCryptoServiceProvider();
                    break;

                case SymmetricProvider.RC2:
                    algorithm = new RC2CryptoServiceProvider();
                    break;

                case SymmetricProvider.Rijndael:
                    algorithm = new RijndaelManaged();
                    break;

                case SymmetricProvider.TripleDES:
                    algorithm = new TripleDESCryptoServiceProvider();
                    break;

                default:
                    throw new InvalidEnumArgumentException("Invalid SymmetricProvider is provided");
            }
            return algorithm;
        }

        /// <summary>
        /// Encrypts the value.
        /// </summary>
        /// <param name="value">Value to encrypt.</param>
        /// <returns>Returns the value encrypted. If there is an error while transforming the value, it returns <c>String.Empty</c>, instead of throwing an exception.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the value is not provided.</exception>
        public string Encrypt(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("value");

            string encrypted;
            try
            {
                encrypted = this.Transform(value, CryptoDirection.Encrypt);
            }
            catch
            {
                encrypted = String.Empty;
            }
            return encrypted;
        }

        /// <summary>
        /// Decrypts the value.
        /// </summary>
        /// <param name="value">Value to decrypt.</param>
        /// <returns>Returns the value decrypted. If there is an error while transforming the value, it returns <c>String.Empty</c>, instead of throwing an exception.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the value is not provided.</exception>
        public string Decrypt(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("value");

            string decrypted;
            try
            {
                decrypted = this.Transform(value, CryptoDirection.Decrypt);
            }
            catch
            {
                decrypted = String.Empty;
            }
            return decrypted;
        }

        /// <summary>
        /// Transforms the value.
        /// </summary>
        /// <param name="value">Value to transform.</param>
        /// <param name="direction">Direction to transform.</param>
        /// <returns>Returns the value transformed.</returns>
        /// <exception cref="PropertyValueNotDefinedException">Thrown when the either key or vector is not declared.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the value to transform is NULL or empty.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the <c>CryptoDirection</c> is neither <c>Encrypt</c> nor <c>Decrypt</c>.</exception>
        public override string Transform(string value, CryptoDirection direction)
        {
            if (String.IsNullOrWhiteSpace(this.Key))
                throw new PropertyValueNotDefinedException("Key has not been defined.");

            if (String.IsNullOrWhiteSpace(this.Vector))
                throw new PropertyValueNotDefinedException("Vector has not been defined.");

            if (String.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("value");

            if (direction == CryptoDirection.Unknown || direction == CryptoDirection.Hash)
                throw new InvalidEnumArgumentException("Only CryptoDirection.Encrypt or CryptoDirection.Decrypt is accepted");

            this.SymmetricAlgorithm.Key = Encoding.UTF8.GetBytes(this.Key);
            this.SymmetricAlgorithm.IV = Encoding.UTF8.GetBytes(this.Vector);

            var buffer = direction == CryptoDirection.Encrypt
                             ? Encoding.UTF8.GetBytes(value)
                             : Convert.FromBase64String(value);
            var transform = direction == CryptoDirection.Encrypt
                                ? this.SymmetricAlgorithm.CreateEncryptor(this.SymmetricAlgorithm.Key,
                                                                          this.SymmetricAlgorithm.IV)
                                : this.SymmetricAlgorithm.CreateDecryptor(this.SymmetricAlgorithm.Key,
                                                                          this.SymmetricAlgorithm.IV);

            string transformed;
            using (var ms = direction == CryptoDirection.Encrypt ? new MemoryStream() : new MemoryStream(buffer))
            using (var cs = new CryptoStream(ms, transform, direction == CryptoDirection.Encrypt ? CryptoStreamMode.Write : CryptoStreamMode.Read))
            {
                if (direction == CryptoDirection.Encrypt)
                {
                    cs.Write(buffer, 0, buffer.Length);
                    cs.FlushFinalBlock();
                    transformed = Convert.ToBase64String(ms.ToArray());
                }
                else
                {
                    using (var sr = new StreamReader(cs))
                    {
                        transformed = sr.ReadToEnd();
                    }
                }
            }
            return transformed;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();

            if (this._disposed)
                return;

            if (this._symmetricAlgorithm != null)
                this._symmetricAlgorithm.Dispose();

            this._disposed = true;
        }
    }
}