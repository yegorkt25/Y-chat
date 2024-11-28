
namespace YChatApi.Entities.Repositories
{
    public class MessagesRepository : IMessagesRepository
    {
        private readonly AppDbContext _context;

        public MessagesRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Message> AddMessage(Message message)
        {
            var addedMessage = await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();

            return addedMessage.Entity;
        }

        public async Task<IEnumerable<Message>> GetAllMessages(int chatId)
        {
            var messages = _context.Messages.Where(x => x.ChatId == chatId).ToList();

            await _context.SaveChangesAsync();

            return messages;
        }
    }
}
