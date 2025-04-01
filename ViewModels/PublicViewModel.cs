using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DCDesktop.Models;
using DCDesktop.Services;

namespace DCDesktop.ViewModels;

public partial class PublicViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Channel> _channels = new();

    [ObservableProperty]
    private Channel? _selectedChannel;
    
    public PublicViewModel()
    {
        Channels = new ObservableCollection<Channel>
        {
            new Channel { id = 1, name = "Channel001" },
            new Channel { id = 2, name = "Channel002" },
            new Channel { id = 3, name = "Channel003" },
            new Channel { id = 4, name = "Channel004" },
            new Channel { id = 5, name = "Channel005" },
            new Channel { id = 3, name = "Channel006" },
            new Channel { id = 4, name = "Channel007" },
            new Channel { id = 5, name = "Channel008" },            
            new Channel { id = 3, name = "Channel009" },
            new Channel { id = 4, name = "Channel010" },
            new Channel { id = 5, name = "Channel011" },            
            new Channel { id = 3, name = "Channel012" },
            new Channel { id = 4, name = "Channel013" },
            new Channel { id = 5, name = "Channel014" },
            new Channel { id = 4, name = "Channel010" },
            new Channel { id = 5, name = "Channel011" },            
            new Channel { id = 3, name = "Channel012" },
            new Channel { id = 4, name = "Channel013" },
            new Channel { id = 5, name = "Channel014" }
        };
    }

    [RelayCommand]
    private void GoBackMain()
    {
        NavigationService.GoToMain();
    }
}