using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommonProvider.Exceptions;

namespace CommonProvider.ProviderLoaders
{
    /// <summary>
    /// Represents a class that loads zero config provider types from assemblies.
    /// The assemblies are located in the specified directory. 
    /// </summary>
    public class ZeroConfigProviderLoader
    {
        readonly string _assemblyDirectory;

        /// <summary>
        /// Initializes an instance of ZeroConfigProviderLoader with the specified 
        /// assembly directory.
        /// </summary>
        /// <param name="assemblyDirectory">The directory where the assemblies 
        /// are located.</param>
        public ZeroConfigProviderLoader(string assemblyDirectory)
        {
            if (string.IsNullOrEmpty(assemblyDirectory))
            {
                throw new ArgumentException("assemblyDirectory not set");
            }

            if (!System.IO.Directory.Exists(assemblyDirectory))
            {
                throw new ArgumentException("assemblyDirectory does not exist");
            }

            _assemblyDirectory = assemblyDirectory;
        }

        /// <summary>
        /// Loads zero config provider types.
        /// </summary>
        /// <returns>The loaded zero config providers types.</returns>
        public IEnumerable<Type> Load()
        {
            try
            {
                var providerTypes = PerformLoad();

                if (providerTypes == null || !providerTypes.Any())
                {
                    throw new ProviderLoadException(
                        "providerTypes not set");
                }

                return providerTypes;
            }
            catch (Exception ex)
            {
                if (!(ex is ProviderLoadException))
                {
                    throw new ProviderLoadException(
                        "Error loading providerTypes", ex);
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Loads zero config provider types from assemblies located in the specified directory. 
        /// </summary>
        /// <returns>The loaded zero config provider types.</returns>
        private IEnumerable<Type> PerformLoad()
        {
            string[] dllFileNames;

            dllFileNames = System.IO.Directory.GetFiles(_assemblyDirectory, "*.dll");

            ICollection<Assembly> assemblies = new List<Assembly>(dllFileNames.Length);
            foreach (string dllFile in dllFileNames)
            {
                AssemblyName an = AssemblyName.GetAssemblyName(dllFile);
                Assembly assembly = Assembly.Load(an);
                assemblies.Add(assembly);
            }

            Type providerType = typeof(IZeroConfigProvider);
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
