using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cannon.Utilities.Testing;

namespace Testing
{
    class TestStuff
    {
        static void Main(string[] args)
        {
            TestManager.RunAllTests();

#if DEBUG
            Console.ReadLine();
#endif
        }
    }
}
