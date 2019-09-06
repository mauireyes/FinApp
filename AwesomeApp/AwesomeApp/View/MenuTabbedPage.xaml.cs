using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace AwesomeApp.View
{
    public partial class MenuTabbedPage : Xamarin.Forms.TabbedPage
    {
        public MenuTabbedPage()
        {
            InitializeComponent();
          //  this.BarTextColor = Color.Accent;
           // this.Title = "FIN";
            //var tabbedPage = new TabbedPage();
               NavigationPage.SetHasNavigationBar(this, false);
            //   NavigationPage.Bar
            var GetAllTransactionsPage = new NavigationPage(new GetAllTransactions());
          //  GetAllTransactionsPage.Title = "Transactions";
            GetAllTransactionsPage.Icon = "scroll.png";

            var AddTransactionsPage = (new NavigationPage(new AddTransaction()));
          // AddTransactionsPage.Title = "Add";
            AddTransactionsPage.Icon = "add.png";

            var SummaryPage = (new NavigationPage(new SummaryPage()));
            // SummaryPage.Title = "Summary";
            SummaryPage.Icon = "document.png";

            var SettingsPage = (new NavigationPage(new SettingsPage()));
            //  SettingsPage.Title = "Settings";
            SettingsPage.Icon = "settings.png";

            var SuggestionPage = (new NavigationPage(new SuggestionsPage()));
            // SuggestionPage.Title = "Suggestion";
            SuggestionPage.Icon = "idea.png";


            Children.Add(GetAllTransactionsPage);
            Children.Add(AddTransactionsPage);
            Children.Add(SummaryPage);
            Children.Add(SettingsPage);
            Children.Add(SuggestionPage);


            On<Android>().SetToolbarPlacement(ToolbarPlacement.Top);
             On<Android>().SetBarSelectedItemColor(Color.Accent);
         



        }

       
    }
}
