using System;

namespace CommonProvider.DependencyManagement
{
    /// <summary>
    /// Represents a Service that sets or internally gets a dependency resolver. 
    /// It uses a single instance for resolving types once set.
    /// </summary>
    public static class DependencyResolverService
    {
        static IDependencyResolver _dependencyResolver;

        /// <summary>
        /// Gets the dependency resolver.
        /// </summary>
        /// <returns></returns>
        internal static IDependencyResolver GetResolver()
        {
            if (_dependencyResolver == null)
            {
                _dependencyResolver = new DependencyResolver();
            }

            return _dependencyResolver;
        }

        /// <summary>
        /// Sets the dependency resolver.
        /// </summary>
        /// <param name="dependencyResolver">The dependency resolve to be set.</param>
        public static void SetResolver(IDependencyResolver dependencyResolver)
        {
            if (dependencyResolver == null)
                throw new ArgumentNullException("dependencyResolver");

            _dependencyResolver = dependencyResolver;
        }
    }
}
