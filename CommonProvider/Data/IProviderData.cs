using System.Collections.Generic;

namespace CommonProvider.Data
{
    /// <summary>
    /// Represents the base interface for ProviderData. It holds 
    /// information regarding all loaded providers.
    /// </summary>
    public interface IProviderData
    {
        /// <summary>
        /// Gets provider wide settings.
        /// </summary>
        ISettings Settings { get; }

        /// <summary>
        /// Gets the meta data of a loaded Provider e.g. Name, Type etc.
        /// </summary>
        IEnumerable<IProviderDescriptor> ProviderDescriptors { get; }
    }
}
