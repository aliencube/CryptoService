namespace Aliencube.CryptoService
{
    /// <summary>
    /// This specifies the symmetric algorithm provider.
    /// </summary>
    public enum SymmetricProvider
    {
        /// <summary>
        /// Indicates that no symmetric algorithm provider identified.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates that Aes algorithm is identified.
        /// </summary>
        Aes = 1,

        /// <summary>
        /// Indicates that DES algorithm is identified.
        /// </summary>
        DES = 2,

        /// <summary>
        /// Indicates that RC2 algorithm is identified.
        /// </summary>
        RC2 = 3,

        /// <summary>
        /// Indicates that Rijndael algorithm is identified.
        /// </summary>
        Rijndael = 4,

        /// <summary>
        /// Indicates that TripleDES algorithm is identified.
        /// </summary>
        TripleDES = 5
    }
}