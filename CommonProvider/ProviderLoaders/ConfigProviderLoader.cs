using CommonProvider.Configuration;
using CommonProvider.Data;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace CommonProvider.ProviderLoaders
{
    /// <summary>
    /// Represents an implementation of ProviderLoaderBase that retrieves provider 
    /// information from a configuration file. The Config Provider Loader requires 
    /// that information regarding the providers be pre-configured before loading.
    /// </summary>
    public class ConfigProviderLoader : ProviderLoaderBase
    {
        private readonly IProviderConfigurationManager _providerConfigurationManager;
        private const string SectionName = "commonProvider";

        /// <summary>
        /// Initializes an instance of ConfigProviderLoader using the specified
        /// ProviderConfigurationManager
        /// </summary>
        /// <param name="providerConfigurationManager">The provider configuration manager.</param>
        public ConfigProviderLoader(
            IProviderConfigurationManager providerConfigurationManager)
        {
            _providerConfigurationManager = providerConfigurationManager;
        }

        /// <summary>
        /// Loads provider information/meta data from a configuration file.
        /// </summary>
        /// <returns>The loaded providers data.</returns>
        protected override IProviderData PerformLoad()
        {
            var configSection = 
                _providerConfigurationManager.GetSection(SectionName);
 
            if (configSection == null)
            {
                throw new ConfigurationErrorsException(
                    string.Format("Config section {0} not defined", 
                    SectionName));
            }

            var providerDescriptors = new List<IProviderDescriptor>();

            Settings generalSettings = null;
            Dictionary<string, string> gs = null;

            // set general settings
            foreach (ProviderSettingElement setting in configSection.Settings)
            {
                if (gs == null)
                    gs = new Dictionary<string, string>();

                gs.Add(setting.Key, setting.Value);
            }

            if (gs != null)
            {
                string dataParserType = string.Empty;
                if (!string.IsNullOrEmpty(configSection.Settings.DataParserType))
                {
                    dataParserType = GetDataParserType(configSection,
                        configSection.Settings.DataParserType);
                }

                generalSettings = new Settings(gs, dataParserType);
            }

            foreach (ProviderElement providerElement in configSection.Providers)
            {
                if (!providerElement.IsEnabled) continue;

                // get provider type
                Type providerType = GetProviderType(configSection, providerElement);
                if (!typeof(IProvider).IsAssignableFrom(providerType))
                {
                    throw new ConfigurationErrorsException(
                        string.Format("Type '{0}' doesn't implement 'IProvider'.", providerType));
                }

                // set custom settings
                Dictionary<string, string> ps = null;
                foreach (ProviderSettingElement setting in providerElement.Settings)
                {
                    if (ps == null)
                        ps = new Dictionary<string, string>();

                    ps.Add(setting.Key, setting.Value);
                }

                // get complex data parser type
                Settings providerSetting = null;
                if (ps != null)
                {
                    string dataParserType = string.Empty;
                    if (!string.IsNullOrEmpty(providerElement.Settings.DataParserType))
                    {
                        dataParserType = GetDataParserType(configSection,
                            providerElement.Settings.DataParserType);

                    }
                    providerSetting = new Settings(ps, dataParserType);
                }

                providerDescriptors.Add(
                    new ProviderDescriptor(
                            providerElement.Name,
                            providerElement.Group,
                            providerType,
                            providerSetting,
                            providerElement.IsEnabled)
                        );
            }

            return new ProviderData(providerDescriptors, generalSettings);
        }

        #region Helpers

        private string GetDataParserType(ProviderConfigSection configSection, string dataParserType)
        {
            string dataParserElementType = GetObjectType(configSection, dataParserType);
            if (!string.IsNullOrEmpty(dataParserElementType))
            {
                if (Type.GetType(dataParserElementType) == null)
                {
                    throw new ConfigurationErrorsException(
                        "The Type defined for the ComplexDataTypeParser is not valid.");
                }
                else
                {
                    return dataParserElementType;
                }
            }
            else
            {
                if (Type.GetType(dataParserType) == null)
                {
                    throw new ConfigurationErrorsException(
                        "No Type found for the ComplexDataTypeParser.");
                }
                else
                {
                    return dataParserType;
                }
            }
        }

        private Type GetProviderType(ProviderConfigSection configSection, ProviderElement providerElement)
        {
            Type providerType = null;
            string providerElementType = GetObjectType(configSection, providerElement.Type);
            if (!string.IsNullOrEmpty(providerElementType))
            {
                providerType = Type.GetType(providerElementType);
                if (providerType == null)
                {
                    throw new ConfigurationErrorsException(
                        string.Format("The Type defined for Provider '{0}' is not valid.",
                        providerElement.Name));
                }
            }
            else
            {
                providerType = Type.GetType(providerElement.Type);
                if (providerType == null)
                {
                    throw new ConfigurationErrorsException(
                        string.Format("No Type found for Provider '{0}'.",
                        providerElement.Name));
                }
            }
            return providerType;
        }

        private string GetObjectType(ProviderConfigSection configSection, string typeName)
        {
            if (configSection.Types == null) return string.Empty;

            string objectType = string.Empty;
            foreach (TypeElement objectTypeElement in configSection.Types)
            {
                if (objectTypeElement.Name.Equals(typeName, StringComparison.OrdinalIgnoreCase))
                {
                    objectType = objectTypeElement.Type;
                    break;
                }
            }
            return objectType;
        }

        #endregion
    }
}
