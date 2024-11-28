namespace YChatApi.Services.Helpers
{
    public class ValidationException : Exception
    {
        public ValidationException() { }
        public ValidationException(string message) : base(message) { }
        public ValidationException(string message, Exception ex) : base(message, ex) { }
    }
}
