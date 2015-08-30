using CommonProvider.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;

namespace CommonProvider.ProviderLoaders
{
    /// <summary>
    /// Represents an implementation of ProviderLoaderBase that loads information 
    /// regarding all providers from assemblies. The assemblies are located in the 
    /// specified directory. The Directory Provider Loader does not require 
    /// any configuration.
    /// </summary>
    public class DirectoryProviderLoader : ProviderLoaderBase
    {
        readonly string _assemblyDirectory;

        /// <summary>
        /// Initializes an instance of DirectoryProviderLoader with the specified 
        /// assembly directory.
        /// </summary>
        /// <param name="assemblyDirectory">The directory where the assemblies 
        /// are located.</param>
        public DirectoryProviderLoader(string assemblyDirectory)
        {
            if (string.IsNullOrEmpty(assemblyDirectory))
            {
                throw new ArgumentException("assemblyDirectory not set");
            }

            if (!Directory.Exists(assemblyDirectory))
            {
                throw new ArgumentException("assemblyDirectory does not exist");
            }

            _assemblyDirectory = assemblyDirectory;
        }

        /// <summary>
        /// Loads information regrading all providers from assemblies located in the specified 
        /// directory. All providers loaded using the Directory Provider Loader are enabled by 
        /// default. Remove a provider's assembly from the specified directory if you choose 
        /// to not load it.
        /// </summary>
        /// <returns>The loaded providers data.</returns>
        protected override IProviderData PerformLoad()
        {
            string[] dllFileNames = null;
            var providerDescriptors = new List<ProviderDescriptor>();


            dllFileNames = Directory.GetFiles(_assemblyDirectory, "*.dll");

            ICollection<Assembly> assemblies = new List<Assembly>(dllFileNames.Length);
            foreach (string dllFile in dllFileNames)
            {
                AssemblyName an = AssemblyName.GetAssemblyName(dllFile);
                Assembly assembly = Assembly.Load(an);
                assemblies.Add(assembly);
            }

            Type providerType = typeof(IProvider);
            ICollection<Type> providerTypes = new List<Type>();
            foreach (Assembly assembly in assemblies)
            {
                if (assembly != null)
                {
                    Type[] types = null;

                    try
                    {
                        types = assembly.GetTypes();
                    }
                    catch (ReflectionTypeLoadException ex)
                    {
                        types = ex.Types.Where(x => x != null).ToArray();
                    }

                    foreach (Type type in types)
                    {
                        if (type.IsInterface || type.IsAbstract)
                        {
                            continue;
                        }
                        else
                        {
                            if (type.GetInterface(providerType.FullName) != null)
                            {
                                providerTypes.Add(type);
                            }
                        }
                    }
                }
            }

            foreach (var exType in providerTypes)
            {
                providerDescriptors.Add(new ProviderDescriptor(
                    string.Empty, string.Empty, exType, null, true));
            }

            return new ProviderData(providerDescriptors, null);
        }
    }
}
