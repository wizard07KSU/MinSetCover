using Cannon.Utilities.Standard.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Cannon.Utilities.Testing
{
    public class TestManager
    {
        #region Properties
        private static readonly IDictionary<string, MethodInfo> testMethods;
        #endregion

        #region Constructors
        private static readonly object[] testFunctionArguments = new object[0];
        static TestManager()
        {
            IEnumerable<Assembly> assemblies = new Assembly[] { Assembly.GetEntryAssembly() };
            MethodSignature signature = new MethodSignature( new Type[0], typeof( bool) );
            testMethods = ReflectionManager.GetMethods( assemblies, signature );

        }
        #endregion

        #region Methods
        public static void RunAllTests()
        {
            if (testMethods.Count == 0)
            {
                Console.WriteLine( "No tests to run." );
                return;
            }
            int maxLengthOfNames = testMethods
                .Select( kvp => kvp.Key.Length )
                .Max();
            int testCount = testMethods.Count;
            int counter = 0;

            foreach ( var testMethod in testMethods )
            {
                Console.Write( "Executing {0}/{1} {2}: ",
                    ++counter,
                    testCount,
                    testMethod.Key.PadRight( maxLengthOfNames ) );
                bool? status = null;
                try
                {
                    status = (bool)testMethod.Value.Invoke( null, testFunctionArguments );
                }
                catch ( Exception e )
                {
                    ConsoleColor oldColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine( "EXCEPTION" );
                    Console.ForegroundColor = oldColor;
                    Console.WriteLine( "  Details: {0}", e );
                }
                if ( status.HasValue )
                {
                    ConsoleColor oldColor = Console.ForegroundColor;
                    if ( !status.Value )
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    Console.WriteLine( status.Value ? "PASS" : "FAIL" );
                    Console.ForegroundColor = oldColor;
                }
            }
        }
        #endregion
    }
}