using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DCDesktop.Models;
using DCDesktop.Request;
using DCDesktop.Response;

namespace DCDesktop.Services;

public class AuthAPIService : ApiService
{
    public async Task<HttpResponseMessage> RegisterAsync(User user)
    {
        var url = $"http://{BaseUrl}/auth/register";

        return await HttpClient.PostAsJsonAsync(url, user);
    }

    public async Task<AuthenticationResponse?> LoginAsync(AuthenticationRequest request)
    {
        var url = $"http://{BaseUrl}/auth/login";
        
        var response = await HttpClient.PostAsJsonAsync(url, request);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<AuthenticationResponse>();
        }

        return null;
    }

    public async Task<HttpResponseMessage> Logout()
    {
        var url = $"http://{BaseUrl}/auth/logout";
    
        var authenticationRequest = new AuthenticationRequest
        {
            username = AuthenticationStateService.GetUsername(),
            password = ""
        };

        var jsonContent = JsonSerializer.Serialize(authenticationRequest);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = content
        };
        
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", JwtToken);

        return await HttpClient.SendAsync(request);
    }

    public async Task<HttpResponseMessage> Me()
    {
        var url = $"http://{BaseUrl}/me";
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", JwtToken);

        return await HttpClient.SendAsync(request);
    }
}