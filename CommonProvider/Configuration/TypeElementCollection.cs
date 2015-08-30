using System.Configuration;

namespace CommonProvider.Configuration
{
    /// <summary>
    /// Represents a Types configuration element collection.
    /// </summary>
    public class TypeElementCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Creates a new Type configuration element.
        /// </summary>
        /// <returns>An instance of a Type configuration element.</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new TypeElement();
        }

        /// <summary>
        /// Gets the unique identifier for a Type configuration element.
        /// </summary>
        /// <param name="element">The Type configuration element.</param>
        /// <returns>The unique identifier Type configuration element.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((TypeElement)element).Name;
        }

        /// <summary>
        /// Adds a configuration element to the collection.
        /// </summary>
        /// <param name="element">The element to add.</param>
        internal void Add(TypeElement element)
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
