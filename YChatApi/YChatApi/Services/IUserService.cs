using System.Security.Claims;
using YChatApi.Entities;

namespace YChatApi.Services
{
    public interface IUserService
    {
        Task<User> GetCurrentUser(string token);
    }
}
