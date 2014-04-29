namespace Aliencube.CryptoService.Interfaces
{
    /// <summary>
    /// This provides interfaces to the SymmetricService class.
    /// </summary>
    public interface ISymmetricService : ICryptoServiceBase
    {
        /// <summary>
        /// Gets or sets the key for encryption or decryption.
        /// </summary>
        string Key { get; set; }

        /// <summary>
        /// Gets or sets the initialisation vector for encryption or decryption.
        /// </summary>
        string Vector { get; set; }

        /// <summary>
        /// Encrypts the value.
        /// </summary>
        /// <param name="value">Value to encrypt.</param>
        /// <returns>Returns the value encrypted. If there is an error while transforming the value, it returns <c>String.Empty</c>, instead of throwing an exception.</returns>
        string Encrypt(string value);

        /// <summary>
        /// Decrypts the value.
        /// </summary>
        /// <param name="value">Value to decrypt.</param>
        /// <returns>Returns the value decrypted. If there is an error while transforming the value, it returns <c>String.Empty</c>, instead of throwing an exception.</returns>
        string Decrypt(string value);
    }
}