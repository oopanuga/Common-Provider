using System.Configuration;

namespace CommonProvider.Configuration
{
    /// <summary>
    /// Represents a Provider configuration element.
    /// </summary>
    public class ProviderElement : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets a provider's name.
        /// </summary>
        [ConfigurationProperty("name", IsRequired = false)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        /// <summary>
        /// Gets or sets a provider's group.
        /// </summary>
        [ConfigurationProperty("group", IsRequired = false)]
        public string Group
        {
            get { return (string)this["group"]; }
            set { this["group"] = value; }
        }

        /// <summary>
        /// Gets or sets a provider's type.
        /// </summary>
        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get { return (string)this["type"]; }
            set { this["type"] = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not the provider has been enabled.
        /// </summary>
        [ConfigurationProperty("enabled", IsRequired = false, DefaultValue = true)]
        public bool IsEnabled
        {
            get { return (bool)this["enabled"]; }
            set { this["enabled"] = value; }
        }

        /// <summary>
        /// Gets a collection of provider specific settings.
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
