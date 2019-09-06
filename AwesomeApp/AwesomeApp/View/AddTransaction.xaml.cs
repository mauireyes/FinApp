using System;
using System.Collections.Generic;
using AwesomeApp.Viewmodel;
using Xamarin.Forms;

namespace AwesomeApp.View
{
    public partial class AddTransaction : ContentPage
    {
        public AddTransaction()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            //  NavigationPage.SetHasNavigationBar(this, false);
            this.Title = "FIN - Add";
            BindingContext = new AddTransactionViewModel(Navigation);

           // NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnAppearing()
        {
            InitializeComponent();

            //  NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);
            this.Title = "FIN - Add";
            this.BindingContext = new AddTransactionViewModel(Navigation);
        }
    }
}
