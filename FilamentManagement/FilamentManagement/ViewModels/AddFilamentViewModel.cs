using FilamentManagement.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FilamentManagement.ViewModels
{
    public class AddFilamentViewModel : BaseViewModel
    {
        public ICommand AddFilamentCommand { get; }
        public ICommand ShowFilamentCommand { get; }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }


        private string _material;
        public string Material
        {
            get => _material;
            set => SetProperty(ref _material, value);
        }
        private string _color;
        public string Color
        {
            get => _color;
            set => SetProperty(ref _color, value);
        }

        private string _batch;
        public string Batch
        {
            get => _batch;
            set => SetProperty(ref _batch, value);
        }
        private string _producer;
        public string Producer
        {
            get => _producer;
            set => SetProperty(ref _producer, value);
        }


        private string _weight;
        public string Weight
        {
            get => _weight;
            set =>  SetProperty(ref _weight, value);
        }
        private string _length;
        public string Length
        {
            get => _length;
            set => SetProperty(ref _length, value);
        }
        public string _nozzleTemperature;
        public string NozzleTemperature
        {
            get => _nozzleTemperature;
            set => SetProperty(ref _nozzleTemperature, value);
        }


        private string _nozzleTemperatureTolerance;
        public string NozzleTemperatureTolerance
        {
            get => _nozzleTemperatureTolerance;
            set => SetProperty(ref _nozzleTemperatureTolerance, value);
        }
        private string _heatbedTemperature;
        public string HeatbedTemperature
        {
            get => _heatbedTemperature;
            set => SetProperty(ref _heatbedTemperature, value);
        }

        private string _heatbedTemperatureTolerance;
        public string HeatbedTemperatureTolerance
        {
            get => _heatbedTemperatureTolerance;
            set => SetProperty(ref _heatbedTemperatureTolerance, value);
        }
        private string _diameter;
        public string Diameter
        {
            get => _diameter;
            set =>  SetProperty(ref _diameter, value);
        }

        public AddFilamentViewModel()
        {
            AddFilamentCommand = new Command(AddFilament);
        }


        private async void AddFilament()
        {
            
            if (CheckForEmptyInputs())
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Please fill out all entries", "OK");
                return;
            }
            double parsedDiameter, parsedWeight, parsedLength, parsedHeatbedTemperatureTolerance,
                parsedHeatbedTemperature, parsedNozzleTemperature, parsedNozzleTemperatureTolerance;

            if(!double.TryParse(Diameter,out parsedDiameter)){
                await Application.Current.MainPage.DisplayAlert("Alert", $"{Diameter} is not a valid input to represent the diameter", "OK");
                return;
            }
            if (!double.TryParse(Weight, out parsedWeight))
            {
                await Application.Current.MainPage.DisplayAlert("Alert", $"{Weight} is not a valid input to represent the weight", "OK");
                return;
            }
            if (!double.TryParse(Length, out parsedLength))
            {
                await Application.Current.MainPage.DisplayAlert("Alert", $"{Length} is not a valid input to represent the length", "OK");
                return;
            }
            if (!double.TryParse(HeatbedTemperatureTolerance, out parsedHeatbedTemperatureTolerance))
            {
                await Application.Current.MainPage.DisplayAlert("Alert", $"{HeatbedTemperatureTolerance} is not a valid input to represent the heatbed temperature tolerance", "OK");
                return;
            }
            if (!double.TryParse(HeatbedTemperature, out parsedHeatbedTemperature))
            {
                await Application.Current.MainPage.DisplayAlert("Alert", $"{HeatbedTemperature} is not a valid input to represent the heatbed temperature", "OK");
                return;
            }
            if (!double.TryParse(NozzleTemperature, out parsedNozzleTemperature))
            {
                await Application.Current.MainPage.DisplayAlert("Alert", $"{NozzleTemperature} is not a valid input to represent the nozzle temperature", "OK");
                return;
            }
            if (!double.TryParse(NozzleTemperatureTolerance, out parsedNozzleTemperatureTolerance))
            {
                await Application.Current.MainPage.DisplayAlert("Alert", $"{NozzleTemperatureTolerance} is not a valid input to represent the length nozzle temperature tolerance", "OK");
                return;
            }
            if (!Regex.IsMatch(Color, "^#([a-fA-F0-9]{3}|[a-fA-F0-9]{6})$"))
            {
                await Application.Current.MainPage.DisplayAlert("Alert", $"{Color} is not a valid input to display a color", "OK");
                return;
            }


            Filament filament = new Filament();
            filament.Name = Name;
            filament.Producer = Producer;
            filament.Diameter = parsedDiameter;
            filament.Color = Color;
            filament.Weight = parsedWeight;
            filament.Length = parsedLength;
            filament.Batch = Batch;
            filament.Material = Material;
            filament.HeatbedTemperatureTolerance = parsedHeatbedTemperatureTolerance;
            filament.HeatbedTemperature = parsedHeatbedTemperature;
            filament.NozzleTemperature = parsedNozzleTemperature;
            filament.NozzleTemperatureTolerance = parsedNozzleTemperatureTolerance;

            await database.SaveFilamentAsync(filament);
            await Shell.Current.GoToAsync("..");
        }
        private bool CheckForEmptyInputs()
        {
            if (Name == null || Diameter == null || Color == null || Weight == null
                || Length == null || Batch == null || Material == null || HeatbedTemperatureTolerance == null 
                || HeatbedTemperature == null || NozzleTemperature == null || NozzleTemperatureTolerance == null)
            {
                return true;

            }
            else if (Name.Replace(" ", "") == "" || Diameter.Replace(" ", "") == "" || Color.Replace(" ", "") == ""
                || Weight.Replace(" ", "") == "" || Length.Replace(" ", "") == ""
                || Batch.Replace(" ", "") == "" || Material.Replace(" ", "") == ""
                || HeatbedTemperatureTolerance.Replace(" ", "") == "" || HeatbedTemperature.Replace(" ", "") == ""
                || NozzleTemperature.Replace(" ", "") == "" || NozzleTemperatureTolerance.Replace(" ", "") == "")
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

