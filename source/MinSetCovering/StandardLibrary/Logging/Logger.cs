using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cannon.Utilities.Standard.Logging
{
    public class Logger
    {
        #region Properties
        private readonly IEnumerable<LogTarget> logTargets;

        public IReadOnlyCollection<LogTarget> LogTargets
        {
            get
            {
                return this.logTargets.AsReadOnly();
            }
        }
        #endregion

        #region Constructors
        public Logger(params LogTarget[] targets)
        {
            this.logTargets = targets;
        }
        #endregion

        #region Methods
        private void WriteMessage( string message, LOGGING_LEVEL level )
        {
            UnicodeEncoding encoding = new UnicodeEncoding();
            byte[] bytes = encoding.GetBytes((message + Environment.NewLine));

            foreach ( LogTarget target in logTargets )
            {
                if ( ( target.Level & level ) == level )
                {
                    target.Target.Write( bytes, 0, bytes.Length );
                }
            }
        }

        private string GetPrefixString( LOGGING_LEVEL level )
        {
            return string.Format( "[{0}] | {1} | ",
                DateTime.Now,
                level );
        }

        public void Info( string message, params object[] args )
        {
            WriteMessage(
                GetPrefixString( LOGGING_LEVEL.INFO ) +
                    string.Format( message, args ),
                LOGGING_LEVEL.INFO );
        }

        public void Error( string message, params object[] args )
        {
            WriteMessage(
                GetPrefixString( LOGGING_LEVEL.ERROR ) +
                    string.Format( message, args ),
                LOGGING_LEVEL.ERROR );
        }
        #endregion
    }
}
