using System;
using System.Text.Json.Serialization;

namespace DCDesktop.Models
{
    public class Message
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; } = "";

        [JsonPropertyName("conversation_id")]
        public int? ConversationId { get; set; }

        [JsonPropertyName("user_id")]
        public uint UserId { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; } = "";
        public DateTime CreatedAt { get; set; }

        public DateTime? ReceivedAt { get; set; }

        [JsonPropertyName("channel")]
        public Channel? Channel { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}