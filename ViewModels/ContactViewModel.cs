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
        
        private readonly WebSocketService _webSocketService = new();
        
        [ObservableProperty]
        private ObservableCollection<User> _friends = new();

        [ObservableProperty]
        private string _newContactUsername = string.Empty;
        
        [ObservableProperty]
        private string _errorMessage = string.Empty;

        public ContactViewModel()
        {
            _ = LoadUserFriendList();
            _webSocketService.SubscribeToContact();
            _webSocketService.OnMessageReceived = async payload =>
            {
                if (payload.User is not null && !Friends.Contains(payload.User))
                {
                    await LoadUserFriendList();
                }
            };
        }

        private async Task LoadUserFriendList()
        {
            Friends.Clear();
            
            var result = await _contactApiService.GetAllUserContacts();

            if (result != null)
            {
                foreach (var contact in result)
                {
                    if (! Friends.Contains(contact))
                    {
                        Friends.Add(contact);
                    }
                }
                
                return;
            }

            ErrorMessage = "Impossible de charger les contacts depuis l'API.";
        }
        
        [RelayCommand]
        private async Task AddFriend()
        {
            if (string.IsNullOrWhiteSpace(NewContactUsername))
            {
                ErrorMessage = "Le nom d'utilisateur ne peut pas être vide.";
                return;
            }

            if (Friends.Any(f => f.Username.Equals(NewContactUsername, System.StringComparison.OrdinalIgnoreCase)))
            {
                ErrorMessage = $"L'utilisateur {NewContactUsername} existe déjà dans votre liste d'amis.";
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
                    ErrorMessage = $"Une erreur est survenue lors de la suppression";
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