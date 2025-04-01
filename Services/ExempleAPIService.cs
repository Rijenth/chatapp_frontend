using System.Net.Http;
using System.Threading.Tasks;

namespace DCDesktop.Services;

public class ExempleAPIService : ApiService
{
    private readonly HttpClient _httpClient;

    public ExempleAPIService() : base("https://jsonplaceholder.typicode.com")
    {
        _httpClient = new HttpClient();
    }

    public async Task<string> GetExamplePostAsync()
    {
        var url = $"{BaseUrl}/posts/1";

        var response = await _httpClient.GetAsync(url);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
}