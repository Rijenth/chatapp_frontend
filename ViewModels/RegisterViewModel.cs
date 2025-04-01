using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DCDesktop.Services;
using System.Threading.Tasks;
using DCDesktop.Models;

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
    private async Task Register()
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

        var service = new AuthAPIService();

        try
        {
            var user = new User()
            {
                Username = Username,
                Password = Password
            };

            var result = await service.RegisterAsync(user);

            if (!result.IsSuccessStatusCode)
            {
                ErrorMessage = $"Impossible de créer l'utilisateur";
                return;
            }
            
            NavigationService.GoToLogin();
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Erreur serveur : {ex.Message}";
        }
    }
    [RelayCommand]
    private void BackToHome()
    {
        NavigationService.GoToHome();
    }
}