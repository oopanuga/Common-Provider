using CommonProvider.Data;
using CommonProvider.ProviderLoaders;

namespace CommonProvider
{
    /// <summary>
    /// Represents the base interface for IZeroConfigProviderManager. This is 
    /// the gateway to all zero config providers.
    /// </summary>
    public interface IZeroConfigProviderManager
    {
        /// <summary>
        /// Gets the set of loaded zero config providers.
        /// </summary>
        IZeroConfigProviderList Providers { get; }
    }

    /// <summary>
    /// Represents the default implementation of IZeroConfigProviderManager. 
    /// This is the gateway to all zero config providers
    /// </summary>
    public sealed class ZeroConfigProviderManager : IZeroConfigProviderManager
    {
        #region Constructors

        /// <summary>
        /// Initializes an instance of ZeroConfigProviderManager using the specified assembly directory. 
        /// </summary>
        /// <param name="assemblyDirectory">The directory to get assemblies from.</param>
        public ZeroConfigProviderManager(string assemblyDirectory)
        {
            var providerTypes = new ZeroConfigProviderLoader(assemblyDirectory).Load();
            Providers = new ZeroConfigProviderList(providerTypes);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the set of loaded providers.
        /// </summary>
        public IZeroConfigProviderList Providers { get; private set; }

        #endregion
    }
}
