using System;

namespace Aliencube.CryptoService.Interfaces
{
    /// <summary>
    /// This provides interfaces to the CryptoServiceBase class.
    /// </summary>
    public interface ICryptoServiceBase : IDisposable
    {
        /// <summary>
        /// Generates the random string.
        /// </summary>
        /// <param name="length">Length of the string generated.</param>
        /// <returns>Returns the random string.</returns>
        string GenerateRandomString(int length = 32);

        /// <summary>
        /// Transforms the value.
        /// </summary>
        /// <param name="value">Value to transform.</param>
        /// <param name="direction">Direction to transform.</param>
        /// <returns>Returns the value transformed.</returns>
        string Transform(string value, CryptoDirection direction);
    }
}