using CommonProvider.DependencyManagement;
using Microsoft.Practices.Unity;
using System;

namespace CommonProvider.DependencyResolvers
{
    /// <summary>
    /// Represents an implementation of a dependency resolver that 
    /// uses Unity IOC to resolve an object instance.
    /// </summary>
    public class UnityDependencyResolver : IDependencyResolver
    {
        private IUnityContainer _container;

        /// <summary>
        /// Initializes a new instance of UnityDependencyResolver
        /// with the specified Unity container 
        /// </summary>
        /// <param name="container"></param>
        public UnityDependencyResolver(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this._container = container;
        }

        /// <summary>
        /// Resolves an instance of the specified type
        /// </summary>
        /// <typeparam name="T">The type to resolve.</typeparam>
        /// <returns>An instance of the type.</returns>
        public T Resolve<T>()
        {
            try
            {
                return (T)_container.Resolve(typeof(T));
            }
            catch (ResolutionFailedException)
            {
                return default(T);
            }
        }

        /// <summary>
        /// Releases and frees up resources.
        /// </summary>
        public void Dispose()
        {
            _container.Dispose();
        }
    }
}
