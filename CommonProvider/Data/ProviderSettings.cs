using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonProvider.Data
{
    /// <summary>
    /// Represents the base interface for a set of Settings.
    /// </summary>
    public interface IProviderSettings
    {
        /// <summary>
        /// Gets a value indicating if a setting name exists or not.
        /// </summary>
        /// <param name="settingName">The name of the setting.</param>
        /// <returns>A value indicating if a setting name exists or not.</returns>
        bool Contains(string settingName);

        /// <summary>
        /// Attempts to get a setting with the specified name and of the specified 
        /// generic type. The setting is returned as an out parameter if the
        /// setting name exists otherwise the default value of the specified 
        /// type is returned.
        /// </summary>
        /// <typeparam name="T">The generic type of the setting.</typeparam>
        /// <param name="settingName">The name of the setting.</param>
        /// <param name="setting">The setting with the specified name of 
        /// the specified generic type.</param>
        /// <returns>A value indicating if a setting name exists or not.</returns>
        bool TryGet<T>(string settingName, out T setting);

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

    /// <summary>
    /// Represents the default implementation of ISettings. It holds a set of settings.
    /// </summary>
    public class ProviderSettings : IProviderSettings
    {
        readonly Dictionary<string, string> _settings;
        readonly string _dataParserType;

        #region Constructors

        /// <summary>
        /// Initializes an instance of Settings using the specified arguments.
        /// </summary>
        /// <param name="settings">A dictionary copy of the settings.</param>
        /// <param name="dataParserType">The data parser to use in parsing 
        /// strings of data to a specified type.</param>
        public ProviderSettings(Dictionary<string, string> settings, string dataParserType)
        {
            if (settings == null || settings.Count == 0)
            {
                throw new ArgumentException("settings not set");
            }

            _settings = settings;
            _dataParserType = dataParserType;
        }

        #endregion


        #region Properties

        /// <summary>
        /// Gets the count of settings
        /// </summary>
        public int Count
        {
            get
            {
                return _settings.Count;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Attempts to get a setting with the specified name and of the specified 
        /// generic type. The setting is returned as an out parameter if the
        /// setting name exists otherwise the default value of the specified 
        /// type is returned.
        /// </summary>
        /// <typeparam name="T">The generic type of the setting.</typeparam>
        /// <param name="settingName">The name of the setting.</param>
        /// <param name="setting">The setting with the specified name of 
        /// the specified generic type.</param>
        /// <returns>A value indicating if a setting name exists or not.</returns>
        public bool TryGet<T>(string settingName, out T setting)
        {
            var isContainsSettingName = Contains(settingName);

            if (isContainsSettingName)
            {
                setting = Get<T>(settingName);
            }
            else
            {
                setting = default(T);
            }

            return isContainsSettingName;
        }

        /// <summary>
        /// Gets a value indicating if a setting name exists or not.
        /// </summary>
        /// <param name="settingName">The name of the setting.</param>
        /// <returns>The value indicating if setting exists or not.</returns>
        public bool Contains(string settingName)
        {
            return _settings.Keys.Contains(settingName);
        }

        /// <summary>
        /// Gets a setting with the specified name and of the specified generic type.
        /// </summary>
        /// <typeparam name="T">The generic type return.</typeparam>
        /// <param name="settingName">The name of the setting to get.</param>
        /// <returns>The setting with the specified name of the specified generic type.</returns>
        public T Get<T>(string settingName)
        {
            if (string.IsNullOrEmpty(settingName))
            {
                throw new ArgumentException("setting name not set");
            }

            if (!Contains(settingName))
            {
                throw new ArgumentException("setting name does not exist");
            }

            Type type = typeof(T);

            if (type.IsInterface)
            {
                throw new InvalidOperationException("Type cannot be an interface");
            }

            var setting = _settings[settingName];
            if (type.IsClass && !type.Equals(typeof(string)))
            {
                // if data parser wasn't set, then attempt to parse
                // setting using the default PipedDataParser
                if (string.IsNullOrEmpty(_dataParserType))
                {
                    try
                    {
                        return new PipeDelimitedDataParser().Parse<T>(setting);
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidOperationException(
                            "Could not parse data with the default PipedDataParser," +
                            "please ensure that you set dataParserType and try again",
                            ex);
                    }
                }
                else
                {
                    var parser = (IDataParser)GenericMethodInvoker.Invoke(
                                DependencyResolver.Current,
                                "Resolve",
                                Type.GetType(_dataParserType),
                                new object[] { }
                                    );

                    return parser.Parse<T>(setting);
                }
            }
            else
            {
                return (T)Convert.ChangeType(setting, type);
            }
        }

        #endregion

        #region Indexers

        /// <summary>
        /// Gets a setting with the specified name and of the specified type.
        /// </summary>
        /// <param name="settingName">The name of the setting to get.</param>
        /// <param name="type">The type of the setting.</param>
        /// <returns>The setting with the specified name of the specified type</returns>
        public dynamic this[string settingName, Type type]
        {
            get
            {
                return GenericMethodInvoker.Invoke(this, "Get",
                    type, new object[] { settingName });
            }
        }

        /// <summary>
        /// Gets a setting with the specified name.
        /// </summary>
        /// <param name="settingName">The name of the setting to get.</param>
        /// <returns>The setting with the specified name.</returns>
        public string this[string settingName]
        {
            get
            {
                return Get<string>(settingName);
            }
        }

        #endregion
    }
}
