using System.Net.Http;

namespace DCDesktop.Services;

public abstract class ApiService
{
    protected readonly string BaseUrl;
    protected readonly HttpClient HttpClient;
    protected ApiService(string baseUrl = "http://localhost:8000")
    {
        BaseUrl = baseUrl;
        HttpClient = new HttpClient();
    }
}