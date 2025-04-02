using System;
using System.Collections.Specialized;
using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using DCDesktop.Models;
using DCDesktop.ViewModels;

namespace DCDesktop.Views;

public partial class PublicView : UserControl
{
    public PublicView()
    {
        InitializeComponent();

        if (DataContext is PublicViewModel vm)
        {
            HookViewModel(vm);
        }

        this.DataContextChanged += (_, _) =>
        {
            if (DataContext is PublicViewModel newVm)
            {
                HookViewModel(newVm);
            }
        };
    }

    private void HookViewModel(PublicViewModel vm)
    {
        vm.Messages.CollectionChanged += (_, args) =>
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
            {
                Dispatcher.UIThread.Post(() =>
                {
                    MessagesScrollViewer?.ScrollToEnd();
                });
            }
        };
    }
    
    private void OnChannelClicked(object? sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.DataContext is Channel channel)
        {
            if (DataContext is PublicViewModel vm)
            {
                // Mettre à jour directement le SelectedChannel au lieu d'utiliser la commande
                vm.SelectedChannel = channel;
            }
        }
    }
}