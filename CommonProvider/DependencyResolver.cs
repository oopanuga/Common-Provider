using System;

namespace CommonProvider
{
    /// <summary>
    /// Represents the base interface for a dependency resolver. It provides 
    /// a means to instantiate an object and resolve its dependencies.
    /// </summary>
    public interface IDependencyResolver : IDisposable
    {
        /// <summary>
        /// Resolves an instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type to resolve.</typeparam>
        /// <returns>An instance of the type.</returns>
        T Resolve<T>();
    }

    /// <summary>
    /// Represents the default implementation of a dependency resolver. 
    /// It uses Activator.CreateInstance() to resolve an object instance.
    /// </summary>
    public class DependencyResolver : IDependencyResolver
    {
        static IDependencyResolver _dependencyResolver;

        /// <summary>
        /// The main constructor for DependencyResolver.
        /// </summary>
        internal DependencyResolver()
        {
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

        /// <summary>
        /// Resolves an instance of the specified type using 
        /// Activator.CreateInstance().
        /// </summary>
        /// <typeparam name="T">The type to resolve.</typeparam>
        /// <returns>An instance of the type.</returns>
        public T Resolve<T>()
        {
            try
            {
                return (T)Activator.CreateInstance(typeof(T));
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        /// <summary>
        /// Gets the current.
        /// </summary>
        /// <value>
        /// The current.
        /// </value>
        internal static IDependencyResolver Current
        {
            get
            {
                if (_dependencyResolver != null)
                    return _dependencyResolver;
                else
                {
                    return new DependencyResolver();
                }
            }
        }

        /// <summary>
        /// Releases and frees up resources.
        /// </summary>
        public void Dispose()
        {

        }
    }
}
