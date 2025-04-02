using System;

namespace DCDesktop.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; } = "";
        public int? ConversationId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; } = "";
        public DateTime CreatedAt { get; set; }
        public DateTime? ReceivedAt { get; set; }
        public Channel? Channel { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}