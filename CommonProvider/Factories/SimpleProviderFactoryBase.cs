using CommonProvider.Exceptions;
using CommonProvider.Helpers;
using System;
using System.Reflection;

namespace CommonProvider.Factories
{
    /// <summary>
    /// Represents the base class for a Simple Provider Factory. 
    /// It provides a means to create a Simple Provider.
    /// </summary>
    public abstract class SimpleProviderFactoryBase
    {
        /// <summary>
        /// Instantiates a Simple Provider based on the specified type.
        /// </summary>
        /// <typeparam name="T">The type of simple provider to instantiate.</typeparam>
        /// <returns>An object instance based on the specified type.</returns>
        protected abstract T Create<T>() where T : ISimpleProvider;

        /// <summary>
        /// Creates a Simple Provider based on the specified type.
        /// </summary>
        /// <typeparam name="T">The type to cast the simple provider to.</typeparam>
        /// <param name="providerType">The type of simple provider.</param>
        /// <returns>The created Provider.</returns>
        public T Create<T>(Type providerType) where T : ISimpleProvider
        {
            try
            {
                if (providerType == null)
                {
                    throw new ArgumentNullException("providerType");
                }

                Type type = typeof(T);

                if (!type.IsAssignableFrom(providerType))
                {
                    throw new CreateProviderException(
                                            string.Format("{0} should be assignable from {1}",
                                            type.Name,
                                            providerType.Name
                                            ));
                }

                var instance = GenericMethodInvoker.Invoke(
                                            this,
                                            "Create",
                                            providerType,
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
                        providerType.Name));
                }

                return (T)instance;
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
