using System.Configuration;

namespace CommonProvider.ConfigSources.Xml.Configuration
{
    /// <summary>
    /// Represents a Setting configuration element. Could be general or provider specific.
    /// </summary>
    public class ProviderSettingElement : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the setting key.
        /// </summary>
        [ConfigurationProperty("key", IsRequired = true)]
        public string Key
        {
            get { return (string)this["key"]; }
            set { this["key"] = value; }
        }

        /// <summary>
        /// Gets or sets the setting value.
        /// </summary>
        [ConfigurationProperty("value", IsRequired = true)]
        public string Value
        {
            get { return (string)this["value"]; }
            set { this["value"] = value; }
        }
    }
}