using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace YChatApi.Entities
{
    public class User
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
        public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();
    }
}
