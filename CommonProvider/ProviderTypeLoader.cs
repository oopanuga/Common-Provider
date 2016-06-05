using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommonProvider.Exceptions;

namespace CommonProvider
{
    /// <summary>
    /// Represents a class that loads zero config provider types from assemblies.
    /// The assemblies are located in the specified directory. 
    /// </summary>
    public class ProviderTypeLoader
    {
        readonly string _assemblyDirectory;

        /// <summary>
        /// Initializes an instance of ZeroXmlConfigSource with the specified 
        /// assembly directory.
        /// </summary>
        /// <param name="assemblyDirectory">The directory where the assemblies 
        /// are located.</param>
        public ProviderTypeLoader(string assemblyDirectory)
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
        /// Loads provider types from an assembly directory.
        /// </summary>
        /// <returns>The loaded provider types.</returns>
        public IEnumerable<Type> Load()
        {
            try
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
            catch (Exception ex)
            {
                throw new LoadProviderTypeException("Error loading provider types", ex);
            }
        }
    }
}
