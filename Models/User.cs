using System.Collections.Generic;

namespace DCDesktop.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public bool IsOnline { get; set; }
   
    public string OnlineStatusText => IsOnline ? "En ligne" : "Hors ligne";
    public string OnlineStatusColor => IsOnline ? "#4CAF50" : "#F44336";
    
    public List<Role> Roles { get; set; } = new();
    public List<Conversation> Conversations { get; set; } = new();
}