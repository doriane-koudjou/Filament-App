using FilamentManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FilamentManagement.ViewModels
{
    [QueryProperty(nameof(FilamentId),"id")]
    public class DetailFilamentViewModel : BaseViewModel
    {
        public ICommand EditFilamentNavigator { get; }
        public ICommand DeleteFilamentCommand { get; }
        public string FilamentId
        {
            set => LoadFilament(value);
        }
        private Filament _displayedFilament;
        public Filament DisplayedFilament 
        {
            get => _displayedFilament;
            set => SetProperty(ref _displayedFilament, value);
        }
        private bool _machineIsVisible;
        public bool MachineIsVisible
        {
            get => _machineIsVisible;
            set => SetProperty(ref _machineIsVisible, value);
        }
        
       private string _machineName;
       public string MachineName
        {
            get => _machineName;
            set => SetProperty(ref _machineName, value);
        }
        private string _machineBrand;
        public string MachineBrand
        {
            get => _machineBrand;
            set => SetProperty(ref _machineBrand, value);
        }
        private string _machineModel;
        public string MachineModel
        {
            get => _machineModel;
            set => SetProperty(ref _machineModel, value);
        }

        private string _machineId;
        public string MachineId {
            get => _machineId;
            set => SetProperty(ref _machineId, value);
        }
        public DetailFilamentViewModel()
        {
            EditFilamentNavigator = new Command(NavigateToEditFilament);
            DeleteFilamentCommand = new Command(DeleteFilament);
            MachineName = "";
            MachineBrand = "";
            MachineModel = "";
            MachineIsVisible = false;
        }

        private void LoadFilament(string id)
        {
            DisplayedFilament = database.GetFilamentByIdAsync(Int32.Parse(id)).Result;
            Machine displayedMachine = database.GetMachineByFilamentUsageAsync(DisplayedFilament).Result;
            if(displayedMachine != null)
            {
                MachineName = displayedMachine.Name;
                MachineId = displayedMachine.Id.ToString();
                MachineBrand = displayedMachine.Brand;
                MachineModel = displayedMachine.Model;
                MachineIsVisible = true;
            }
        }
        private async void NavigateToEditFilament()
        {
            await Shell.Current.GoToAsync($"EditFilament?id={DisplayedFilament.Id}");
        }
        private async void DeleteFilament()
        {
            bool isUsed = database.GetMachineByFilamentUsageAsync(DisplayedFilament).Result == null? false : true;
            if (isUsed)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Filament is currently in use. Please remove it first.", "OK");
                return;
            }

            bool agreed = await Application.Current.MainPage.DisplayAlert("Question?", $"Are you sure you want to delete the filament?", "Yes", "No");
            if (!agreed) return;
            await database.DeleteFilamentAsync(DisplayedFilament);
            await Shell.Current.GoToAsync("..");
        }
    }
}
