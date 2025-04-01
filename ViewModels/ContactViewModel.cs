using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DCDesktop.Models;

namespace DCDesktop.ViewModels
{
    public partial class ContactViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<User> _friends = new();

        [ObservableProperty]
        private string _newUsername = string.Empty;

        public ContactViewModel()
        {
            // Initialisation avec quelques utilisateurs
            Friends = new ObservableCollection<User>
            {
                new User { Username = "test" },
                new User { Username = "hello" }
            };
        }

        [RelayCommand]
        private void AddFriend()
        {
            if (string.IsNullOrWhiteSpace(NewUsername))
            {
                Debug.WriteLine("Le nom d'utilisateur ne peut pas être vide.");
                return;
            }

            if (Friends.Any(f => f.Username.Equals(NewUsername, System.StringComparison.OrdinalIgnoreCase)))
            {
                Debug.WriteLine($"L'utilisateur {NewUsername} existe déjà dans votre liste d'amis.");
                return;
            }

            Friends.Add(new User { Username = NewUsername });
            NewUsername = string.Empty;
        }

        public void RemoveFriend(User? user)
        {
            if (user != null)
            {
                Friends.Remove(user);
            }
        }
    }
}