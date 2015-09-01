using CommonProvider.Data;
namespace CommonProvider
{
    /// <summary>
    /// Represents the base interface for a Provider.
    /// </summary>
    public interface IProvider
    {
        /// <summary>
        /// Gets or sets a provider's name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets a provider's settings.
        /// </summary>
        ISettings Settings { get; set; }

        /// <summary>
        /// Gets or sets a provider's group.
        /// </summary>
        string Group { get; set; }
    }
}
