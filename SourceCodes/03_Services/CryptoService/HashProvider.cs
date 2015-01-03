namespace Aliencube.CryptoService
{
    /// <summary>
    /// This specifies the hash algorithm provider.
    /// </summary>
    public enum HashProvider
    {
        /// <summary>
        /// Indicates that no hash algorithm provider identified.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates that MD5 algorithm is identified.
        /// </summary>
        MD5 = 1,

        /// <summary>
        /// Indicates that SHA1 algorithm is identified.
        /// </summary>
        SHA1 = 2,

        /// <summary>
        /// Indicates that SHA256 algorithm is identified.
        /// </summary>
        SHA256 = 3,

        /// <summary>
        /// Indicates that SHA384 algorithm is identified.
        /// </summary>
        SHA384 = 4,

        /// <summary>
        /// Indicates that SHA512 algorithm is identified.
        /// </summary>
        SHA512 = 5
    }
}