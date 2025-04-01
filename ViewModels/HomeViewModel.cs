using System;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using DCDesktop.Services;
using DCDesktop.Views;

namespace DCDesktop.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    [RelayCommand]
    private void Login()
    {
        NavigationService.GoToLogin();
    }

    [RelayCommand]
    private void Register()
    {
        NavigationService.GoToRegister();
    }
}