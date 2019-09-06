using System;
using AwesomeApp.Viewmodel;
using Xamarin.Forms;

namespace AwesomeApp.View
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            this.Title = "FIN - Settings";
            NavigationPage.SetHasBackButton(this, false);
            //  NavigationPage.SetHasNavigationBar(this, false);

           this.BindingContext = new SettingsViewModel(Navigation);

        }
    }
}
