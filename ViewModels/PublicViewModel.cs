using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DCDesktop.Models;
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
        
        Console.WriteLine("⚠️ Impossible de charger les channels depuis l'API.");
    }

    [RelayCommand]
    private void GoBackMain()
    {
        NavigationService.GoToMain();
    }
}