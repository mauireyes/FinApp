using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwesomeApp.View;
using Xamarin.Forms;

namespace AwesomeApp
{
    public partial class MainPage : ContentPage
    {

        Label mytextnow = new Label();

        public MainPage()
        {
            InitializeComponent();
        }
        // int count = 0;
        private async void Handle_Clicked(object sender, System.EventArgs e)
        {
            //    count++;

            // ((Button)sender).Text = $"You clicked {count} times.";
            // await Navigation.PushAsync(new NextPage());

            if (entry_Username.Text == "o" && entry_Password.Text == "o")
            {
                // PageTitle.Text = "Logged In";
                //await Navigation.PushAsync(new GetAllTransactions());
                //await Navigation.PushAsync(new GetAllTransactions());
                await Navigation.PushAsync(new MenuTabbedPage());

            }
            else
            {
                PageTitle.Text = "Wrong details.";
            }


        }
    }
}
