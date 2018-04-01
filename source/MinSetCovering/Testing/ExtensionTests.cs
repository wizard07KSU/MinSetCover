using Cannon.Utilities.Standard;
using Cannon.Utilities.Standard.Reflection;
using Cannon.Utilities.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    public class ExtensionTests
    {
        [ReflectiveMethodLoading("BinarySearchTests")]
        public static bool BinarySearchTest1()
        {
            try
            {
                int[] data = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

                int actualIndex = data.BinarySearch( 1 );
                int expectedIndex = 0;
                Assert.Equal( "Binary Search (Nominal-A)", expectedIndex, actualIndex );

                actualIndex = data.BinarySearch( 5 );
                expectedIndex = 4;
                Assert.Equal( "Binary Search (Nominal-B)", expectedIndex, actualIndex );

                actualIndex = data.BinarySearch( 10 );
                expectedIndex = 9;
                Assert.Equal( "Binary Search (Nominal-C)", expectedIndex, actualIndex );

                actualIndex = data.BinarySearch( 11 );
                expectedIndex = -10;
                Assert.Equal( "Binary Search (Anormal-A)", expectedIndex, actualIndex );

                data = new int[] { 1, 2, 3, 4, 6, 7, 8, 9, 10 };
                actualIndex = data.BinarySearch( 5 );
                expectedIndex = -4;
                Assert.Equal( "Binary Search (Anormal-B)", expectedIndex, actualIndex );

                return true;
            }
            catch ( TestFailureException )
            {
                return false;
            }
        }
    }
}
