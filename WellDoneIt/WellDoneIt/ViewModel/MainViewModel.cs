using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace WellDoneIt.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public MainViewModel(INavigationService navigationService)
        {
            Title = "WellDoneIt";

            if (navigationService == null) throw new ArgumentNullException("navigationService");
            _navigationService = navigationService;

            NavigationCommand = new RelayCommand<string>((parameter) => Navigate(parameter));

        }

        private void Navigate(string parameter)
        {
            _navigationService.NavigateTo(ViewModelLocator.ListPage, parameter ?? string.Empty);
        }

        public RelayCommand<string> NavigationCommand { get; private set; }
        

        public string Title { get; set; }
        
    }
}