using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DCDesktop.Services;

namespace DCDesktop.Views.Components;

public partial class UserBadge : UserControl
{
    public UserBadge()
    {
        InitializeComponent();
        
        var usernameTextBlock = this.FindControl<TextBlock>("UsernameText");
        if (usernameTextBlock is not null)
        {
            usernameTextBlock.Text = AuthenticationStateService.GetUsername();
        }
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}