using System;

namespace CommonProvider.DependencyManagement
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
}
