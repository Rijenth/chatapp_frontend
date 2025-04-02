using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

    [ObservableProperty]
    private ObservableCollection<Channel> _channels = new();

    [ObservableProperty]
    private Channel? _selectedChannel;
    
    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [ObservableProperty]
    private bool _isCreateChannelDialogVisible;

    [ObservableProperty]
    private string _newChannelName = string.Empty;

    public PublicViewModel()
    {
        _ = LoadChannelsAsync();
    }

    private async Task LoadChannelsAsync()
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

    [RelayCommand]
    private void GoBackMain()
    {
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
}