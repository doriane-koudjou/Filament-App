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
    public partial class FilamentOverviewPage : ContentPage
    {
        FilamentOverviewViewModel _viewModel;
        public FilamentOverviewPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new FilamentOverviewViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private void searchBar_TextChanged(object sender, TextChangedEventArgs e) {

        }
    }
}