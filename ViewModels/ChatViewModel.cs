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

public partial class ChatViewModel : PublicViewModel
{
    [ObservableProperty]
    private string _newMessageText = string.Empty;

    private bool CanSendMessage => !string.IsNullOrWhiteSpace(NewMessageText) && SelectedChannel != null;

    public ChatViewModel()
    {
        _ = LoadChannelsAsync();
    }
    
    [RelayCommand]
    private async Task SendMessage()
    {
        ErrorMessage = String.Empty;
        
        if (!CanSendMessage)
        {
            ErrorMessage = "Le champs texte ne peut être vide";
            return;
        }
        
        var success = await _channelApiService.CreateChannelMessage(NewMessageText, SelectedChannel.Id);

        if (success)
        {
            await LoadSelectedChannelMessages();
        }
        
        NewMessageText = String.Empty;
    }
}