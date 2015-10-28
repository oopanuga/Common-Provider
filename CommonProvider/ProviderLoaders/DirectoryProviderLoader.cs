using CommonProvider.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CommonProvider.ProviderLoaders
{
    /// <summary>
    /// Represents an implementation of SimpleProviderLoaderBase that loads simple provider 
    /// types from assemblies. The assemblies are located in the specified directory. The 
    /// Directory Provider Loader does not require any configuration.
    /// </summary>
    public class DirectoryProviderLoader : SimpleProviderLoaderBase
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
        /// Loads simple provider types from assemblies located in the specified directory. 
        /// All  simple providers loaded using the Directory Provider Loader are enabled by 
        /// default. Remove a provider's assembly from the specified directory if you choose 
        /// to not load it.
        /// </summary>
        /// <returns>The loaded simple provider types.</returns>
        protected override IEnumerable<Type> PerformLoad()
        {
            string[] dllFileNames;

            dllFileNames = Directory.GetFiles(_assemblyDirectory, "*.dll");

            ICollection<Assembly> assemblies = new List<Assembly>(dllFileNames.Length);
            foreach (string dllFile in dllFileNames)
            {
                AssemblyName an = AssemblyName.GetAssemblyName(dllFile);
                Assembly assembly = Assembly.Load(an);
                assemblies.Add(assembly);
            }

            Type providerType = typeof(ISimpleProvider);
            ICollection<Type> providerTypes = null;
            foreach (Assembly assembly in assemblies)
            {
                if (assembly != null)
                {
                    Type[] types;

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
                            continue;

                        if (type.GetInterface(providerType.FullName) != null)
                        {
                            if (providerTypes == null)
                                providerTypes = new List<Type>();

                            providerTypes.Add(type);
                        }
                    }
                }
            }

            return providerTypes;
        }
    }
}
