
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using DCDesktop.Views;

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
    private void Login()
    {
        if (Username != "admin" || Password != "1234")
        {
            ErrorMessage = "Nom d'utilisateur ou mot de passe incorrect.";
            return;
        }

        
        if (App.Current.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime { MainWindow: MainWindow mainWindow }) return;
        
        mainWindow.Content = new MainPage();
    }
}
