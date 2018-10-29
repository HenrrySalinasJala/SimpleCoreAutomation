namespace Automation.Simple.Helpers.Reflection
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Helpers for Delegate factory.
    /// </summary>
    public static class DelegateHelper
    {
        /// <summary>
        /// Gets the delegate return type.
        /// </summary>
        /// <typeparam name="TDelegate">The delegate.</typeparam>
        /// <returns>The return type.</returns>
        public static Type GetDelegateReturnType<TDelegate>() where TDelegate : class
        {
            var invokeMethod = typeof(TDelegate).GetTypeInfo().GetMethod("Invoke");
            return invokeMethod.ReturnType;
        }

        /// <summary>
        /// Checks if the delegate has to returns a type or not (void).
        /// </summary>
        /// <typeparam name="TDelegate">The delegate</typeparam>
        /// <param name="method">The method.</param>
        public static void CheckDelegateReturnType<TDelegate>(MethodInfo method) where TDelegate : class
        {
            var delegateReturnType = GetDelegateReturnType<TDelegate>();
            if (delegateReturnType != method.ReturnType)
            {
                if (method.ReturnType == typeof(void))
                {
                    throw new ArgumentException(string.Format(
                        "TDelegate return type is non-void and method found in {0} have return type of void.", 
                        method.DeclaringType.FullName));
                }
                if (delegateReturnType == typeof(void))
                {
                    throw new ArgumentException(string.Format(
                        "TDelegate return type is void and method found in {0} have return type of {1}.", 
                        method.DeclaringType.FullName, method.ReturnType.FullName));
                }
            }
        }
    }
}
