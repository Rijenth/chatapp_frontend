using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DCDesktop.Services;
using System.Threading.Tasks;

namespace DCDesktop.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly ExempleAPIService _exempleAPIService = new();

    [ObservableProperty]
    private string result = "Clique sur le bouton pour charger la donnée.";

    [RelayCommand]
    public async Task LoadDataAsync()
    {
        Result = "Chargement...";

        try
        {
            var data = await _exempleAPIService.GetExamplePostAsync();
            Result = data;
        }
        catch (Exception ex)
        {
            Result = $"Erreur : {ex.Message}";
        }
    }
}