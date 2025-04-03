using System.Net.Http;

namespace DCDesktop.Services;

public abstract class ApiService
{
    protected readonly string BaseUrl;
    protected readonly HttpClient HttpClient;
    
    protected readonly uint AuthenticatedUserId;
    protected readonly string AuthenticatedUserUsername;
    protected readonly string JwtToken;

    protected ApiService(string baseUrl = "http://localhost:8000")
    {
        AuthenticatedUserId = AuthenticationStateService.GetUserID();
        AuthenticatedUserUsername = AuthenticationStateService.GetUsername();
        JwtToken = AuthenticationStateService.GetJWT();
        BaseUrl = baseUrl;
        HttpClient = new HttpClient();
    }
}