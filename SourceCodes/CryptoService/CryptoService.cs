using Aliencube.CryptoService.Exceptions;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using KeyNotFoundException = Aliencube.CryptoService.Exceptions.KeyNotFoundException;

namespace Aliencube.CryptoService
{
    public enum CryptionProvider
    {
        None = 0,
    }

    /// <summary>
    /// This represents a crypto-service entity that provides either one-way encryption or two-way encryption/decryption service for text.
    /// </summary>
    public class CryptoService
    {
        private readonly CryptionProvider _cryptoProvider;

        /// <summary>
        /// Initialises a new instance of the CryptoService class.
        /// </summary>
        /// <param name="provider">Cryption service provider name.</param>
        /// <param name="key">Passphrase key.</param>
        /// <param name="vector">Passphrase vector.</param>
        public CryptoService(string provider, string key = null, string vector = null)
        {
            this._cryptoProvider = this.GetCryptoProvider(provider);
            this.Key = key;
            this.Vector = !String.IsNullOrWhiteSpace(vector) ? vector : this.GetVectorFromKey(key);
        }

        /// <summary>
        /// Initialises a new instance of the CryptoService class.
        /// </summary>
        /// <param name="provider">Cryption service provider name.</param>
        /// <param name="key">Passphrase key.</param>
        /// <param name="vector">Passphrase vector.</param>
        public CryptoService(CryptionProvider provider, string key = null, string vector = null)
        {
            this._cryptoProvider = provider;
            this.Key = key;
            this.Vector = !String.IsNullOrWhiteSpace(vector) ? vector : this.GetVectorFromKey(key);
        }

        /// <summary>
        /// Gets or sets the key for encryption or decryption.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the initialisation vector for encryption or decryption.
        /// </summary>
        public string Vector { get; set; }

        /// <summary>
        /// Gets the cryption provider.
        /// </summary>
        /// <param name="provider">Cryption service provider name.</param>
        /// <returns>Returns the hash provider.</returns>
        private CryptionProvider GetCryptoProvider(string provider)
        {
            CryptionProvider result;
            var cryptoProvider = Enum.TryParse(provider, true, out result) ? result : CryptionProvider.None;
            return cryptoProvider;
        }

        /// <summary>
        /// Gets the initialisation vector value computed from the key.
        /// </summary>
        /// <param name="key">Passphrase.</param>
        /// <returns>Returns the vector value computed from the key.</returns>
        private string GetVectorFromKey(string key)
        {
            var vector = String.Empty;
            var chars = key.ToCharArray();
            for (var i = 0; i < chars.Length; i = i + 2)
                vector += chars[i];
            return vector;
        }

        /// <summary>
        /// Generates the random string.
        /// </summary>
        /// <param name="value">Seed.</param>
        /// <param name="maxLength">Maxinum length of the string generated.</param>
        /// <returns>Returns the random string.</returns>
        public static string GenerateRandomCode(long value = 0, int maxLength = 32)
        {
            var keyLower = "abcdefghijklmnopqrstuvwxyz";
            var keyUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var keyNumber = "0123456789";
            var keys = (keyLower + keyUpper + keyNumber).ToCharArray();

            string code = null;
            var random = value > 0 ? new Random(unchecked((int)value)) : new Random();
            for (var i = 0; i < maxLength; i++)
                code += keys[random.Next(keys.Length)];
            return code;
        }

        /// <summary>
        /// Encrypts the value.
        /// </summary>
        /// <param name="value">Value to encrypt.</param>
        /// <returns>Returns the value encrypted. If there is an error while transforming the value, it returns <c>String.Empty</c>, instead of throwing an exception.</returns>
        public string Encrypt(string value)
        {
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
        public string Decrypt(string value)
        {
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
        /// <exception cref="KeyNotFoundException">Thrown when the key is not declared.</exception>
        /// <exception cref="VectorNotFoundException">Thrown when the initialisation vector is not declared.</exception>
        /// <exception cref="InvalidFormatException">Thrown when either key or vector does not have a proper length.</exception>
        /// <exception cref="ValueNotFoundException">Thrown when the value to transform is NULL or empty.</exception>
        /// <returns>Returns the value transformed.</returns>
        public string Transform(string value, CryptoDirection direction)
        {
            if (String.IsNullOrWhiteSpace(this.Key))
                throw new KeyNotFoundException("Key has not been defined.");
            if (this.Key.Length != 32)
                throw new InvalidFormatException("Invalid key format.");
            if (String.IsNullOrWhiteSpace(this.Vector))
                throw new VectorNotFoundException("Vector has not been defined.");
            if (this.Vector.Length != 16)
                throw new InvalidFormatException("Invalid vector format.");
            if (String.IsNullOrWhiteSpace(value))
                throw new ValueNotFoundException("Value is NULL or empty.");

            string transformed;
            using (var aes = new AesManaged())
            {
                aes.Key = Encoding.UTF8.GetBytes(this.Key);
                aes.IV = Encoding.UTF8.GetBytes(this.Vector);

                var buffer = direction == CryptoDirection.Encrypt
                                 ? Encoding.UTF8.GetBytes(value)
                                 : Convert.FromBase64String(value);
                var transform = direction == CryptoDirection.Encrypt
                                    ? aes.CreateEncryptor(aes.Key, aes.IV)
                                    : aes.CreateDecryptor(aes.Key, aes.IV);

                using (var ms = direction == CryptoDirection.Encrypt ? new MemoryStream() : new MemoryStream(buffer))
                using (var cs = new CryptoStream(ms, transform, direction == CryptoDirection.Encrypt
                                                                    ? CryptoStreamMode.Write
                                                                    : CryptoStreamMode.Read))
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
            }
            return transformed;
        }
    }
}