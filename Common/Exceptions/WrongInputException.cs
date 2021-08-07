using System;

namespace Tranzact.SearchFight.Common.Exceptions
{
    public class WrongInputException : Exception
    {
        public WrongInputException() { }

        public WrongInputException(string message)
            : base(message) { }

        public WrongInputException(string message, Exception inner)
            : base(message, inner) { }
    }
}
