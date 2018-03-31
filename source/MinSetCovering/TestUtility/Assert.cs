using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cannon.Utilities.Testing
{
    public static class Assert
    {
        private static TestLogger testLogger = new TestLogger();

        static Assert()
        {

        }

        public static bool Equal( string testCase, object expected, object actual)
        {
            if (object.ReferenceEquals( expected, null))
            {
                if (object.ReferenceEquals(actual, null))
                {
                    testLogger.Pass( null );
                    return true;
                }
            }
            if ( expected.Equals(actual) )
            {
                testLogger.Pass( expected );
                return true;
            }
            else
            {
                testLogger.Fail( expected, actual );
                throw new TestFailureException( testCase );
            }
        }
    }
}
