using System;
using System.Runtime.Serialization;

namespace Cannon.Utilities.Testing
{
    public class TestFailureException : Exception
    {
        public TestFailureException( string testCase ) :
            base( $"Test case {testCase} failed." ) { }
    }
}