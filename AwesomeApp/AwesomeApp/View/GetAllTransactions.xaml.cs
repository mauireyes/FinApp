using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using AwesomeApp;
using SQLite;
using AwesomeApp.Model;
using System.Collections.ObjectModel;
using AwesomeApp.Viewmodel;

namespace AwesomeApp
{
    public partial class GetAllTransactions : ContentPage
    {


        public GetAllTransactions()
        {
            InitializeComponent();

            //  NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);
            this.Title = "FIN - Transactions";
            this.BindingContext = new TransactionListViewModel(Navigation);

        }

            protected override void OnAppearing()
        {
            InitializeComponent();

            //  NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);
            this.Title = "FIN - Transactions";
            this.BindingContext = new TransactionListViewModel(Navigation);
        }
    }
    }

       
        

        //async void Insert_Clicked(object sender, System.EventArgs e)
        //{
        //    await Navigation.PushAsync(new NextPage(selected));
        //}
   
