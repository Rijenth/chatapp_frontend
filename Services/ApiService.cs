namespace DCDesktop.Services;

public abstract class ApiService
{
    protected readonly string BaseUrl;

    protected ApiService(string baseUrl)
    {
        BaseUrl = baseUrl;
    }
}