using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using AwesomeApp.Model;
using Xamarin.Forms;

namespace AwesomeApp.Viewmodel
{
    public class TransactionListViewModel : BaseTransactionViewModel
    {
        public ICommand AddCommand { get; private set; }
        public ICommand DeleteAllTransactionsCommand { get; private set; }
        public TransactionListViewModel(INavigation navigation)
        {
            _navigation = navigation;

            AddCommand = new Command(async () => await ShowAddTransaction());
            DeleteAllTransactionsCommand = new Command(async () => await DeleteAllTransactions());

        } 


        protected List<Transaction> _transactionList;

        public List<Transaction> TransactionList
        {
            get => GetCurrentTransactions();
            set
            {
                _transactionList = value;
                OnPropertyChanged("TransactionList");
                OnPropertyChanged("Total");
            }
        }



        private decimal _total;
        public decimal Total
        {
            get
            {
                decimal total = 0;

                if (_transactionList != null)
                {
                    // total = 0;
                    foreach (Transaction item in _transactionList)
                    {
                        total = total + item.Amount;
                    }
                }
                return total;
            }
            set
            {
                _total = value;
                OnPropertyChanged("Total");

            }
        }







        async Task ShowAddTransaction()
        {
            await _navigation.PushAsync(new TransactionDetailsPage(_selectedTransaction.Id));
        }

        async Task DeleteAllTransactions()
        {
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Transaction List", "Delete All Transactions?", "Okay", "Cancel");
            if( isUserAccept)
            {
                DatabaseHelper.DeleteTableTransaction();
                await _navigation.PushAsync(new TransactionDetailsPage(_selectedTransaction.Id));
            }
        }

        async void ShowTransactionDetails(int selectedTransactionID)
        {
            await _navigation.PushAsync(new TransactionDetailsPage(selectedTransactionID));
        }

        Transaction _selectedTransaction;

        public Transaction SelectedTransaction
        {
            get => _selectedTransaction;
            set { 
            
            if (value != null)
                {
                    _selectedTransaction = value;
                    OnPropertyChanged("SelectedTransaction");
                    ShowTransactionDetails(value.Id);
                }

            }

        }


    }
}
