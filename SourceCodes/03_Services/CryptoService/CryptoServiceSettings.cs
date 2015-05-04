using System;
using System.ComponentModel;
using System.Configuration;
using Aliencube.ConfigurationConverters;
using Aliencube.CryptoService.Interfaces;

namespace Aliencube.CryptoService
{
    /// <summary>
    /// This represents the <c>ConfigurationSection</c> element entity for <c>CryptoServiceSettings</c>.
    /// </summary>
    public class CryptoServiceSettings : ConfigurationSection, ICryptoServiceSettings
    {
        private bool _disposed;

        /// <summary>
        /// Gets or sets the hash algorithm provider.
        /// </summary>
        [ConfigurationProperty("hashProvider", IsRequired = false)]
        [TypeConverter(typeof(CaseInsensitiveEnumConverter<HashProvider>))]
        public HashProvider HashProvider
        {
            get { return (HashProvider)this["hashProvider"]; }
            set { this["hashProvider"] = value; }
        }

        /// <summary>
        /// Gets or sets the symmetric algorithm provider.
        /// </summary>
        [ConfigurationProperty("symmetricProvider", IsRequired = false)]
        [TypeConverter(typeof(CaseInsensitiveEnumConverter<SymmetricProvider>))]
        public SymmetricProvider SymmetricProvider
        {
            get { return (SymmetricProvider)this["symmetricProvider"]; }
            set { this["symmetricProvider"] = value; }
        }

        /// <summary>
        /// Gets or sets the symmetric key for encription/decription.
        /// </summary>
        [ConfigurationProperty("symmetricKey", IsRequired = false)]
        public string SymmetricKey
        {
            get { return (string)this["symmetricKey"]; }
            set { this["symmetricKey"] = value; }
        }

        /// <summary>
        /// Gets or sets the symmetric vector/IV for encription/decription.
        /// </summary>
        [ConfigurationProperty("symmetricVector", IsRequired = false)]
        public string SymmetricVector
        {
            get { return (string)this["symmetricVector"]; }
            set { this["symmetricVector"] = value; }
        }

        /// <summary>
        /// Creates a new instance of the <c>CryptoServiceSettings</c> class.
        /// </summary>
        /// <returns>Returns the new instance of the <c>CryptoServiceSettings</c> class.</returns>
        public static ICryptoServiceSettings CreateInstance()
        {
            var settings = GetFromConverterSettings();
            if (settings != null)
            {
                return settings;
            }

            settings = GetFromAppSettings();
            return settings;
        }

        /// <summary>
        /// Gets the <c>CryptoServiceSettings</c> object from the converterSettings element.
        /// </summary>
        /// <returns>Returns the <c>CryptoServiceSettings</c> object.</returns>
        private static ICryptoServiceSettings GetFromConverterSettings()
        {
            var settings = ConfigurationManager.GetSection("cryptoServiceSettings") as ICryptoServiceSettings;
            return settings;
        }

        /// <summary>
        /// Gets the <c>ConverterSettings</c> object from the appSettings element.
        /// </summary>
        /// <returns>Returns the <c>ConverterSettings</c> object.</returns>
        private static ICryptoServiceSettings GetFromAppSettings()
        {
            HashProvider hashProviderResult;
            var hashProvider = Enum.TryParse(ConfigurationManager.AppSettings["HashProvider"], true, out hashProviderResult)
                                   ? hashProviderResult
                                   : HashProvider.None;

            SymmetricProvider symmetricProviderResult;
            var symmetricProvider = Enum.TryParse(ConfigurationManager.AppSettings["SymmetricProvider"], true, out symmetricProviderResult)
                                        ? symmetricProviderResult
                                        : SymmetricProvider.None;

            var symmetricKey = ConfigurationManager.AppSettings["SymmetricKey"];
            var symmetricVector = ConfigurationManager.AppSettings["SymmetricVector"];

            var settings = new CryptoServiceSettings
                           {
                               HashProvider = hashProvider,
                               SymmetricProvider = symmetricProvider,
                               SymmetricKey = symmetricKey,
                               SymmetricVector = symmetricVector,
                           };
            return settings;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this._disposed)
            {
                return;
            }

            this._disposed = true;
        }
    }
}