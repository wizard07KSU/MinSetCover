using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cannon.Utilities.Standard.Logging
{
    /// <summary>
    /// Specifies a target for logging.
    /// </summary>
    public class LogTarget
    {
        #region Properties
        /// <summary>
        /// The stream to write log messages to.
        /// </summary>
        public Stream Target { get; protected set; }

        public LOGGING_LEVEL Level { get; protected set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new log target using the provided stream.
        /// </summary>
        /// <param name="stream"></param>
        public LogTarget( Stream stream, LOGGING_LEVEL level )
        {
            this.Target = stream;
            this.Level = level;
        }

        /// <summary>
        /// Creates a new log file.
        /// </summary>
        /// <param name="fileName">
        /// The name of the log file to create.
        /// </param>
        /// <param name="overwrite">
        /// Whether this function should overwrite the file already present. If
        /// <see langword="false"/> and a file with the given name already exists,
        /// an exception will be thrown.
        /// </param>
        public LogTarget( string fileName, bool overwrite, LOGGING_LEVEL level )
        {
            FileInfo file = new FileInfo( fileName );
            if ( file.Exists )
            {
                if (overwrite)
                {
                    file.Delete();
                }
                else
                {
                    throw new Exception( "Target file already exists!" );
                }
            }

            this.Level = level;
            this.Target = file.OpenWrite();
        }
        #endregion
    }
}
