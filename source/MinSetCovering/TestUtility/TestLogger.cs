using Cannon.Utilities.Standard.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cannon.Utilities.Testing
{
    internal class TestLogger : Logger
    {
        #region Constructors
        /// <summary>
        /// Creates a new logger that directs all output to one log file, and only
        /// exception messages to the console.
        /// </summary>
        public TestLogger()
            : base( new LogTarget( "allLog.log", true, LOGGING_LEVEL.ALL),
                    new LogTarget( Console.OpenStandardOutput(), LOGGING_LEVEL.EXCEPTION ) )
        {

        }
        #endregion

        #region Methods
        public void Pass( object expectedValue )
        {
            base.Info( "PASS: Expected and found {0}", expectedValue );
        }

        public void Fail( object expected, object actual )
        {
            base.Error( "FAIL: Expected [{0}] but found [{1}]", expected, actual );
        }
        #endregion
    }
}
