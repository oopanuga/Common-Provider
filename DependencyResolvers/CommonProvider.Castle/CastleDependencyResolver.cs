using Castle.Windsor;
using CommonProvider.DependencyManagement;
using System;

namespace CommonProvider.Castle
{
    /// <summary>
    /// Represents an implementation of a dependency resolver that 
    /// uses Castle Windsor IOC to resolve an object instance.
    /// </summary>
    public class CastleDependencyResolver : IDependencyResolver
    {
        private IWindsorContainer _container;

        /// <summary>
        /// Initializes a new instance of CastleDependencyResolver
        /// with the specified Castle Windsor container 
        /// </summary>
        /// <param name="container"></param>
        public CastleDependencyResolver(IWindsorContainer container)
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
            return _container.Resolve<T>();
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
