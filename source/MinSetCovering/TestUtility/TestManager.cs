using Cannon.Utilities.Standard.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Testing
{
    internal class TestManager
    {
        #region Properties
        #endregion

        #region Constructors
        private static readonly object[] testFunctionArguments = new object[0];
        static TestManager()
        {
            IEnumerable<Assembly> assemblies = new Assembly[] { Assembly.GetEntryAssembly() };
            MethodSignature signature = new MethodSignature( new Type[0], typeof( bool) );
            IDictionary<string, MethodInfo> testMethods = ReflectionManager.GetMethods( assemblies, signature );

            int maxLengthOfNames = testMethods
                .Select( kvp => kvp.Key.Length )
                .Max();
            int testCount = testMethods.Count;
            int counter = 0;

            foreach ( var testMethod in testMethods )
            {
                Console.Write( "Executing {0}/{1} {2}: ",
                    testCount,
                    ++counter,
                    testMethod.Key.PadRight( maxLengthOfNames ) );
                bool? status = null;
                try
                {
                    status = (bool)testMethod.Value.Invoke( null, testFunctionArguments );
                }
                catch( Exception e)
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
                    if (!status.Value)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    Console.WriteLine( status.Value ? "PASS" : "FAIL" );
                    Console.ForegroundColor = oldColor;
                }
            }
        }
        #endregion

        #region Methods
        internal static void RunAllTests()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}