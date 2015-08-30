using CommonProvider.Data;
namespace CommonProvider
{
    /// <summary>
    /// Represents the base interface for a Provider.
    /// </summary>
    public interface IProvider
    {
        /// <summary>
        /// Gets a provider's name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets a provider's settings.
        /// </summary>
        ISettings Settings { get; set; }

        /// <summary>
        /// Gets a provider's group.
        /// </summary>
        string Group { get; set; }
    }
}
