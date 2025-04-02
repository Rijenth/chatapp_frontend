namespace DCDesktop.Request;

public class CreateChannelMessageRequest
{
    public string Content { get; set; } = string.Empty;

    public int UserId { get; set; }

    public string Username { get; set; } = string.Empty;
}
