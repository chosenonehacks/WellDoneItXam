using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WellDoneIt.ViewModel;
using WellDoneIt.Views;
using Xamarin.Forms;

namespace WellDoneIt
{
    public class App : Application
    {
        public App()
        {
            MainPage = new NavigationPage(new MainPage());
            
        }

        private static readonly ViewModelLocator _locator = new ViewModelLocator();
        public static ViewModelLocator Locator
        {
            get { return _locator; }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
