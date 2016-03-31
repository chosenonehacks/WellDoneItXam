using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using MvvmHelpers;
using WellDoneIt.Model;
using WellDoneIt.Services;

namespace WellDoneIt.ViewModel
{
    public class ListViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IWellDoneItMobileService _wellDoneItMobileService;

        public ListViewModel(INavigationService navigationService, IWellDoneItMobileService wellDoneItMobileService)
        {
            NavigationBackCommand = new RelayCommand(() => NavigateBack());
            AddNewTaskCommand = new RelayCommand(async () => await NewTask());
            LoadTaskCommand = new RelayCommand(async () => await LoadTasks());

            if (navigationService == null) throw new ArgumentNullException("navigationService");
            _navigationService = navigationService;

            if (navigationService == null) throw new ArgumentNullException("wellDoneItMobileService");
            _wellDoneItMobileService = wellDoneItMobileService;

            LoadTaskCommand.Execute(null);


        }

        private async Task LoadTasks()
        {
             var tasks = await _wellDoneItMobileService.GetWellDoneItTasks();
            WellDoneItList.AddRange(tasks);
        }

        private async Task NewTask()
        {
            await _wellDoneItMobileService.AddWellDoneItTask();
        }

        public ObservableRangeCollection<WellDoneItTask> WellDoneItList { get; set; } = new ObservableRangeCollection<WellDoneItTask>();
        


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

        public RelayCommand AddNewTaskCommand { get; private set; }

        public RelayCommand LoadTaskCommand { get; private set; }
    }
}
