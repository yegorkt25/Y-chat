namespace YChatApi.Entities.Repositories
{
    public interface IChatRepository
    {
        Task<Chat> AddChatAsync(Chat chat);
        Task AddUserToChatAsync(ChatUsers user);
        Task RemoveUserFromChatAsync(ChatUsers user);
        Task RemoveChatAsync(long chatId);
        Task<IEnumerable<Chat>> GetUserChats(long userId);
        Task<IEnumerable<Chat>> GetAllChats();
    }
}
