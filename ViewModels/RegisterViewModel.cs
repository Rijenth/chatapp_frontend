using System;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DCDesktop.Services;
using DCDesktop.Views;

namespace DCDesktop.ViewModels;

public partial class RegisterViewModel : ObservableObject
{
    [ObservableProperty]
    private string username = string.Empty;

    [ObservableProperty]
    private string password = string.Empty;
    
    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [RelayCommand]
    private void Register()
    {
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = "Veuillez remplir tous les champs.";
            return;
        }
        
        if (Username.Length < 4)
        {
            ErrorMessage = "Le nom d'utilisateur doit contenir au moins 4 caractères.";
            return;
        }

        if (Password.Length < 4)
        {
            ErrorMessage = "Le mot de passe doit contenir au moins 4 caractères.";
            return;
        }

        Console.WriteLine($"Utilisateur enregistré : {Username} / {Password}");

        ErrorMessage = "";
    }

    [RelayCommand]
    private void BackToHome()
    {
        NavigationService.GoToHome();
    }
}