using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cannon.Utilities.Standard.Reflection
{
    public static class ReflectionManager
    {
        #region Load Files
        public static ICollection<Assembly> LoadFiles( string directoryName )
        {
            List<Assembly> assemblies = new List<Assembly>();
            foreach ( string iFileName in Directory.EnumerateFiles(directoryName, "*.dll"))
            {
                assemblies.Add(Assembly.LoadFile(iFileName));
            }
            return assemblies;
        }
        #endregion

        #region Load Types
        /// <summary>
        /// Gets constructors from throughout the current app domain. Loaded types are not constrained by type.
        /// </summary>
        public static IDictionary<string, ConstructorInfo> GetConstructors()
        {
            return GetConstructors<object>( AppDomain.CurrentDomain.GetAssemblies() );
        }

        /// <summary>
        /// Gets a mapping of public names to constructors from all types from 
        /// throughout the current App Domain. To be loaded, a type must inherit 
        /// from <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The base type for any loaded type.
        /// </typeparam>
        public static IDictionary<string, ConstructorInfo> GetConstructors<T>()
        {
            return GetConstructors<T>( AppDomain.CurrentDomain.GetAssemblies() );
        }

        /// <summary>
        /// Gets a mapping of public names to constructors for all types in the 
        /// provided assemblies that inherit from <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The base type for all types.
        /// </typeparam>
        /// <param name="assemblies">
        /// The set of assemblies to load constructors from.
        /// </param>
        /// <returns></returns>
        public static IDictionary<string, ConstructorInfo> GetConstructors<T>( IEnumerable<Assembly> assemblies )
        {
            return GetConstructors<T>( assemblies, new Type[0] );
        }

        /// <summary>
        /// Gets a mapping of public names to constructors for the provided assemblies
        /// for types that inherit from <typeparamref name="T"/> and where the constructor
        /// has a signature matting <paramref name="constructorSignature"/>. If a constructor
        /// with the provided signature does not exist, it will be skipped.
        /// </summary>
        /// <typeparam name="T">
        /// The base type all applicable types must inherit from.
        /// </typeparam>
        /// <param name="assemblies">
        /// the set of assemblies to search.
        /// </param>
        /// <param name="constructorSignature">
        /// The required signature for a constructor.
        /// </param>
        /// <returns></returns>
        public static IDictionary<string, ConstructorInfo> GetConstructors<T>( IEnumerable<Assembly> assemblies, IEnumerable<Type> constructorSignature)
        {
            Type[] lConstructorSignature = constructorSignature.ToArray();
            Dictionary<string, ConstructorInfo> constructors = new Dictionary<string, ConstructorInfo>();

            foreach ( Assembly iAssembly in assemblies )
            {
                IEnumerable<Type> types = iAssembly.GetTypes()
                    .Where( t => t.IsSubclassOf( typeof(T) ) );

                foreach ( Type iType in types )
                {
                    ReflectiveClassLoadingAttributeAttribute attribute = iType.GetCustomAttribute<ReflectiveClassLoadingAttributeAttribute>();
                    if ( attribute == null )
                    {
                        continue;
                    }

                    ConstructorInfo constructor = iType.GetConstructor( lConstructorSignature );
                    if ( constructor == null )
                    {
                        continue;
                    }

                    constructors.Add( attribute.Name, constructor );
                }
            }

            return constructors;
        }
        #endregion

        #region Load Methods
        /// <summary>
        /// Gets a mapping of human-readable names to methods for all types in 
        /// the provided set of assemblies where the method has the provided signature.
        /// </summary>
        /// <param name="assemblies"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        public static IDictionary<string, MethodInfo> GetMethods(IEnumerable<Assembly> assemblies, MethodSignature signature)
        {
            return GetMethods<object>( assemblies, signature );
        }

        /// <summary>
        /// Gets a mapping of human-readable names to methods for all types in
        /// the provided set of assemblies for all types that inherit from
        /// <typeparamref name="T"/> and when the method has the provided signature.
        /// </summary>
        /// <typeparam name="T">
        /// The base type that the defining type for any applicable methods must derive from.
        /// </typeparam>
        /// <param name="assemblies">
        /// The set of assemblies to search.
        /// </param>
        /// <param name="methodSignature">
        /// The signature of the methods to load.
        /// </param>
        /// <returns></returns>
        public static IDictionary<string, MethodInfo> GetMethods<T>( IEnumerable<Assembly> assemblies, MethodSignature signature )
        {
            Dictionary<string, MethodInfo> methods = new Dictionary<string, MethodInfo>();

            foreach (Assembly iAssembly in assemblies)
            {
                IEnumerable<MethodInfo> iMethods = iAssembly.GetTypes()
                    .Where( t => t.IsSubclassOf( typeof(T) ) )
                    .SelectMany( t => t.GetMethods());

                foreach (MethodInfo iMethod in iMethods )
                {
                    ReflectiveMethodLoadingAttribute attribute = 
                        iMethod.GetCustomAttribute<ReflectiveMethodLoadingAttribute>();
                    if ( attribute == null || iMethod.IsConstructor )
                    {
                        continue;
                    }

                    if ( signature != iMethod )
                    {
                        continue;
                    }

                    methods.Add( attribute.Name, iMethod );
                }
            }

            return methods;
        }
        #endregion
    }
}
