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

public class ContactApiService : ApiService
{
    public async Task<List<User>?> GetAllUserContacts()
    {
        try
        {
            var authenticatedUserID = AuthenticationStateService.GetUserID();
            var url = $"{BaseUrl}/users/{authenticatedUserID}/contacts";
            
            var jwt = AuthenticationStateService.GetJWT();

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

            var response = await HttpClient.SendAsync(request);
            
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                var contacts = await JsonSerializer.DeserializeAsync<List<User>>(stream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return contacts;
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