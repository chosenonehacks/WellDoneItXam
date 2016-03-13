using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace WellDoneIt.Views
{
    public partial class ListPage : ContentPage
    {
        public ListPage(string parameter)
        {
            InitializeComponent();
            var viewModel = App.Locator.List;
            BindingContext = viewModel;

            viewModel.ParameterText = string.IsNullOrEmpty(parameter) ? "No parameter set" : parameter;
        }
    }
}
