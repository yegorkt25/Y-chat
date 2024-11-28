using YChatApi.DTOs;
using YChatApi.Entities;

namespace YChatApi.Services
{
    public interface IChatService
    {
        Task<IEnumerable<Chat>> GetAllUserChats(long userId);
        Task<Chat> AddChat(ChatDto dto);
    }
}
