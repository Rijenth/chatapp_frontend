namespace DCDesktop.Response;

public class AuthenticationResponse
{
    public uint userId { get; set; } = 0;
    public string username { get; set; } = "";
    public string jwt { get; set; } = "";
}