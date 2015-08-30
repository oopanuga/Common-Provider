using System.Configuration;

namespace CommonProvider.Configuration
{
    /// <summary>
    /// Represents a Provider configuration element collection.
    /// </summary>
    public class ProviderElementCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Creates an instance of a provider configuration element.
        /// </summary>
        /// <returns>An instance of a Provider configuration element.</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new ProviderElement();
        }

        /// <summary>
        /// Gets the unique identifier for a provider element.
        /// </summary>
        /// <param name="element">The provider element.</param>
        /// <returns>The unique identifier for a provider element.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ProviderElement)element).Name;
        }

        /// <summary>
        /// Adds a configuration element to the collection.
        /// </summary>
        /// <param name="element">The element to add.</param>
        internal void Add(ProviderElement element)
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
