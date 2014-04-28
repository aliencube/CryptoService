using System;

namespace Aliencube.CryptoService.Interfaces
{
    /// <summary>
    /// This provides interfaces to the HashService class.
    /// </summary>
    public interface IHashService : IDisposable
    {
        /// <summary>
        /// Generates the random string.
        /// </summary>
        /// <param name="length">Length of the string generated.</param>
        /// <returns>Returns the random string.</returns>
        string GenerateRandomString(int length = 32);

        /// <summary>
        /// Hashes the value.
        /// </summary>
        /// <param name="value">Value to hash.</param>
        /// <returns>Returns the value hashed.</returns>
        string Hash(string value);

        /// <summary>
        /// Validates whether the hashed value is equal to the unhashed value.
        /// </summary>
        /// <param name="hashed">Hashed text value.</param>
        /// <param name="plainText">Unhashed plain text value.</param>
        /// <returns>Returns <c>True</c>, if both are equal to each other; otherwise returns <c>False</c>.</returns>
        bool ValidateHashedStrings(string hashed, string plainText);
    }
}