using System;

namespace Exceptions
{
    public class ExistException : Exception
    {
        public ExistException(string message) : base(message)
        {

        }
    }
}
