using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
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
        
        var response = await _httpClient.PostAsJsonAsync(url, request);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<AuthenticationResponse>();
        }

        return null;
    }

    public async Task<HttpResponseMessage> Me()
    {
        var url = $"{BaseUrl}/me";
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        var jwt = AuthenticationStateService.GetJWT();
        
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

        return await _httpClient.SendAsync(request);
    }
}