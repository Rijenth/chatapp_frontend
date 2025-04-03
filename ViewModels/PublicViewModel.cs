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
    private readonly ChannelApiService _channelApiService = new();
    
    private readonly WebSocketService _webSocketService = new();

    [ObservableProperty]
    private ObservableCollection<Channel> _channels = new();

    [ObservableProperty]
    private Channel? _selectedChannel;

    [ObservableProperty] 
    private ObservableCollection<Message> _messages = new();
           
    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [ObservableProperty]
    private bool _isCreateChannelDialogVisible;

    [ObservableProperty]
    private string _newChannelName = string.Empty;

    protected PublicViewModel()
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
    
    protected async Task LoadChannelsAsync()
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

    private async Task HandleNewMessageFromWebsocket(Message message)
    {
        var authUserId = AuthenticationStateService.GetUserID();

        if (authUserId != message.UserId && SelectedChannel != null)
        {
            if (Messages.Contains(message))
            {
                return;
            }
            
            Messages.Add(message);
        }
    }

    protected async Task LoadSelectedChannelMessages()
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
    private void GoBackMain()
    {
        if (SelectedChannel != null)
        {
            _webSocketService.Disconnect();
        }
        
        NavigationService.GoToMain();
    }

    [RelayCommand]
    private void ShowCreateChannelDialog()
    {
        NewChannelName = string.Empty;
        ErrorMessage = string.Empty;
        IsCreateChannelDialogVisible = true;
    }

    [RelayCommand]
    private void CancelCreateChannel()
    {
        IsCreateChannelDialogVisible = false;
    }

    [RelayCommand]
    private async Task CreateChannel()
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

    public ChannelApiService GetChannelApiService()
    {
        return _channelApiService;
    }
}