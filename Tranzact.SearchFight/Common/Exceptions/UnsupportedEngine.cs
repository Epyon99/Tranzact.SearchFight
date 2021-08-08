using System;

namespace Tranzact.SearchFight.Common.Exceptions
{
    public class UnsupportedEngine : Exception
    {
        public UnsupportedEngine() { }

        public UnsupportedEngine(string message)
            : base(message) { }

        public UnsupportedEngine(string message, Exception inner)
            : base(message, inner) { }
    }
}
