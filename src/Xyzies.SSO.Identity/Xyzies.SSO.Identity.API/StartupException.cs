using System;

namespace Xyzies.SSO.Identity.API
{
    /// <summary>
    /// Use this type of exception when configuration is wrong
    /// </summary>
    public class StartupException : ApplicationException
    {
        /// <summary>
        /// Instantiate a new object of exception
        /// </summary>
        /// <param name="rootCause"></param>
        public StartupException(string rootCause)
            : base(rootCause)
        {

        }

        /// <summary>
        /// Throw a StartupException
        /// </summary>
        /// <param name="rootCause"></param>
        public static void Throw(string rootCause) => throw new StartupException(rootCause);
    }
}
