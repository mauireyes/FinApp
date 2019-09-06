using System;
using System.Windows.Input;
using Xamarin.Forms;
using AwesomeApp.Model;
using System.Threading.Tasks;
using System.IO;
using AwesomeApp.View;
using Syncfusion.PMML;
using System.Reflection;

namespace AwesomeApp.Viewmodel
{
    public class AddTransactionViewModel : BaseTransactionViewModel
    {

        public ICommand AddTransactionCommand { get; private set; }
        public ICommand ViewAllTransactionsCommand { get; private set; }

       // string my_db_path = Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "FIN_db.sqlite");
        
        public AddTransactionViewModel(INavigation navigation)
        {
            _navigation = navigation;

            _transaction = new Transaction();
            _dbhelper = new DatabaseHelper();
         
            AddTransactionCommand = new Command(async () => await AddTransaction());

        }

        async Task AddTransaction()
        {

            if (_transaction != null)
            {
                if (_transaction.Category != null && _transaction.Category != "All" && _transaction.Amount != 0 && _transaction.Type != null && _transaction.Type != "All")
                {



                    _transaction.CreatedAt = DateTime.Now;
                    if (_transaction.Type == "Expense")
                    {
                        _transaction.Amount = (_transaction.Amount * -1);
                        DatabaseHelper.Insert(ref _transaction);
                    }
                    else
                    {

                        DatabaseHelper.Insert(ref _transaction);
                    }



                    OnPropertyChanged("TransactionList");
                    OnPropertyChanged("Balance");
                    await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Add Transaction", "Success", "Okay");
                    await _navigation.PushAsync(new AddTransaction());
                }
                else
                {
                    await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Invalid Input", "Please try again.", "Okay");
                }
            }
          

        }


    }
}
