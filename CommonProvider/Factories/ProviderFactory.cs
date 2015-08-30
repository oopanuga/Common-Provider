using CommonProvider.DependencyManagement;
namespace CommonProvider.Factories
{
    /// <summary>
    /// Represents the default implementation of a Provider List Factory. 
    /// It provides a means to create a Provider List.
    /// </summary>
    public class ProviderFactory : ProviderFactoryBase
    {
        /// <summary>
        /// Instantiates a Provider based on the specified type.
        /// </summary>
        /// <typeparam name="T">The type of provider to instantiate.</typeparam>
        /// <returns>An object instance based on the specified type.</returns>
        protected override T Create<T>()
        {
            // use default dependency resolver if not set
            var dependencyResolver = DependencyResolverService.GetResolver();

            var type = typeof(T);

            return dependencyResolver.Resolve<T>();
        }
    }
}
