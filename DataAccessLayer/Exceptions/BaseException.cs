using System;

namespace DataAccessLayer
{
    public class BaseException : Exception
    {
        public BaseException(string message) : base(message)
        {
        }
    }
}
