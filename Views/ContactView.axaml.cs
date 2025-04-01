using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Interactivity;
using DCDesktop.Models;
using DCDesktop.ViewModels;

namespace DCDesktop.Views
{
    public partial class ContactView : UserControl
    {
        public ContactView()
        {
            InitializeComponent();
        }
        
        private void onRemoveFriendClicked(object? sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is User user)
            {

                if (DataContext is ContactViewModel vm)
                {
                    vm.RemoveFriend(user);
                }
            }
        }
    }
}