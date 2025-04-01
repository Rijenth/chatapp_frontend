using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DCDesktop.Services;
using System.Threading.Tasks;

namespace DCDesktop.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly AuthAPIService _authApiService = new();

    [ObservableProperty] private string _username;

    public MainViewModel()
    {
        _username = AuthenticationStateService.GetUsername() != "" 
            ? AuthenticationStateService.GetUsername()
            : "INCONNU";
    }

    [RelayCommand]
    private void Public()
    {
        
    }
    
    [RelayCommand]
    private void Contact()
    {
        
    }
    
    [RelayCommand]
    private void Logout()
    {
        AuthenticationStateService.SetUsername("");
        AuthenticationStateService.SetJWT("");
        NavigationService.GoToLogin();
    }
}