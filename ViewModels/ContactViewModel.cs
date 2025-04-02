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
        private string _NewContactUsername = string.Empty;

        public ContactViewModel()
        {
            _ = LoadUserFriendList();
        }

        private async Task LoadUserFriendList()
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
        private async Task AddFriend()
        {
            if (string.IsNullOrWhiteSpace(NewContactUsername))
            {
                Console.WriteLine("Le nom d'utilisateur ne peut pas être vide.");
                return;
            }

            if (Friends.Any(f => f.Username.Equals(NewContactUsername, System.StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine($"L'utilisateur {NewContactUsername} existe déjà dans votre liste d'amis.");
                return;
            }

            var response = await _contactApiService.AddContactAsync(NewContactUsername);

            if (! response)
            {
                return;
            }
                
            await LoadUserFriendList();
            
            NewContactUsername = string.Empty;
        }

        public async Task RemoveFriend(User? user)
        {
            if (user != null)
            {
                var response = await _contactApiService.DeleteContactAsync(user.Id);

                if (! response)
                {
                    return;
                }
                
                await LoadUserFriendList();
            }
        }

        [RelayCommand]
        private void GoBackMain()
        {
            NavigationService.GoToMain();
        }
    }
}