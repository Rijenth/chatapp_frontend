using System;
using System.Collections.Generic;

namespace DCDesktop.Models
{
    public class Channel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public List<Message> Messages { get; set; } = new List<Message>();
        public List<ChannelRole> ChannelRoles { get; set; } = new List<ChannelRole>();
    }
}