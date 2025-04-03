using System;
using System.Reactive.Linq;
using System.Text.Json;
using DCDesktop.Models;
using DCDesktop.Services;
using Websocket.Client;

public class WebSocketService: ApiService
{
    private WebsocketClient? _client;
    
    public Action<Message>? OnMessageReceived { get; set; }

    public void SubscribeToChannel(int channelId)
    {
        // Ferme l’ancienne connexion si nécessaire
        if (_client != null && _client.IsRunning)
        {
            _client.Dispose();
        }

        var url = new Uri($"ws://{BaseUrl}/ws/channel/{channelId}");
        _client = new WebsocketClient(url);

        _client.ReconnectTimeout = TimeSpan.FromSeconds(30);

        _client.MessageReceived
            .Where(msg => !string.IsNullOrEmpty(msg.Text))
            .Subscribe(msg =>
            {
                try
                {
                    var message = JsonSerializer.Deserialize<Message>(msg.Text);
                    
                    if (message != null)
                    {
                        Console.WriteLine("on invoke la methode");
                        OnMessageReceived?.Invoke(message);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Erreur de désérialisation : {ex.Message}");
                }
            });

        _client.Start().Wait();
        Console.WriteLine($"✅ Connecté au channel {channelId}");
    }

    public void Disconnect()
    {
        _client?.Dispose();
        Console.WriteLine("❌ Déconnecté du WebSocket");
        _client = null;
    }
}
