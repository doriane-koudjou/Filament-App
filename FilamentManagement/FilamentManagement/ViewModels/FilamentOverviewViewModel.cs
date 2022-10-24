using FilamentManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FilamentManagement.ViewModels
{
    public class FilamentOverviewViewModel : BaseViewModel
    {
        public ObservableCollection<Filament> FilamentCollection { get; set; }
        public ICommand FilamentTappedCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand AddFilamentNavigator { get; }

        private Filament _selectedFilament;
        public Filament SelectedFilament {
            get => _selectedFilament;
            set => SetProperty(ref _selectedFilament, value);
        }
        private string _searchInput;
        public string SearchInput
        {
            get => _searchInput;
            set => SetProperty(ref _searchInput, value);
        }
        
        public FilamentOverviewViewModel()
        {
            Title = "FilamentOverview";
            FilamentCollection = new ObservableCollection<Filament>();
            FilamentTappedCommand = new Command(OnFilamentTapped);
            SearchCommand = new Command(SearchInCollection);
            AddFilamentNavigator = new Command(NavigateToAddFilament);
        }

        public void OnAppearing()
        {
            SelectedFilament = null;
            FilamentCollection.Clear();
            database.GetFilamentsAsync().Result.ForEach(FilamentCollection.Add);
        }
        private async void OnFilamentTapped()
        {
            if (SelectedFilament != null)
            {
                await Shell.Current.GoToAsync($"FilamentDetail?id={SelectedFilament.Id}");
            }
        }
        private void SearchInCollection()
        {
            List<Filament> displayableFilaments;
            FilamentCollection.Clear();
            if (_searchInput != "" && _searchInput != null)
            {
                displayableFilaments = database.SearchFilamentsByNameAsync(_searchInput).Result;
            }
            else
            {
                displayableFilaments = database.GetFilamentsAsync().Result;
            }
            displayableFilaments.ForEach(FilamentCollection.Add);
        }

        private async void NavigateToAddFilament()
        {
            await Shell.Current.GoToAsync("AddFilament");
        }
    }
}
