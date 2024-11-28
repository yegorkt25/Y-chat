using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace YChatApi.Entities
{
    public class ChatUsers
    {
        [Key]
        [Column(Order = 0)]
        public long ChatId { get; set; }

        [Key]
        [Column(Order = 1)]
        public long UserId { get; set; }
    }
}
