using Autofac;
using CommonProvider.DependencyManagement;
using System;

namespace CommonProvider.Autofac
{
    /// <summary>
    /// Represents an implementation of a dependency resolver that 
    /// uses Autofac IOC to resolve an object instance.
    /// </summary>
    public class AutofacDependencyResolver : IDependencyResolver
    {
        private IContainer _container;

        /// <summary>
        /// Initializes a new instance of AutofacDependencyResolver
        /// with the specified Autofac container 
        /// </summary>
        /// <param name="container"></param>
        public AutofacDependencyResolver(IContainer container)
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
