namespace Aliencube.CryptoService.Interfaces
{
    public interface ISymmetricService : ICryptoServiceBase
    {
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