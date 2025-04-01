using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DCDesktop.Models;

namespace DCDesktop.Services;

public class AuthAPIService : ApiService
{
    private readonly HttpClient _httpClient;

    public AuthAPIService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<HttpResponseMessage> RegisterAsync(User user)
    {
        var url = $"{BaseUrl}/auth/register";

        return await _httpClient.PostAsJsonAsync(url, user);
    }

    public async Task<AuthenticationResponse?> LoginAsync(AuthenticationRequest request)
    {
        var url = $"{BaseUrl}/auth/login";
        
        Debug.WriteLine("request: " + request);

        var response = await _httpClient.PostAsJsonAsync(url, request);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<AuthenticationResponse>();
        }

        return null;
    }

    public async Task<string> HelloAsync()
    {
        var url = $"{BaseUrl}/auth/hello";

        var response = await _httpClient.GetAsync(url);
        return await response.Content.ReadAsStringAsync();
    }
}