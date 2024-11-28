using System.Text.RegularExpressions;

namespace YChatApi.Services.Helpers
{
    public class PasswordValidationHelper
    {
        public bool ValidatePassword(string password)
        {
            if (password.Length < 8)
            {
                //Console.WriteLine("Password must be at least 8 characters.");
                return false;
            }

            if (!Regex.IsMatch(password, @"[a-z]"))
            {
                //Console.WriteLine("Password must contain at one letter.");
                return false;
            }

            if (!Regex.IsMatch(password, @"\d"))
            {
                //Console.WriteLine("Password must contain at least one digit.");
                return false;
            }

            return true;
        }
    }
}
