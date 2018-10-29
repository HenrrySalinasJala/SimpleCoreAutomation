namespace Automation.Simple.Helpers
{
    using log4net;
    using System;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// This Utility class contains  methods to deal with assemblies.
    /// </summary>
    public static class AssemblyHelper
    {
        /// <summary>
        /// The logger instance.
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Gets the path of the given assembly name in the current App Domain.
        /// </summary>
        /// <returns>
        /// Returns the assembly path.
        /// </returns>
        public static string GetAssemblyPath(string assemblyName)
        {
            var asmPath = AppDomain.CurrentDomain.BaseDirectory;
            var asmFullPath = Path.Combine(asmPath, assemblyName);

            return asmFullPath;
        }

        /// <summary>
        /// Gets a Type given the  Class name, loading from the given assembly.
        /// </summary>
        /// <param name="className">
        /// string parameter, represents the Class name.
        /// </param>
        /// <param name="assemblyName"></param>
        /// <returns>
        /// Returns the Type of the given Class name
        /// </returns>
        public static Type GetTypeFromAssembly(string className, string assemblyName)
        {
            log.Info($"Get type: {className}, from assembly: {assemblyName}");
            var assemblyPath = GetAssemblyPath(assemblyName);
            var asm = Assembly.LoadFrom(assemblyPath);
            Type classType = GetTypeByTypeName(asm.GetTypes(), className);
            if (classType == null)
            {
                log.Error($"Unable to find type: {className} in assembly: {assemblyName}");
                throw new Exception("Unable to find the specified Class name" + className);
            }
            return classType;
        }


        /// <summary>
        /// Iterates over an array of Types to get the type that matches with the given Class Name.
        /// </summary>
        /// <param name="types">
        /// Type[] parameter, represents an array of Types.
        /// </param>
        /// <param name="className">
        /// string parameter, represents the Class name to look for into the array Types
        /// </param>
        /// <returns></returns>
        public static Type GetTypeByTypeName(Type[] types, string className)
        {
            foreach (Type type in types)
            {
                if (IsClassNameEqualsToTypeName(type, className))
                {
                    return type;
                }
            }

            return null;
        }

        /// <summary>
        /// Compares the given Type name and the given Class name.
        /// </summary>
        /// <param name="type">
        /// Type parameter, represents the Type to compare with.
        /// </param>
        /// <param name="className">
        /// string parameter, represents the Class Name.
        /// </param>
        /// <returns>
        /// Returns true if the Type name and the Class Name matches otherwise false.
        /// </returns>
        public static bool IsClassNameEqualsToTypeName(Type type, string className)
        {
            string blankSpace = " ";
            string typeName = type.Name.Replace(blankSpace, string.Empty).ToLower();
            string classNameWithoutSpaces = className.Replace(blankSpace, string.Empty).ToLower();

            return typeName == classNameWithoutSpaces;
        }

        /// <summary>
        /// Gets MethodInfo from the given generic method.
        /// </summary>
        /// <param name="classType">Type which the generic method belongs to.</param>
        /// <param name="methodName">string parameter, represents the generic method name.</param>
        /// <param name="genericMethodType">Type of the generic method.</param>
        /// <param name="parameters">Type[] parameter, represents the generic method arguments</param>
        /// <returns></returns>
        public static MethodInfo GetMethodInfoFromType(Type classType, string methodName,
            Type genericMethodType, Type[] parameters = null)
        {
            try
            {
                log.Info($"Get method information: {methodName} from type: {classType}");
                MethodInfo method = parameters == null
                    ? classType.GetMethod(methodName)
                    : classType.GetMethod(methodName, parameters);
                MethodInfo genericMethod = method.MakeGenericMethod(genericMethodType);
                return genericMethod;
            }
            catch (Exception error)
            {
                log.Error($"Unable to get method information: {methodName}, from type: {classType}");
                throw new Exception(string.Format("Unable to Get Method {0} info of {1} from {2}, [ERROR]: {3}",
                    methodName, classType.Name, genericMethodType.Name, error.Message));
            }
        }
    }
}
