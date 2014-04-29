using Aliencube.CryptoService.Interfaces;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace Aliencube.CryptoService
{
    /// <summary>
    /// This represents the base class for cryption service entities. This must be inherited.
    /// </summary>
    public abstract class CryptoServiceBase : ICryptoServiceBase
    {
        /// <summary>
        /// Generates the random string.
        /// </summary>
        /// <param name="length">Length of the string generated.</param>
        /// <returns>Returns the random string.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the length value is less than or equal to 0.</exception>
        public string GenerateRandomString(int length = 32)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException("length");

            string code;
            using (var rng = new RNGCryptoServiceProvider())
            {
                var buffer = new byte[length];
                rng.GetBytes(buffer);
                code = String.Join("", Convert.ToBase64String(buffer)
                                              .Where(p => !(new[] { '/', '+' }).Contains(p))
                                              .Take(length));
            }
            return code;
        }

        /// <summary>
        /// Transforms the value.
        /// </summary>
        /// <param name="value">Value to transform.</param>
        /// <param name="direction">Direction to transform.</param>
        /// <returns>Returns the value transformed.</returns>
        public abstract string Transform(string value, CryptoDirection direction);

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public virtual void Dispose()
        {
        }
    }
}