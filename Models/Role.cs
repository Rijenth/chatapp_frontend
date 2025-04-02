using System.Collections.Generic;

namespace DCDesktop.Models;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    
    public List<ChannelRole> ChannelRoles { get; set; } = new();
}