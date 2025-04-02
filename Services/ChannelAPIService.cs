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
using DCDesktop.Request;
using DCDesktop.Response;

namespace DCDesktop.Services;

public class ChannelApiService : ApiService
{
    public async Task<List<Channel>?> GetAllChannelsAsync()
    {
        try
        {
            var url = $"{BaseUrl}/channels";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", JWTToken);

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
    public async Task<Channel?> CreateChannelAsync(CreateChannelRequest channelRequest)
    {
        try
        {
            var url = $"{BaseUrl}/channels";
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", JWTToken);
    
            // Ajout du logging pour vérifier la requête
            Debug.WriteLine($"Tentative de création de channel avec le nom: {channelRequest.Name}");
        
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase // Important pour les APIs REST
            };
        
            var json = JsonSerializer.Serialize(channelRequest, options);
            Debug.WriteLine($"JSON envoyé: {json}");
        
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            request.Content = content;
    
            var response = await HttpClient.SendAsync(request);
    
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"Erreur POST /channels : {response.StatusCode} - {errorContent}");
                return null;
            }

            var contentStream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<Channel>(contentStream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception lors de l'appel à /channels : {ex.Message}");
            throw;
        }
    }
}