using FilamentManagement.Models;
using FilamentManagement.Services;
using MQTTnet;
using MQTTnet.Client.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace FilamentManagement.ViewModels
{
    [QueryProperty(nameof(MachineId), "id")]
    public class DetailMachineViewModel : BaseViewModel
    {
        public ObservableCollection<Filament> EquipedFilaments { get; set; }
        public ObservableCollection<Record> RecordCollection { get; set; }
        public ICommand EditMachineNavigator { get; }
        public ICommand DeleteMachineCommand { get; }
        public ICommand EquipFilamentNavigator { get; }
        public ICommand UnequipFilamentCommand { get; }
        public string MachineId
        {
            set => LoadMachine(value);
        }

        private Machine _displayedMachine;
        public Machine DisplayedMachine
        {
            get => _displayedMachine;
            set => SetProperty(ref _displayedMachine, value);
        }
        
        public DetailMachineViewModel()
        {
            EquipedFilaments = new ObservableCollection<Filament>();
            RecordCollection = new ObservableCollection<Record>();

            EditMachineNavigator = new Command(NavigateToEditMachine);
            DeleteMachineCommand = new Command(DeleteMachine);
            EquipFilamentNavigator = new Command(NavigateToEquipFilament);
            UnequipFilamentCommand = new Command<Filament>(UnequipFilament);
        }

        private void LoadMachine(string id)
        {
            EquipedFilaments.Clear();
            RecordCollection.Clear();
            DisplayedMachine = database.GetMachineByIdAsync(Int32.Parse(id)).Result;
            List<Record> dependingRecords = database.GetRecordsByMachineAsync(DisplayedMachine).Result;
            List<Filament> equipedFilaments = database.GetEquipedFilaments(DisplayedMachine);
            dependingRecords.ForEach(RecordCollection.Add);
            equipedFilaments.ForEach(EquipedFilaments.Add);
        }
        private async void NavigateToEditMachine()
        {
            await Shell.Current.GoToAsync($"EditMachine?id={DisplayedMachine.Id}");
        }
        private async void DeleteMachine()
        {
            bool agreed = await Application.Current.MainPage.DisplayAlert("Question?", $"Are you sure you want to delete the machine and all related records?", "Yes", "No");
            if (!agreed) return;
            await database.DeleteMachineAsync(DisplayedMachine);
            await Shell.Current.GoToAsync("..");

        }
        private async void NavigateToEquipFilament()
        {
            if (DisplayedMachine.SpoolCount <= EquipedFilaments.Count)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "All spools are currently in use.", "OK");
            }
            else
            {
                await Shell.Current.GoToAsync($"EquipFilament?id={DisplayedMachine.Id}");
            } 
        }

        private async void UnequipFilament(Filament selectedFilament)
        {
            bool agreed = await Application.Current.MainPage.DisplayAlert("Question?", $"Are you sure you want to unequip the filament {selectedFilament.Name}?", "Yes", "No");
            if (!agreed) return;
            Record affectedRecord = database.GetRecordsAsync().Result
                .Find(record => record.FK_Machine == DisplayedMachine.Id 
                && record.FK_Filament == selectedFilament.Id 
                && record.UnloadingDate == null);
            affectedRecord.UnloadingDate = DateTime.Now;
            await database.UpdateRecordAsync(affectedRecord);
            EquipedFilaments.Remove(selectedFilament);
            LoadMachine(DisplayedMachine.Id.ToString());

            //MQTT Notification
            RecordNotification notification = new RecordNotification(affectedRecord, DisplayedMachine, selectedFilament);
            String payload = JsonConvert.SerializeObject(notification);
            await MqttService.Publish("broker.hivemq.com", "thm/sfm/filament-management", payload);
        }
    }
}
