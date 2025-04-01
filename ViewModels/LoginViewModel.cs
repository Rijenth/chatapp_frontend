
using System;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DCDesktop.Models;
using DCDesktop.Services;
using System.Threading.Tasks;


namespace DCDesktop.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    [ObservableProperty]
    private string _username = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [RelayCommand]
    private async Task Login()
    {
        var service = new AuthAPIService();
        var request = new AuthenticationRequest
        {
            password = Password,
            username = Username
        };

        if (string.IsNullOrWhiteSpace(request.username) || string.IsNullOrWhiteSpace(request.password))
        {
            ErrorMessage = "Veuillez remplir tous les champs.";
            return;
        }

        if (request.username.Length < 4)
        {
            ErrorMessage = "Le nom d'utilisateur doit contenir au moins 4 caractères.";
            return;
        }

        if (request.password.Length < 4)
        {
            ErrorMessage = "Le mot de passe doit contenir au moins 4 caractères.";
            return;
        }

        try
        {
            var response = await service.LoginAsync(request);

            if (response is null || string.IsNullOrEmpty(response.jwt))
            {
                ErrorMessage = "Nom d'utilisateur ou mot de passe incorrect.";
                return;
            }
            
            NavigationService.GoToMain();
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Erreur réseau : {ex.Message}";
        }
    }

    [RelayCommand]
    private void GoBackHome()
    {
        NavigationService.GoToHome();
    }
}
