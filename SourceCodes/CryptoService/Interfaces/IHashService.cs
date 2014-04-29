namespace Aliencube.CryptoService.Interfaces
{
    /// <summary>
    /// This provides interfaces to the HashService class.
    /// </summary>
    public interface IHashService : ICryptoServiceBase
    {
        /// <summary>
        /// Generates the hashed string.
        /// </summary>
        /// <param name="length">Length of the string generated.</param>
        /// <returns>Returns the hashed string.</returns>
        string GenerateHash(int length = 32);

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