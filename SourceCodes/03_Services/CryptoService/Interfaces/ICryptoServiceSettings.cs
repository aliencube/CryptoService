namespace Aliencube.CryptoService.Interfaces
{
    /// <summary>
    /// This provides interfaces to the <c>CryptoServiceSettings</c> class.
    /// </summary>
    public interface ICryptoServiceSettings
    {
        /// <summary>
        /// Gets or sets the hash algorithm provider.
        /// </summary>
        HashProvider HashProvider { get; set; }

        /// <summary>
        /// Gets or sets the symmetric algorithm provider.
        /// </summary>
        SymmetricProvider SymmetricProvider { get; set; }

        /// <summary>
        /// Gets or sets the symmetric key for encription/decription.
        /// </summary>
        string SymmetricKey { get; set; }

        /// <summary>
        /// Gets or sets the symmetric vector/IV for encription/decription.
        /// </summary>
        string SymmetricVector { get; set; }
    }
}