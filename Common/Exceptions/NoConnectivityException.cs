using System;

namespace Tranzact.SearchFight.Common.Exceptions
{
    public class NoConnectivityException : Exception
    {
        public NoConnectivityException() { }

        public NoConnectivityException(string message)
            : base(message) { }

        public NoConnectivityException(string message, Exception inner)
            : base(message, inner) { }
    }
}
