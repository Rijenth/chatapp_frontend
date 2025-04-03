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

public class ContactApiService : ApiService
{
    public async Task<List<User>?> GetAllUserContacts()
    {
        try
        {
            var url = $"{BaseUrl}/users/{AuthenticatedUserId}/contacts";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", JwtToken);

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
    
    public async Task<bool> AddContactAsync(string contactUsername)
    {
        var url = $"{BaseUrl}/users/{AuthenticatedUserId}/contacts";

        var requestBody = new CreateContactRequest()
        {
            Username = contactUsername
        };
        
        var json = JsonSerializer.Serialize(
            requestBody,
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }
        );

        var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", JwtToken);

        try
        {
            var response = await HttpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("✅ Contact ajouté avec succès.");
                return true;
            }
         
            Console.WriteLine($"❌ Échec de l'ajout du contact : {response.StatusCode}");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Erreur AddContactAsync : {ex.Message}");
            return false;
        }
    }
    
    public async Task<bool> DeleteContactAsync(int contactId)
    {
        var url = $"{BaseUrl}/users/{AuthenticatedUserId}/contacts/{contactId}";
        var request = new HttpRequestMessage(HttpMethod.Delete, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", JwtToken);

        try
        {
            var response = await HttpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"✅ Contact {contactId} supprimé pour l'utilisateur {AuthenticatedUserId}.");
                return true;
            }

            Console.WriteLine($"❌ Échec suppression contact : {response.StatusCode}");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Erreur DeleteContactAsync : {ex.Message}");
            return false;
        }
    }
}