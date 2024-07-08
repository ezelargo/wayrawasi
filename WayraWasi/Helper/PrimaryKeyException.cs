namespace WayraWasi.Helper
{
    internal class PrimaryKeyException : Exception
    {
        public PrimaryKeyException()
        {
        }

        public PrimaryKeyException(string? message) : base(message)
        {
        }

        public PrimaryKeyException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}