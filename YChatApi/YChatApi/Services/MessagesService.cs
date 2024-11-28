using YChatApi.DTOs;
using YChatApi.Entities;
using YChatApi.Entities.Repositories;
using YChatApi.Services.Helpers;

namespace YChatApi.Services
{
    public class MessagesService : IMessagesService
    {
        private readonly IMessagesRepository _repository;
        private readonly IChatRepository _chatRepository;

        public MessagesService(IMessagesRepository repository, IChatRepository chatRepository)
        {
            _repository = repository;
            _chatRepository = chatRepository;
        }

        public async Task<IEnumerable<Message>> GetMessages(int chatId)
        {
            var messages = await _repository.GetAllMessages(chatId);

            return messages;
        }

        public async Task<Message> SendMessage(int chatId, MessageDTO dto, User user)
        {
            var chats = await _chatRepository.GetAllChats();

            var chat = chats.FirstOrDefault(x => x.Id == chatId);

            if (!chat.Users.Any(x => x.Id == user.Id))
            {
                throw new ValidationException("User doesn't belong to the chat");
            }

            if (!chats.Any(x => x.Id == chatId))
            {
                throw new ValidationException("chat doesn't exist");
            }

            if (string.IsNullOrWhiteSpace(dto.Content))
            {
                throw new ValidationException("Message cannot be empty");
            }

            var message = new Message() { CreatedAt = DateTime.UtcNow, ChatId = chatId, UserId = user.Id, Content = dto.Content };

            await _repository.AddMessage(message);

            return message;
        }
    }
}
