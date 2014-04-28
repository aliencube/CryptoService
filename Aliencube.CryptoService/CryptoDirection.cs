namespace Aliencube.CryptoService
{
    /// <summary>
    /// This provides a direction for crypto service.
    /// </summary>
    public enum CryptoDirection
    {
        /// <summary>
        /// Indicates that no encryption/decryption is identified.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Indicates that two-way "Encrypt" is performed.
        /// </summary>
        Encrypt = 1,

        /// <summary>
        /// Indicates that two-way "Decrypt" is performed.
        /// </summary>
        Decrypt = 2
    }
}