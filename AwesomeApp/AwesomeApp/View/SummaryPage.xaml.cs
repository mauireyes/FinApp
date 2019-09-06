using System;
using System.Collections.Generic;
using AwesomeApp.Viewmodel;
using Xamarin.Forms;

namespace AwesomeApp.View
{
    public partial class SummaryPage : ContentPage
    {
        public SummaryPage()
        {
            InitializeComponent();
            //  NavigationPage.SetHasBackButton(this, false);
            this.Title = "FIN - Summary";
            this.BindingContext = new SummaryPageViewModel(Navigation);

        }

        protected override void OnAppearing()
        {
            InitializeComponent();

            //  NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);
            this.Title = "FIN - Summary";
            this.BindingContext = new SummaryPageViewModel(Navigation);
        }
    }
}
