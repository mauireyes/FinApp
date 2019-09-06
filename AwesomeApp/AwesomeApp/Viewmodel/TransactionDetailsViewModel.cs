using System;
using System.IO;
using System.Windows.Input;
using Xamarin.Forms;
using AwesomeApp.Model;
using System.Threading.Tasks;

namespace AwesomeApp.Viewmodel
{
    public class TransactionDetailsViewModel : BaseTransactionViewModel
    {

        public ICommand UpdateTransactionCommand { get; private set; }
        public ICommand DeleteTransactionCommand { get; private set; }


        public TransactionDetailsViewModel(INavigation navigation, int selectedTransactionID)
        {
            _navigation = navigation;
            _transaction = new Transaction();
            _transaction.Id = selectedTransactionID;


            UpdateTransactionCommand = new Command(async () => await UpdateTransaction());
            DeleteTransactionCommand = new Command(async () => await DeleteTransaction());

            GetContactDetails();

        }

        void GetContactDetails()
        {
            
            _transaction = DatabaseHelper.GetTransactionData(_transaction.Id);
        }

        async Task UpdateTransaction()
        {
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Transaction Details", "Update", "Okay", "Cancel");
            if (isUserAccept)
            {



                if (_transaction.Category != null && _transaction.Category != "All" && _transaction.Amount != 0 && _transaction.Type != null && _transaction.Type != "All")
                {
                
                    DatabaseHelper.UpdateTransaction(_transaction);
                    await Application.Current.MainPage.DisplayAlert("Update Transaction", "Success", "Okay");
                    await _navigation.PushAsync(new GetAllTransactions());
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Invalid", "Please try again.", "Okay");
                }

            }
        }

        async Task DeleteTransaction()
        {
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Transaction Details", "Delete", "Okay", "Cancel");

            if (isUserAccept)
            {
                DatabaseHelper.Delete(ref _transaction);
                await Application.Current.MainPage.DisplayAlert("Delete Transaction", "Success", "Okay");
                await _navigation.PushAsync(new GetAllTransactions());
            }
        }


    }
}
