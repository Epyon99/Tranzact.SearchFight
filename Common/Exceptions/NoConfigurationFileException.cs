using System;

namespace Tranzact.SearchFight.Common.Exceptions
{
    public class NoConfigurationFileException : Exception
    {
        public NoConfigurationFileException() { }

        public NoConfigurationFileException(string message)
            : base(message) { }

        public NoConfigurationFileException(string message, Exception inner)
            : base(message, inner) { }
    }
}
