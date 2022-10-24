using System;
using System.IO;
using FilamentManagement.Models;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using FilamentManagement.Views;

namespace FilamentManagement.ViewModels
{
    public class AddMachineViewModel : BaseViewModel
    {
        public ICommand AddMachineCommand { get; }

        private string _name;
        public string Name
        {
            get => _name; 
            set => SetProperty(ref _name, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private string _spoolCount;
        public string SpoolCount
        {
            get => _spoolCount;
            set => SetProperty(ref _spoolCount, value);
        }


        private string _brand;
        public string Brand
        {
            get => _brand;
            set => SetProperty(ref _brand, value);
        }


        private string _model;
        public string Model
        {
            get => _model;
            set => SetProperty(ref _model, value);
        }


        private string _owner;
        public string Owner
        {
            get => _owner;
            set => SetProperty(ref _owner, value);
        }

        private string _filamentDiameter;
        public string FilamentDiameter
        {
            get => _filamentDiameter;
            set => SetProperty(ref _filamentDiameter, value);
        }

        private string _maxNozzleTemperature;
        public string MaxNozzleTemperature
        {
            get => _maxNozzleTemperature;
            set => SetProperty(ref _maxNozzleTemperature, value);
        }

        private string _maxHeatbedTemperature;
        public string MaxHeatbedTemperature
        {
            get => _maxHeatbedTemperature;
            set => SetProperty(ref _maxHeatbedTemperature, value);
        }
        public AddMachineViewModel()
        {
            AddMachineCommand = new Command(AddMachine);
        }

        private async void AddMachine()
        {
            if (CheckForEmptyInputs())
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Please fill out all entries", "OK");
                return;
            }

            int parsedSpoolCount;
            double parsedMaxNozzleTemperature, parsedMaxHeatbedTemperature, parsedFilamentDiameter;
            if(!int.TryParse(SpoolCount,out parsedSpoolCount))
            {
                await Application.Current.MainPage.DisplayAlert("Alert", $"{SpoolCount} is not a valid input to represent the spool count", "OK");
                return;
            }
            if(!double.TryParse(MaxNozzleTemperature,out parsedMaxNozzleTemperature))
            {
                await Application.Current.MainPage.DisplayAlert("Alert", $"{MaxNozzleTemperature} is not a valid input to represent the max nozzle temperature", "OK");
                return;
            }
            if (!double.TryParse(MaxHeatbedTemperature, out parsedMaxHeatbedTemperature))
            {
                await Application.Current.MainPage.DisplayAlert("Alert", $"{MaxHeatbedTemperature} is not a valid input to represent the max heatbed temperature", "OK");
                return;
            }
            if (!double.TryParse(FilamentDiameter, out parsedFilamentDiameter))
            {
                await Application.Current.MainPage.DisplayAlert("Alert", $"{FilamentDiameter} is not a valid input to represent the filament diameter", "OK");
                return;
            }

            Machine machine = new Machine();
            machine.Name = Name;
            machine.Description = Description;
            machine.SpoolCount = parsedSpoolCount;
            machine.MaxNozzleTemperature = parsedMaxNozzleTemperature;
            machine.MaxHeatbedTemperature = parsedMaxHeatbedTemperature;
            machine.Brand = Brand;
            machine.Model = Model;
            machine.Owner = Owner;
            machine.FilamentDiameter = parsedFilamentDiameter;

            await database.SaveMachineAsync(machine);
            await Shell.Current.GoToAsync("..");
        }

        private bool CheckForEmptyInputs()
        {
            if(Name == null || SpoolCount == null || MaxNozzleTemperature == null 
                || MaxHeatbedTemperature == null || Brand == null || Model == null || Owner == null || FilamentDiameter == null)
            {
                return true;

            }else if (Name.Replace(" ","") == "" || SpoolCount.Replace(" ", "") == "" 
                || MaxNozzleTemperature.Replace(" ", "") == "" || MaxHeatbedTemperature.Replace(" ", "") == "" 
                || Brand.Replace(" ", "") == "" || Model.Replace(" ", "") == "" 
                || Owner.Replace(" ", "") == "" || FilamentDiameter.Replace(" ", "") == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
