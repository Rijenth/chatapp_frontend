
namespace DCDesktop.Services;


public static class AuthenticationStateService
{

    private static uint _userId;
    
    private static string? _jwt;

    private static string? _username;

    public static uint GetUserID()
    {
        return _userId;
    }
    public static void SetUserID(uint id)
    {
        _userId = id;
    }
    
    public static string GetJWT()
    {
        return _jwt ?? "";
    }
    public static void SetJWT(string jwt)
    {
        _jwt = jwt;
    }

    public static void SetUsername(string username)
    {
        _username = username;
    }

    public static string GetUsername()
    {
        return _username ?? "";
    }

    public static bool IsAuthenticated => !string.IsNullOrWhiteSpace(_jwt) && !string.IsNullOrWhiteSpace(_username);
}