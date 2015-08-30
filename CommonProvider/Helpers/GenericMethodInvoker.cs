using System;
using System.Reflection;

namespace CommonProvider.Helpers
{
    /// <summary>
    /// Provides a mean to dyncamically invoke a generic method.
    /// </summary>
    internal static class GenericMethodInvoker
    {
        /// <summary>
        /// Dynamically invokes a generic method.
        /// </summary>
        /// <param name="obj">The object that has the method.</param>
        /// <param name="methodName">The name of the method.</param>
        /// <param name="genericType">The generic type.</param>
        /// <param name="parameters">The method parameters.</param>
        /// <param name="bindingFlags">Specifies flags that control binding and the way in 
        /// which the search for members and types is conducted by reflection.</param>
        /// <returns>Object returned from the method call.</returns>
        internal static object Invoke(object obj, string methodName, 
            Type genericType, object[] parameters, BindingFlags? bindingFlags = null)
        {
            MethodInfo method = null;
            if(bindingFlags == null)
            {
                method = obj.GetType().GetMethod(methodName);
            }
            else
            {
                method = obj.GetType().GetMethod(methodName, bindingFlags.Value);
            }

            method = method.MakeGenericMethod(new Type[] { genericType });
            return method.Invoke(obj, parameters);
        }
    }
}
