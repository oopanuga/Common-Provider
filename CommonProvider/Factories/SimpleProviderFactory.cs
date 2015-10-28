

using CommonProvider.DependencyManagement;

namespace CommonProvider.Factories
{
    /// <summary>
    /// Represents the default implementation of a Simple Provider Factory. 
    /// It provides a means to create a Simple Provider Factory.
    /// </summary>
    public class SimpleProviderFactory : SimpleProviderFactoryBase
    {
        /// <summary>
        /// Instantiates a Simple Provider based on the specified type.
        /// </summary>
        /// <typeparam name="T">The type of simple provider to instantiate.</typeparam>
        /// <returns>An object instance based on the specified type.</returns>
        protected override T Create<T>()
        {
            // use default dependency resolver if not set
            var dependencyResolver = DependencyResolverService.GetResolver();

            return dependencyResolver.Resolve<T>();
        }
    }
}
