using FilamentManagement.Models;
using FilamentManagement.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FilamentManagement.ViewModels
{
    [QueryProperty(nameof(MachineId),"id")]
    public class EquipFilamentViewModel : BaseViewModel
    {
        public ObservableCollection<Filament> EquipableFilaments { get; set; }
        public ICommand FilamentTappedCommand { get; }
        private Machine _displayedMachine;
        public Machine DisplayedMachine
        {
            get => _displayedMachine;
            set => SetProperty(ref _displayedMachine, value);
        }
        public string MachineId
        {
            set 
            {
                DisplayedMachine = database.GetMachineByIdAsync(Int32.Parse(value)).Result;
                LoadEquipableFilaments(value);
            }
        }
        private Filament _selectedFilament;
        public Filament SelectedFilament
        {
            get => _selectedFilament;
            set => SetProperty(ref _selectedFilament, value);
        }
        
        public EquipFilamentViewModel()
        {
            EquipableFilaments = new ObservableCollection<Filament>();
            FilamentTappedCommand = new Command(EquipFilament);
        }

        private void LoadEquipableFilaments(string id)
        {
            List<Filament> unusedFilaments = database.GetUnusedFilaments();
            Predicate<Filament> isCompatibleWithMachine = (filament) =>
            {
                bool isCompatible = false;
                if(filament.Diameter == DisplayedMachine.FilamentDiameter && 
                (filament.NozzleTemperature + filament.NozzleTemperatureTolerance) <= DisplayedMachine.MaxNozzleTemperature &&
                (filament.HeatbedTemperature + filament.HeatbedTemperatureTolerance <= DisplayedMachine.MaxHeatbedTemperature))
                {
                    isCompatible = true;
                }
                return isCompatible;
            };
            unusedFilaments.FindAll(isCompatibleWithMachine).ForEach(EquipableFilaments.Add);
        }
        private async void EquipFilament()
        {
            Record newRecord = new Record()
            {
                FK_Machine = DisplayedMachine.Id,
                FK_Filament = SelectedFilament.Id,
                LoadingDate = DateTime.Now
            };
            await database.SaveRecordAsync(newRecord);

            // MQTT Notification
            RecordNotification notification = new RecordNotification(newRecord, DisplayedMachine, SelectedFilament);
            String payload = JsonConvert.SerializeObject(notification);
            await MqttService.Publish("broker.hivemq.com", "thm/sfm/filament-management",payload);
            await Shell.Current.GoToAsync("..");
        }
    }
}
