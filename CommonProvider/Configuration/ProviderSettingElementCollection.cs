using System.Configuration;

namespace CommonProvider.Configuration
{
    /// <summary>
    /// Represents a Setting configuration element collection. Could be general 
    /// or provider specific.
    /// </summary>
    public class ProviderSettingElementCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Creates an instance of a setting configuration element.
        /// </summary>
        /// <returns>An instance of a Setting configuration element.</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new ProviderSettingElement();
        }

        /// <summary>
        /// Gets the unique identifier for a setting configuration element.
        /// </summary>
        /// <param name="element">The provider setting element.</param>
        /// <returns>The unique identifier for a provider setting element.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ProviderSettingElement)element).Key;
        }

        /// <summary>
        /// Gets or sets the data parser type for the setting. This is used to 
        /// parse strings of data to specified types.
        /// </summary>
        [ConfigurationProperty("dataParserType", IsRequired = false)]
        public string DataParserType
        {
            get { return (string)this["dataParserType"]; }
            set { this["dataParserType"] = value; }
        }

        /// <summary>
        /// Adds a configuration element to the collection.
        /// </summary>
        /// <param name="element">The element to add.</param>
        internal void Add(ProviderSettingElement element)
        {
            this.BaseAdd(element);
        }

        /// <summary>
        /// Removes all configuration elements from the collection.
        /// </summary>
        internal void Clear()
        {
            this.BaseClear();
        }
    }
}
