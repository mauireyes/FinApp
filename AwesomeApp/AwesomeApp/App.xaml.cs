using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AwesomeApp.View;
using AwesomeApp.Model;
using System.IO;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AwesomeApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //var tabbedPage = new TabbedPage();
            //tabbedPage.Children.Add(new NavigationPage( new GetAllTransactions()));
            //tabbedPage.Children.Add(new NavigationPage( new AddTransaction()));

            //MainPage = new TabbedPage();
            //MainPage = tabbedPage;
            MainPage = new NavigationPage(new MenuTabbedPage());
       //     MainPage = new NavigationPage(new MenuTabbedPage());
          //  MainPage = new NavigationPage(new GetAllTransactions());
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
