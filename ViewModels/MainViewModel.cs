using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DCDesktop.Services;
using System.Threading.Tasks;

namespace DCDesktop.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly AuthAPIService _authApiService = new();

    [ObservableProperty]
    private string result = "Suis je authentifé ?";

    [RelayCommand]
    public async Task LoadDataAsync()
    {
        Result = "Chargement...";

        try
        {
            var response = await _authApiService.Me();

            if (response.IsSuccessStatusCode)
            {

                Result = "Vous êtes authentifié";
                return;
            }
            
            Result = "Vous n'êtes pas authentifié";
        }
        catch (Exception ex)
        {
            Result = $"Erreur : {ex.Message}";
        }
    }
}