using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using WellDoneIt.Services;
using WellDoneIt.ViewModel;
using WellDoneIt.Views;
using Xamarin.Forms;

namespace WellDoneIt
{
    public class App : Application
    {
        private static ViewModelLocator _locator;
        public static ViewModelLocator Locator { get { return _locator ?? (_locator = new ViewModelLocator()); } }

        public App()
        {
            var nav = new NavigationService();

            nav.Configure(ViewModelLocator.MainPage, typeof(MainPage));
            nav.Configure(ViewModelLocator.ListPage, typeof(ListPage));

            SimpleIoc.Default.Register<INavigationService>(() => nav);

            var mainPage = new NavigationPage(new MainPage());

            nav.Initialize(mainPage);
            
            MainPage = mainPage;
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
