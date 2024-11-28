namespace YChatApi.Entities.Repositories
{
    public interface IMessagesRepository
    {
        Task<Message> AddMessage(Message message);
        Task<IEnumerable<Message>> GetAllMessages(int chatId);
    }
}
