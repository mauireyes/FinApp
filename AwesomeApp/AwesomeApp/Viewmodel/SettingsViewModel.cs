using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using AwesomeApp.Model;
using AwesomeApp.View;
using Xamarin.Forms;

namespace AwesomeApp.Viewmodel
{
    public class SettingsViewModel : BaseTransactionViewModel
    {
        public ICommand DeleteAllTransactionsCommand { get; private set; }
        public ICommand AddNewCategoryCommand { get; private set; }
        public ICommand DeleteCategoryCommand { get; private set; }
        public ICommand DeleteTransactionsCommand { get; private set; }

        public SettingsViewModel()
        {
        }


        public SettingsViewModel(INavigation navigation)
        {
            _navigation = navigation;

            _newCategory = new Category();
            _dbhelper = new DatabaseHelper();

            DeleteAllTransactionsCommand = new Command(async () => await DeleteAllTransactions());
            AddNewCategoryCommand = new Command(async () => await AddNewCategory());
            DeleteCategoryCommand = new Command(async () => await DeleteCategory());
            DeleteTransactionsCommand = new Command(async () => await DeleteTransactions());


        }
        async Task AddNewCategory()
        {

              if (_newCategory.Name != null)
            {
                bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Add New Category", "Would you like to add this category?", "Yes", "Cancel");
                if (isUserAccept)
                {
                    DatabaseHelper.Insert(ref _newCategory);
                    await Application.Current.MainPage.DisplayAlert("Add New Category", "Success", "Okay");
                    //  OnPropertyChanged("Categories");

                }
              
                OnPropertyChanged("Categories");
                OnPropertyChanged("TransactionList");
                await _navigation.PushAsync(new SettingsPage());
            }

        }

        async Task DeleteCategory()
        {

            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Delete Category", "Are you sure you want to delete this category?", "Yes", "Cancel");
            if (isUserAccept)
            {
                DatabaseHelper.DeleteCategory(_categoryToDelete);
                await Application.Current.MainPage.DisplayAlert("Delete Category", "Success", "Okay");
                // _categories.Remove(_selectedCategory);

            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Delete Category", "Error", "Okay");
            }
           // OnPropertyChanged("Categories");
            OnPropertyChanged("TransactionList");
            OnPropertyChanged("Categories");
            await _navigation.PushAsync(new SettingsPage());
        }

        async Task DeleteTransactions()
        {

            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Delete Multiple Entries", "Are you sure you want to delete these entries?", "Yes", "Cancel");
            if (isUserAccept)
            {
                foreach(Transaction i in GetCurrentTransactions())
                {
                    DatabaseHelper.Delete(i);
                }

                await Application.Current.MainPage.DisplayAlert("Delete Multiple Entries", "Success", "Okay");

            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Delete Multiple Entries", "Error", "Okay");
            }
            OnPropertyChanged("TransactionList");
            await _navigation.PushAsync(new SettingsPage());
        }


        async Task DeleteAllTransactions()
        {
           
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Delete All Transactions", "Are you sure you want to delete all entries?", "Yes", "Cancel");
            if (isUserAccept)
            {
                DatabaseHelper.DeleteTableTransaction();
                await Application.Current.MainPage.DisplayAlert("Delete All Transactions", "Success", "Okay");
                await _navigation.PushAsync(new SettingsPage());


            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Delete All", "Error", "Okay");
            }
        }


        private Category _newCategory;

        public Category NewCategory
        {
            get => _newCategory;
            set
            {

                if (value != null)
                {
                    _newCategory = value;
                    OnPropertyChanged("NewCategory");

                }

            }

        }

        public string CategoryName
        {
            get => _newCategory.Name;
            set
            {
                _newCategory.Name = value;
                OnPropertyChanged("CategoryName");
            }
        }

        private string _categoryToDelete;

        public string CategoryToDelete
        {
            get => _categoryToDelete;
            set
            {

                if (value != null)
                {
                    _categoryToDelete = value;
                    OnPropertyChanged("CategoryToDelete");
                }

            }

        }






    }
}
