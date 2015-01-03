using System;

namespace Aliencube.CryptoService.Interfaces
{
    /// <summary>
    /// This provides interfaces to the CryptoServiceBase class.
    /// </summary>
    public interface ICryptoServiceBase : IDisposable
    {
        /// <summary>
        /// Transforms the value.
        /// </summary>
        /// <param name="value">Value to transform.</param>
        /// <param name="direction">Direction to transform.</param>
        /// <returns>Returns the value transformed.</returns>
        string Transform(string value, CryptoDirection direction);
    }
}