using FilamentManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FilamentManagement.ViewModels
{
    public class MachineOverviewViewModel : BaseViewModel
    {
        public ObservableCollection<Machine> MachineCollection { get; set; }
        public ICommand MachineTappedCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand AddMachineNavigator { get; }

        private Machine _selectedMachine;
        public Machine SelectedMachine
        {
            get => _selectedMachine;
            set => SetProperty(ref _selectedMachine, value);
        }

        private string _searchInput;
        public string SearchInput
        {
            get => _searchInput;
            set => SetProperty(ref _searchInput, value);
        }


        public MachineOverviewViewModel()
        {
            Title = "MachineOverview";
            MachineCollection = new ObservableCollection<Machine>();
            MachineTappedCommand = new Command(OnMachineTapped);
            SearchCommand = new Command(SearchInCollection);
            AddMachineNavigator = new Command(NavigateToAddMachine);  
        }
        public void OnAppearing()
        {
            SelectedMachine = null;
            MachineCollection.Clear();
            database.GetMachinesAsync().Result.ForEach(MachineCollection.Add);
        }

        private async void OnMachineTapped()
        {
            if (SelectedMachine != null)
            {
                await Shell.Current.GoToAsync($"MachineDetail?id={SelectedMachine.Id}");
            }
        }
        private void SearchInCollection()
        {
            List<Machine> displayableMachines;
            MachineCollection.Clear();
            if (_searchInput != "" && _searchInput != null)
            {
                displayableMachines = database.SearchMachinesByNameAsync(_searchInput).Result;
            }
            else
            {
                displayableMachines = database.GetMachinesAsync().Result;
            }
            displayableMachines.ForEach(MachineCollection.Add);
        }

        private async void NavigateToAddMachine()
        {
            await Shell.Current.GoToAsync("AddMachine");
        }
    }
}
