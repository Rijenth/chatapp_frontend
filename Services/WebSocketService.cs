using System;
using System.Reactive.Linq;
using System.Text.Json;
using DCDesktop.Models;
using DCDesktop.Services;
using Websocket.Client;

public class WebSocketService: ApiService
{
    private WebsocketClient? _client;
    
    public Action<WebSocketPayload>? OnMessageReceived { get; set; }
    
    public void SetupSubscription(Uri url)
    {
        if (_client != null && _client.IsRunning)
        {
            _client.Dispose();
        }
        
        _client = new WebsocketClient(url);
        
        _client.ReconnectTimeout = TimeSpan.FromSeconds(30);
    }

    public void SubscribeToContact(int userId)
    {
        var url = new Uri($"ws://{BaseUrl}/ws/contacts/{userId}");
        
        SetupSubscription(url);
        
        if (_client == null) return;

        _client.MessageReceived
            .Where(msg => !string.IsNullOrEmpty(msg.Text))
            .Subscribe(msg =>
            {
                try
                {
                    var message = JsonSerializer.Deserialize<User>(msg.Text);

                    if (message != null)
                    {
                        OnMessageReceived?.Invoke(WebSocketPayload.From(message));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Erreur de désérialisation dans SubscribeToContact() : {ex.Message}");
                }
            });

        _client.Start().Wait();
    }

    public void SubscribeToChannel(int channelId)
    {
        var url = new Uri($"ws://{BaseUrl}/ws/channel/{channelId}");
        
        SetupSubscription(url);

        if (_client == null) return;
        
        _client.MessageReceived
            .Where(msg => !string.IsNullOrEmpty(msg.Text))
            .Subscribe(msg =>
            {
                try
                {
                    var message = JsonSerializer.Deserialize<Message>(msg.Text);

                    if (message != null)
                    {
                        OnMessageReceived?.Invoke(WebSocketPayload.From(message));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Erreur de désérialisation dans SubscribeToChannel(): {ex.Message}");
                }
            });

        _client.Start().Wait();
    }

    public void Disconnect()
    {
        _client?.Dispose();
        _client = null;
    }
}
