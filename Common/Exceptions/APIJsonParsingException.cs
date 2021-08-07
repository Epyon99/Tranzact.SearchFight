using System;

namespace Tranzact.SearchFight.Common.Exceptions
{
    public class APIJsonParsingException : Exception
    {
        public APIJsonParsingException() { }

        public APIJsonParsingException(string message)
            : base(message) { }

        public APIJsonParsingException(string message, Exception inner)
            : base(message, inner) { }
    }
}
