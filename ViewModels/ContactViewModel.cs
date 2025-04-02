using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DCDesktop.Models;
using DCDesktop.Services;
using System.Threading.Tasks;

namespace DCDesktop.ViewModels
{
    public partial class ContactViewModel : ObservableObject
    {
        private readonly ContactApiService _contactApiService = new();
        
        [ObservableProperty]
        private ObservableCollection<User> _friends = new();

        [ObservableProperty]
        private string _newUsername = string.Empty;

        public ContactViewModel()
        {
            _ = LoadContactsAsync();
        }

        private async Task LoadContactsAsync()
        {
            var result = await _contactApiService.GetAllUserContacts();

            if (result != null)
            {
                Friends.Clear();
                foreach (var contact in result)
                {
                    Friends.Add(contact);
                }
                
                return;
            }
            
            Console.WriteLine("⚠️ Impossible de charger les contacts depuis l'API.");
        }
        
        [RelayCommand]
        private void AddFriend()
        {
            if (string.IsNullOrWhiteSpace(NewUsername))
            {
                Console.WriteLine("Le nom d'utilisateur ne peut pas être vide.");
                return;
            }

            if (Friends.Any(f => f.Username.Equals(NewUsername, System.StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine($"L'utilisateur {NewUsername} existe déjà dans votre liste d'amis.");
                return;
            }

            Friends.Insert(0, new User { Username = NewUsername });
            NewUsername = string.Empty;
        }

        public void RemoveFriend(User? user)
        {
            if (user != null)
            {
                Friends.Remove(user);
            }
        }

        [RelayCommand]
        public void GoBackMain()
        {
            NavigationService.GoToMain();
        }
    }
}