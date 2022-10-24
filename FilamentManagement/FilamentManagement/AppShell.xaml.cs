using FilamentManagement.ViewModels;
using FilamentManagement.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FilamentManagement
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("FilamentDetail", typeof(DetailFilamentPage));
            Routing.RegisterRoute("MachineDetail", typeof(DetailMachinePage));
            Routing.RegisterRoute("AddFilament", typeof(AddFilamentPage));
            Routing.RegisterRoute("AddMachine", typeof(AddMachinePage));
            Routing.RegisterRoute("EditMachine", typeof(EditMachinePage));
            Routing.RegisterRoute("EditFilament", typeof(EditFilamentPage));
            Routing.RegisterRoute("EquipFilament", typeof(EquipFilamentPage));
        }  
    }
}
