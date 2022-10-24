using FilamentManagement.Models;
using FilamentManagement.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FilamentManagement.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<Filament> FilamentCollection { get; set; }
        public ObservableCollection<Machine> MachineCollection { get; set; }
        public ICommand FilamentTappedCommand { get; } 
        public ICommand MachineTappedCommand { get; } 
        public ICommand FilamentOverviewNavigator { get; }
        public ICommand MachineOverviewNavigator { get; }
        public ICommand AddFilamentNavigator { get; }
        public ICommand AddMachineNavigator { get; }

        private Filament _selectedFilament;
        public Filament SelectedFilament
        {
            get => _selectedFilament;
            set => SetProperty(ref _selectedFilament, value);
        }

        private Machine _selectedMachine;
        public Machine SelectedMachine
        {
            get => _selectedMachine;
            set => SetProperty(ref _selectedMachine, value);
        }

        public MainViewModel()
        {
            FilamentCollection = new ObservableCollection<Filament>();
            MachineCollection = new ObservableCollection<Machine>();
            
            FilamentTappedCommand = new Command(OnFilamentTapped);
            MachineTappedCommand = new Command(OnMachineTapped);
            FilamentOverviewNavigator = new Command(NavigateToFilamentOverview);
            MachineOverviewNavigator = new Command(NavigateToMachineOverview);
            AddFilamentNavigator = new Command(NavigateToAddFilament);
            AddMachineNavigator = new Command(NavigateToAddMachine);

            Title = "MainPage";
        }

        public void OnAppearing()
        {
            SelectedFilament = null;
            SelectedMachine = null;
            FilamentCollection.Clear();
            MachineCollection.Clear();
            database.GetFilamentsAsync().Result.ForEach(FilamentCollection.Add);
            database.GetMachinesAsync().Result.ForEach(MachineCollection.Add);
        }

        private async void OnFilamentTapped()
        {
            if(SelectedFilament != null)
            {
                await Shell.Current.GoToAsync($"FilamentDetail?id={SelectedFilament.Id}");
            }
        }

        private async void OnMachineTapped()
        {
            if(SelectedMachine != null)
            {
                await Shell.Current.GoToAsync($"MachineDetail?id={SelectedMachine.Id}");
            }

        }
        private async void NavigateToFilamentOverview()
        {
            await Shell.Current.GoToAsync("///FilamentOverview");
        }
        private async void NavigateToMachineOverview()
        {
            await Shell.Current.GoToAsync("///MachineOverview");
        }
        private async void NavigateToAddFilament()
        {
            await Shell.Current.GoToAsync("AddFilament");
        }
        private async void NavigateToAddMachine()
        {
            await Shell.Current.GoToAsync("AddMachine");
        }
    }
}
