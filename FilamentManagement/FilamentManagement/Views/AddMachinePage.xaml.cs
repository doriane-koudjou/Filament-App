using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilamentManagement.ViewModels;

namespace FilamentManagement.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddMachinePage : ContentPage
	{
		public AddMachinePage()
		{
			InitializeComponent();
			BindingContext = new AddMachineViewModel();
		}
	}


	/*
	async void OnCreateMachineButtonClicked(object sender, EventArgs e)
	{
		var machine = (Machine)BindingContext;
		//machine.Date = DateTime.UtcNow;
		if (!string.IsNullOrWhiteSpace(machine.Text))
		{
			await App.Database.SaveMachineAsync(machine);
		}
		await Shell.Current.GoToAsync("AddMachineViewModel");
	}
	*/

}
