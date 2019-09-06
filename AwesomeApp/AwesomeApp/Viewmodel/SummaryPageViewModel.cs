using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AwesomeApp.Viewmodel
{
    public class SummaryPageViewModel : BaseTransactionViewModel
    {
    


        public SummaryPageViewModel()
        {
        }

        public SummaryPageViewModel(INavigation navigation)
        {
            _navigation = navigation;
          
           
        }

      
    }
}
