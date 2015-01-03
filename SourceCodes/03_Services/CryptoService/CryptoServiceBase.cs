using Aliencube.CryptoService.Interfaces;

namespace Aliencube.CryptoService
{
    /// <summary>
    /// This represents the base class for cryption service entities. This must be inherited.
    /// </summary>
    public abstract class CryptoServiceBase : ICryptoServiceBase
    {
        /// <summary>
        /// Transforms the value.
        /// </summary>
        /// <param name="value">Value to transform.</param>
        /// <param name="direction">Direction to transform.</param>
        /// <returns>Returns the value transformed.</returns>
        public abstract string Transform(string value, CryptoDirection direction);

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public virtual void Dispose()
        {
        }
    }
}