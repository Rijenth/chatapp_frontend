using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DCDesktop.Models;
using DCDesktop.Response;

namespace DCDesktop.Services;

public class ChannelApiService : ApiService
{
    public async Task<List<Channel>?> GetAllChannelsAsync()
    {
        try
        {
            var url = $"{BaseUrl}/channels";
            var jwt = AuthenticationStateService.GetJWT();

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

            var response = await HttpClient.SendAsync(request);
            
            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();

                var result = await JsonSerializer.DeserializeAsync<ChannelListResponse>(contentStream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return result?.Data?.Channels ?? new List<Channel>();
            }
           
            Console.WriteLine($"Erreur GET /channels : {response.StatusCode}");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception lors de l'appel à /channels : {ex.Message}");
            return null;
        }
    }
}