using System.Configuration;

namespace CommonProvider.Configuration
{
    /// <summary>
    /// Represents a Provider configuration section.
    /// </summary>
    public class ProviderConfigSection : ConfigurationSection
    {
        /// <summary>
        /// Gets a collection of types.
        /// </summary>
        [ConfigurationProperty("types", IsRequired = false)]
        [ConfigurationCollection(typeof(TypeElementCollection), AddItemName = "add")]
        public TypeElementCollection Types
        {
            get
            {
                return (TypeElementCollection)base["types"];
            }
        }

        /// <summary>
        /// Gets a collection of providers.
        /// </summary>
        [ConfigurationProperty("providers")]
        [ConfigurationCollection(typeof(ProviderElementCollection), AddItemName = "provider")]
        public ProviderElementCollection Providers
        {
            get
            {
                return (ProviderElementCollection)base["providers"];
            }
        }

        /// <summary>
        /// Gets a collection of provider wide settings.
        /// </summary>
        [ConfigurationProperty("settings", IsRequired = false)]
        [ConfigurationCollection(typeof(ProviderSettingElementCollection), AddItemName = "add")]
        public ProviderSettingElementCollection Settings
        {
            get
            {
                return (ProviderSettingElementCollection)base["settings"];
            }
        }
    }
}
