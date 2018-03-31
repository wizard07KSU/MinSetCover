using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cannon.Utilities.Standard.Logging
{
    /// <summary>
    /// Logging levels.
    /// </summary>
    public enum LOGGING_LEVEL
    {
        /// <summary>
        /// No logging at all.
        /// </summary>
        NONE      = 0,
        /// <summary>
        /// Log only exception messages.
        /// </summary>
        EXCEPTION = 1,
        /// <summary>
        /// Log only error messages.
        /// </summary>
        ERROR     = 2,
        /// <summary>
        /// Log only warning messages.
        /// </summary>
        WARNING   = 4,
        /// <summary>
        /// Log only informational messages.
        /// </summary>
        INFO      = 8,
        /// <summary>
        /// Log only debug messages.
        /// </summary>
        DEBUG     = 16,
        /// <summary>
        /// Log all messages.
        /// </summary>
        ALL       = EXCEPTION | ERROR | WARNING | INFO | DEBUG,
        /// <summary>
        /// The default level of logging. Logs Exceptions, errors, and 
        /// informational messages.
        /// </summary>
        DEFAULT   = EXCEPTION | ERROR | INFO,

    }
}
