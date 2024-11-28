namespace YChatApi.Services.Helpers
{
    public class AuthenticationException : Exception
    {
        public AuthenticationException() { }
        public AuthenticationException(string message) : base(message) { }
        public AuthenticationException(string message, Exception ex) : base(message, ex) { }
    }
}
