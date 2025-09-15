using System;

namespace NameSorterClasses
{
    public class NameSorterFileException : Exception
    {
        public NameSorterFileException() { }

        public NameSorterFileException(string message) : base(message) { }

        public NameSorterFileException(string message, Exception innerException) : base(message, innerException) { }
    }
}
