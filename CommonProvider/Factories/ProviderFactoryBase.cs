using CommonProvider.Data;
using CommonProvider.Exceptions;
using CommonProvider.Helpers;
using System;
using System.Reflection;

namespace CommonProvider.Factories
{
    /// <summary>
    /// Represents the base class for a Provider Factory. 
    /// It provides a means to create a Provider.
    /// </summary>
    public abstract class ProviderFactoryBase
    {
        /// <summary>
        /// Instantiates a Provider based on the specified type.
        /// </summary>
        /// <typeparam name="T">The type of provider to instantiate.</typeparam>
        /// <returns>An object instance based on the specified type.</returns>
        protected abstract T Create<T>();

        /// <summary>
        /// Creates a Provider based on the specified type.
        /// </summary>
        /// <typeparam name="T">The type to cast the provider to.</typeparam>
        /// <param name="providerDescriptor">Holds information about the provider to be created.</param>
        /// <returns>The created Provider.</returns>
        public T Create<T>(IProviderDescriptor providerDescriptor) where T : IProvider
        {
            try
            {
                if (providerDescriptor == null)
                {
                    throw new ArgumentNullException("providerDescriptor");
                }

                Type type = typeof(T);

                if (type.IsSubclassOf(typeof(IProvider)))
                {
                    throw new ArgumentException(
                        string.Format("{0} should inherit from IProvider", type.Name));
                }

                if (providerDescriptor == null)
                {
                    throw new ArgumentException("providerDescriptor not set");
                }

                if (!type.IsAssignableFrom(providerDescriptor.ProviderType))
                {
                    throw new CreateProviderException(
                                            string.Format("{0} should be assignable from {1}",
                                            type.Name,
                                            providerDescriptor.ProviderType.Name
                                            ));
                }

                var instance = GenericMethodInvoker.Invoke(
                                            this,
                                            "Create",
                                            providerDescriptor.ProviderType,
                                            new object[] { },
                                            BindingFlags.NonPublic | BindingFlags.Instance
                                            );

                //TODO: Add documentation link to exception message
                if (instance == null)
                {
                    throw new CreateProviderException(
                        string.Format(
                        "Could not create instance of type {0}. If you've got a Provider" +
                        "implementation that exposes constructor arguments then please consider" +
                        "using any of the existing dependency resolvers or write your" +
                        "own implementation(see documentation for details)",
                        providerDescriptor.ProviderType.Name));
                }

                var provider = (IProvider)instance;
                provider.Group = providerDescriptor.ProviderGroup;
                provider.Name = providerDescriptor.ProviderName;
                provider.Settings = providerDescriptor.ProviderSettings;

                return (T)(object)instance;
            }
            catch (Exception ex)
            {
                if (!(ex is CreateProviderException))
                {
                    throw new CreateProviderException(
                        "Error creating provider",
                        ex);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
