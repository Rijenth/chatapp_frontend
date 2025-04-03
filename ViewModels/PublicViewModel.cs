using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DCDesktop.Models;
using DCDesktop.Request;
using DCDesktop.Services;
using System.Threading.Tasks;

namespace DCDesktop.ViewModels;

public partial class PublicViewModel : ObservableObject
{
    public readonly ChannelApiService _channelApiService = new();
    
    public readonly WebSocketService _webSocketService = new();

    [ObservableProperty]
    public ObservableCollection<Channel> _channels = new();

    [ObservableProperty]
    public Channel? _selectedChannel;

    [ObservableProperty] 
    public ObservableCollection<Message> _messages = new();
           
    [ObservableProperty]
    public string _errorMessage = string.Empty;

    [ObservableProperty]
    public bool _isCreateChannelDialogVisible;

    [ObservableProperty]
    public string _newChannelName = string.Empty;

    public PublicViewModel()
    {
        _webSocketService.OnMessageReceived = async payload =>
        {
            if (payload.Message is not null)
            {
                await HandleNewMessageFromWebsocket(payload.Message);
            }
        };
    }
    
    partial void OnSelectedChannelChanged(Channel? value)
    {
        if (ErrorMessage != String.Empty)
        {
            ErrorMessage = String.Empty;
        }

        if (value != null)
        {
            _webSocketService.SubscribeToChannel(value.Id);
        }
        
        _ = LoadSelectedChannelMessages();
        
    }
    
    public async Task LoadChannelsAsync()
    {
        var result = await _channelApiService.GetAllChannelsAsync();

        if (result != null)
        {
            Channels.Clear();
            foreach (var channel in result)
            {
                Channels.Add(channel);
            }
            return;
        }
        
        ErrorMessage = "Impossible de charger les channels depuis l'API.";
    }

    public async Task HandleNewMessageFromWebsocket(Message message)
    {
        var authUserId = AuthenticationStateService.GetUserID();

        if (authUserId != message.UserId && SelectedChannel != null)
        {
            await LoadSelectedChannelMessages();
        }
    }

    public async Task LoadSelectedChannelMessages()
    {
        if (SelectedChannel != null)
        {
            var result = await _channelApiService.GetAllMessagesFromChannel(SelectedChannel);

            if (result != null)
            {
                Messages.Clear();
                foreach (var message in result)
                {
                    Messages.Add(message);
                }

                return;
            }
        
            ErrorMessage = "Impossible de charger les messages du channel.";  
        }
    }

    [RelayCommand]
    public void GoBackMain()
    {
        if (SelectedChannel != null)
        {
            _webSocketService.Disconnect();
        }
        
        NavigationService.GoToMain();
    }

    [RelayCommand]
    public void ShowCreateChannelDialog()
    {
        NewChannelName = string.Empty;
        ErrorMessage = string.Empty;
        IsCreateChannelDialogVisible = true;
    }

    [RelayCommand]
    public void CancelCreateChannel()
    {
        IsCreateChannelDialogVisible = false;
    }

    [RelayCommand]
    public async Task CreateChannel()
    {
        if (string.IsNullOrWhiteSpace(NewChannelName))
        {
            ErrorMessage = "Le nom du channel ne peut pas être vide";
            return;
        }

        try
        {
            var request = new CreateChannelRequest { Name = NewChannelName };
            var createdChannel = await _channelApiService.CreateChannelAsync(request);
            
            if (createdChannel != null)
            {
                await LoadChannelsAsync();
                IsCreateChannelDialogVisible = false;
            }
            else
            {
                ErrorMessage = "La création du channel a échoué";
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Erreur création channel: {ex.Message}");
            ErrorMessage = $"Erreur lors de la création: {ex.Message}";
        }
    }
}