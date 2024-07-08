namespace WayraWasi.Helper
{
    internal class ForeignKeyException : Exception
    {
        public ForeignKeyException()
        {
        }

        public ForeignKeyException(string? message) : base(message)
        {
        }

        public ForeignKeyException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}