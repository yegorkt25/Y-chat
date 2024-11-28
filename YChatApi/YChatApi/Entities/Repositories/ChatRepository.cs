using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace YChatApi.Entities.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly AppDbContext _context;

        public ChatRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Chat> AddChatAsync(Chat chat)
        {
            var addedChat = await _context.Chats.AddAsync(chat);
            await _context.SaveChangesAsync();

            return addedChat.Entity;
        }

        public async Task AddUserToChatAsync(ChatUsers user)
        {
            await _context.ChatUsers.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Chat>> GetAllChats()
        {
            return await _context.Chats.Include(x => x.Users).Include(x => x.Messages).ToListAsync();
        }

        public async Task<IEnumerable<Chat>> GetUserChats(long userId)
        {
            var user = await _context.Users.Include(x => x.Chats).ThenInclude(x => x.Messages).ThenInclude(x => x.User).Include(x => x.Chats).ThenInclude(x => x.Users).FirstOrDefaultAsync(x => x.Id == userId);
            return user.Chats.OrderBy(x => x.CreatedAt).Select(x => new Chat { Id = x.Id, CreatedAt = x.CreatedAt, Name = x.Name, Users = x.Users, Messages = x.Messages.OrderBy(e => e.CreatedAt).ToList() });
        }

        public async Task RemoveChatAsync(long chatId)
        {
            var chat = await _context.Chats.FirstOrDefaultAsync(x => x.Id == chatId);

            foreach (var user in chat.Users)
            {
                var chatUser = await _context.ChatUsers.FirstOrDefaultAsync(x => x.ChatId == chatId && x.UserId == user.Id);
                _context.ChatUsers.Remove(chatUser);
            }

            _context.Chats.Remove(chat);

            await _context.SaveChangesAsync();
        }

        public async Task RemoveUserFromChatAsync(ChatUsers user)
        {
            _context.ChatUsers.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
