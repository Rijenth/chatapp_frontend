using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Interactivity;
using DCDesktop.Models;
using DCDesktop.ViewModels;

namespace DCDesktop.Views;

public partial class PublicView : UserControl
{
    public PublicView()
    {
        InitializeComponent();
    }
    
    private void OnChannelClicked(object? sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.DataContext is Channel channel)
        {
            Debug.WriteLine($"🟣 Channel sélectionné : {channel.Id} - {channel.Name}");

            if (DataContext is PublicViewModel vm)
            {
                // Mettre à jour directement le SelectedChannel au lieu d'utiliser la commande
                vm.SelectedChannel = channel;
            }
        }
    }
}