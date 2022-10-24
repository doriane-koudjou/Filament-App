using FilamentManagement.Models;
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
    public partial class DetailMachinePage : ContentPage
    {
        DetailMachineViewModel _viewModel;
        public DetailMachinePage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new DetailMachineViewModel();
        }
    }
}