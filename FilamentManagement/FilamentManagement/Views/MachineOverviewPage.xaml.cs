using FilamentManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FilamentManagement.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MachineOverviewPage : ContentPage
    {
        MachineOverviewViewModel _viewModel;
        public MachineOverviewPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new MachineOverviewViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}