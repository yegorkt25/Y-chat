using Microsoft.EntityFrameworkCore;

namespace YChatApi.Entities
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ChatUsers> ChatUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();

            // Configuring composite key for ChatUsers table
            modelBuilder.Entity<ChatUsers>()
                .HasKey(cu => new { cu.ChatId, cu.UserId });

            // Configuring relationships
            modelBuilder.Entity<User>()
                .HasMany(m => m.Chats)
                .WithMany(m => m.Users)
                .UsingEntity<ChatUsers>();

            modelBuilder.Entity<Message>()
                .HasOne(m => m.User)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.UserId);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Chat)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ChatId);
        }
    }
}
