using System;

namespace Xyzies.SSO.Identity.Services.Exceptions
{
    public class AccessException : ApplicationException
    {
        public AccessException() : base() { }
        public AccessException(string message) : base(message) { }
    }
}
