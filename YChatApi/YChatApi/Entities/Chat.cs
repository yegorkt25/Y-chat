using System.ComponentModel.DataAnnotations;

namespace YChatApi.Entities
{
    public class Chat
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
