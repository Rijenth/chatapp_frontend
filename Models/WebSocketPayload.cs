namespace DCDesktop.Models
{
    public class WebSocketPayload
    {
        public Message? Message { get; init; }
        public User? User { get; init; }

        public static WebSocketPayload From(Message message) => new() { Message = message };
        public static WebSocketPayload From(User user) => new() { User = user };
    } 
}
