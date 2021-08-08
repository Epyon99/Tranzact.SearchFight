using System;

namespace Tranzact.SearchFight.Common.Exceptions
{
    public class NoParametersReceived : Exception
    {
        public NoParametersReceived() { }

        public NoParametersReceived(string message)
            : base(message) { }

        public NoParametersReceived(string message, Exception inner)
            : base(message, inner) { }
    }
}
