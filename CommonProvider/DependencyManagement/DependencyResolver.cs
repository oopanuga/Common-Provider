using System;

namespace CommonProvider.DependencyManagement
{
    /// <summary>
    /// Represents the default implementation of a dependency resolver. 
    /// It uses Activator.CreateInstance() to resolve an object instance.
    /// </summary>
    public class DependencyResolver: IDependencyResolver
    {
        /// <summary>
        /// The main constructor for DependencyResolver.
        /// </summary>
        internal DependencyResolver()
        {
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
        /// Releases and frees up resources.
        /// </summary>
        public void Dispose()
        {
            
        }
    }
}
