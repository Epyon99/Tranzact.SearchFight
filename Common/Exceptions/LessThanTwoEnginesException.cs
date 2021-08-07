using System;

namespace Tranzact.SearchFight.Common.Exceptions
{
    public class LessThanTwoEnginesException : Exception
    {
        public LessThanTwoEnginesException() { }

        public LessThanTwoEnginesException(string message)
            : base(message) { }

        public LessThanTwoEnginesException(string message, Exception inner)
            : base(message, inner) { }
    }
}
