
using System;
namespace CommonProvider.Data
{
    /// <summary>
    /// Represents the base interface for a set of Settings.
    /// </summary>
    public interface ISettings
    {
        /// <summary>
        /// Gets a setting with the specified name and of the specified generic type.
        /// </summary>
        /// <typeparam name="T">The generic type return.</typeparam>
        /// <param name="settingName">The name of the setting to get.</param>
        /// <returns>The setting with the specified name of the specified generic type.</returns>
        T Get<T>(string settingName);

        /// <summary>
        /// Gets a setting with the specified name.
        /// </summary>
        /// <param name="settingName">The name of the setting to get.</param>
        /// <returns>The setting with the specified name.</returns>
        string this[string settingName] { get; }

        /// <summary>
        /// Gets a setting with the specified name and of the specified type.
        /// </summary>
        /// <param name="settingName">The name of the setting to get.</param>
        /// <param name="type">The type return.</param>
        /// <returns>The setting with the specified name of the specified type.</returns>
        dynamic this[string settingName, Type type] { get; }

        /// <summary>
        /// Gets the count of settings
        /// </summary>
        int Count { get; }
    }
}
