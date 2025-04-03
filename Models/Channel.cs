using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DCDesktop.Models
{
    public class Channel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        [JsonPropertyName("creator")]
        public ChannelCreator? Creator { get; set; }

        public List<Message> Messages { get; set; } = new List<Message>();
        public List<ChannelRole> ChannelRoles { get; set; } = new List<ChannelRole>();
    }

    public class ChannelCreator
    {
        [JsonPropertyName("user_id")]
        public int UserId { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; } = "";
    }
}