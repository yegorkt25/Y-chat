using Microsoft.IdentityModel.Tokens;
using YChatApi.DTOs;
using YChatApi.Entities;
using YChatApi.Entities.Repositories;
using YChatApi.Services.Helpers;

namespace YChatApi.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly JwtTokenHelper _jwtTokenHelper;
        private readonly PasswordValidationHelper _passwordValidationHelper;

        public AuthenticationService(IUsersRepository usersRepository, JwtTokenHelper jwtTokenHelper, PasswordValidationHelper passwordValidationHelper)
        {
            _usersRepository = usersRepository;
            _jwtTokenHelper = jwtTokenHelper;
            _passwordValidationHelper = passwordValidationHelper;
        }

        public async Task<string> Login(UserDto dto)
        {
            var user = await _usersRepository.GetUserByUsernameAsync(dto.Username);
            if (user == null || !VerifyPassword(dto.Password.ToLower(), user.PasswordHash))
            {
                throw new AuthenticationException("Either user doesn't exist or password is wrong");
            }

            var token = _jwtTokenHelper.GenerateToken(user.Username);
            return token;
        }

        public async Task Register(UserDto dto)
        {
            var username = await _usersRepository.GetUserByUsernameAsync(dto.Username);

            if (username != null)
            {
                throw new ValidationException("Username isn't unique");
            }

            if (!_passwordValidationHelper.ValidatePassword(dto.Password))
            {
                throw new ValidationException("Password should follow the rules");
            }
            var user = new User() { Username = dto.Username, PasswordHash = HashPassword(dto.Password.ToLower()), CreatedAt = DateTime.UtcNow };
            await _usersRepository.AddUserAsync(user);
        }

        private static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        }

        private static bool VerifyPassword(string password, string hashPassword)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, hashPassword);
        }
    }
}
