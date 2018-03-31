using Cannon.Utilities.Standard.Collections;
using Cannon.Utilities.Standard.Reflection;
using Cannon.Utilities.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    class DoubleLinkedListTests
    {
        [ReflectiveMethodLoading("DoubleLinkedList_A")]
        public static bool DoubleLinkedList1()
        {
            try
            {
                DoubleLinkedList<int> list = new DoubleLinkedList<int>();

                int[] elements = new int[]
                { 0,1,1,2,3,5,8,13,21,34 };
                foreach ( int i in elements )
                {
                    list.Add( i );
                }

                Assert.Equal( "DLL Count (Initial)", list.Count, elements.Length );

                for (int i = 0; i < elements.Length; i++)
                {
                    int current = list.RemoveHead();
                    Assert.Equal( "DLL Values", elements[ i ], current );
                }

                Assert.Equal( "DLL Count (Final)", 0, list.Count );

                return true;
            }
            catch ( TestFailureException )
            {
                return false;
            }
        }
        [ReflectiveMethodLoading( "DoubleLinkedList_B" )]
        public static bool DoubleLinkedList2()
        {
            try
            {
                DoubleLinkedList<int> list = new DoubleLinkedList<int>();

                int[] elements = new int[]
                { 0,1,1,2,3,5,8,13,21,34 };
                foreach ( int i in elements )
                {
                    list.Add( i );
                }

                Assert.Equal( "DLL Count (Initial)", list.Count, elements.Length );

                for ( int i = elements.Length - 1; i > 0; i-- )
                {
                    int current = list.RemoveTail();
                    Assert.Equal( "DLL Values", elements[ i ], current );
                }

                Assert.Equal( "DLL Count (Final)", 0, list.Count );

                return true;
            }
            catch ( TestFailureException )
            {
                return false;
            }
        }
    }
}
