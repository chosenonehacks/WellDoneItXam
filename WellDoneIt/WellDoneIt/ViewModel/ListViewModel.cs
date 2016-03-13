using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace WellDoneIt.ViewModel
{
    public class ListViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public ListViewModel(INavigationService navigationService)
        {
            if (navigationService == null) throw new ArgumentNullException("navigationService");
            _navigationService = navigationService;
            
            NavigationBackCommand = new RelayCommand(() => NavigateBack());
        }

        private void NavigateBack()
        {
            _navigationService.GoBack();
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title == value) return;
                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        private string _parameterText;
        public string ParameterText
        {
            get { return _parameterText; }
            set
            {
                if (_parameterText == value) return;
                _parameterText = value;
                RaisePropertyChanged(() => ParameterText);
            }
        }

        public RelayCommand NavigationBackCommand { get; private set; }
    }
}
