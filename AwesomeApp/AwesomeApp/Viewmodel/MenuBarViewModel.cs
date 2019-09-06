using System;
using System.Threading.Tasks;
using System.Windows.Input;
using AwesomeApp.View;
using Xamarin.Forms;

namespace AwesomeApp.Viewmodel
{
    public class MenuBarViewModel 
    {

        public ICommand EnterNewTransactionCommand { get; private set; }
        public INavigation _navigation;

        public MenuBarViewModel(INavigation navigation)
        {
            _navigation = navigation;
            EnterNewTransactionCommand = new Command(async () => await NewTransaction());
        }

        private async Task NewTransaction()
        {
            await _navigation.PushAsync(new AddTransaction());

        }
    }
}
