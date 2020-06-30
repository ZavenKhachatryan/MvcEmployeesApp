namespace DataAccessLayer
{
    public class DuplicateException : BaseException
    {
        public DuplicateExceptionType Type { get; private set; }
        public DuplicateException(string message, DuplicateExceptionType duplicateExceptionType) : base(message)
        {
            Type = duplicateExceptionType;
        }
    }
    public enum DuplicateExceptionType
    {
        Email,
        Phone
    }
}
