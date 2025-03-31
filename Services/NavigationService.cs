using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using DCDesktop.Views;

namespace DCDesktop.Services;

public static class NavigationService
{
    public static void GoToMain()
    {
        if (App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop &&
            desktop.MainWindow is Window mainWindow)
        {
            mainWindow.Content = new MainView();
        }
    }
    
    public static void GoToHome()
    {
        if (App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop &&
            desktop.MainWindow is Window mainWindow)
        {
            mainWindow.Content = new HomeView();
        }
    }

    public static void GoToLogin()
    {
        if (App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop &&
            desktop.MainWindow is Window mainWindow)
        {
            mainWindow.Content = new LoginView();
        }
    }

    public static void GoToRegister()
    {
        if (App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop &&
            desktop.MainWindow is Window mainWindow)
        {
            mainWindow.Content = new RegisterView();
        }
    }
}