using System.Configuration;

namespace CommonProvider.Configuration
{
    /// <summary>
    /// Represents a Type configuration element.
    /// </summary>
    public class TypeElement : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the name of a Type.
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        /// <summary>
        /// Gets or sets the type of the Type.
        /// </summary>
        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get { return (string)this["type"]; }
            set { this["type"] = value; }
        }
    }
}
