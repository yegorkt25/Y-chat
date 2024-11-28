using YChatApi.DTOs;
using YChatApi.Entities;

namespace YChatApi.Services
{
    public interface IMessagesService
    {
        Task<Message> SendMessage(int chatId, MessageDTO dto, User user);
        Task<IEnumerable<Message>> GetMessages(int chatId);
    }
}
