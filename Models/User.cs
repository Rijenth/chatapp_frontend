using System.Collections.Generic;

namespace DCDesktop.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public bool IsOnline { get; set; }

    public List<Role> Roles { get; set; } = new();
    public List<Conversation> Conversations { get; set; } = new();
}