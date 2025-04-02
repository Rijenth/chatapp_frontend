using System;
using System.Text.Json.Serialization;

namespace DCDesktop.Models
{
    public class Message
    {
        public int Id { get; set; }

        public string Content { get; set; } = "";

        [JsonPropertyName("conversation_id")]
        public int? ConversationId { get; set; }

        [JsonPropertyName("user_id")]
        public uint UserId { get; set; }

        public string Username { get; set; } = "";

        public DateTime CreatedAt { get; set; }

        public DateTime? ReceivedAt { get; set; }

        public Channel? Channel { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}