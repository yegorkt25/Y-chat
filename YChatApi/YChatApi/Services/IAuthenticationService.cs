using YChatApi.DTOs;

namespace YChatApi.Services
{
    public interface IAuthenticationService
    {
        Task<string> Login(UserDto dto);
        Task Register(UserDto dto);
    }
}
