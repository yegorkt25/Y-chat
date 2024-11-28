using YChatApi.DTOs;
using YChatApi.Entities;
using YChatApi.Entities.Repositories;
using YChatApi.Services.Helpers;

namespace YChatApi.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _repository;
        private readonly IUsersRepository _usersRepository;

        public ChatService(IChatRepository repository, IUsersRepository usersRepository)
        {
            _repository = repository;
            _usersRepository = usersRepository;
        }
        public async Task<Chat> AddChat(ChatDto dto)
        {
            var time = DateTime.UtcNow;
            var chat = new Chat { Name = dto.Name, CreatedAt = time };

            var chatUsers = new List<User>();

            foreach (var username in dto.Usernames)
            {
                var user = await _usersRepository.GetUserByUsernameAsync(username);
                if (user == null)
                {
                    throw new ValidationException($"User with username {username} doesn't exist");
                }

                chatUsers.Add(user);
            }

            var chatAdded = await _repository.AddChatAsync(chat);

            var allChats = await _repository.GetAllChats();

            var currChatId = allChats.FirstOrDefault(x => x.CreatedAt == time).Id;

            foreach (var user in chatUsers)
            {
                var newChatUser = new ChatUsers { UserId = user.Id, ChatId = currChatId };
                await _repository.AddUserToChatAsync(newChatUser);
            }

            return chatAdded;
        }

        public async Task<IEnumerable<Chat>> GetAllUserChats(long userId)
        {
            var chats = await _repository.GetUserChats(userId);

            return chats;
        }
    }
}
