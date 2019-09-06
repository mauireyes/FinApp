using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AwesomeApp.Model;
using Xamarin.Forms;

namespace AwesomeApp.Viewmodel
{
    public class BaseTransactionViewModel : INotifyPropertyChanged
    {

 //public string my_db_path = Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "FIN_db.sqlite");

        public Transaction _transaction;

        public DatabaseHelper _dbhelper;

        public INavigation _navigation;

        public string Category
        {
            get => _transaction.Category;
            set
            {
                _transaction.Category = value;
                OnPropertyChanged("Category");
            }
        }


        public string Description
        {
            get => _transaction.Description;
            set
            {
                _transaction.Description = value;
                OnPropertyChanged("Description");
            }
        }

        public decimal Amount
        {
            get => _transaction.Amount;
            set
            {
                _transaction.Amount = value;
                OnPropertyChanged("Amount");
            }
        }


        public string Type
        {
            get => _transaction.Type;
            set
            {
                _transaction.Type = value;
                OnPropertyChanged("Type");
            }
        }


        public DateTime CreatedAt
        {
            get => _transaction.CreatedAt;
            set
            {
                _transaction.CreatedAt = value;
                OnPropertyChanged("CreatedAt");
            }
        }


        protected List<string> _categories;

        public List<string> Categories
        {
            get
            {

                List<string> CategoriesList = new List<string>() { "All" };
                foreach (var item in DatabaseHelper.ReadCategoriesNames())
                {
                    if(item != null)
                    {
                        CategoriesList.Add(item);
                    }
                }

                return CategoriesList;
            } 
        set
            {
                _categories = value;
                OnPropertyChanged("TransactionList");
                OnPropertyChanged("Categories");
            }
        }

        protected string _selectedCategory;
        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged("Income");
                OnPropertyChanged("Expense");
                OnPropertyChanged("Balance");
                OnPropertyChanged("TransactionList");
                OnPropertyChanged("SelectedCategory");
            }
        }

        readonly List<string> TypesList = new List<string>() { "All","Income", "Expense" };

        private List<string> _types;

        public List<string> Types
        {
            get => TypesList;
            set
            {
                _types = value;
                OnPropertyChanged("Types");
            }
        }

        protected string _selectedType;
        public string SelectedType
        {
            get => _selectedType;
            set
            {
                _selectedType = value;
                OnPropertyChanged("Income");
                OnPropertyChanged("Expense");
                OnPropertyChanged("Balance");
                OnPropertyChanged("TransactionList");
                OnPropertyChanged("SelectedType");
            }
        }





        protected decimal _balance;
        public decimal Balance
        {
            get => DatabaseHelper.GetBalance();
            set
            {
                _balance = value;
                OnPropertyChanged("Balance");

            }
        }

        protected decimal _expense;
        public decimal Expense
        {
            get

            {
                if (_selectedCategory == null || _selectedCategory == "All")
                {

                    return DatabaseHelper.GetTotal("Expense");


                }
                else
                {
                    return DatabaseHelper.GetTotal("Expense", _selectedCategory);

                }
            }
            set
            {
                _expense = value;
                OnPropertyChanged("Expense");

            }
        }

        public decimal _income;
        public decimal Income
        {
            get
            {
                if (_selectedCategory == null || _selectedCategory == "All")
                {
                    return DatabaseHelper.GetTotal("Income");

                }
                else
                {
                    return DatabaseHelper.GetTotal("Income", _selectedCategory);
                }
            }
            set
            {
                _income = value;
                OnPropertyChanged("Income");

            }
        }



        public BaseTransactionViewModel()
        {

            
        }

        public event PropertyChangedEventHandler PropertyChanged;

     

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected List<Transaction> GetCurrentTransactions()
        {

            //  TransactionList = DatabaseHelper.Read();
            //   List<Transaction> _fullList = DatabaseHelper.Read();
            List<Transaction> list = new List<Transaction>();
            if (_selectedCategory == null) _selectedCategory = "All";
            if (_selectedType == null) _selectedType = "All";

            //any category, specific type
            if ((_selectedCategory == "All") && (_selectedType != "All"))
            {
                list = DatabaseHelper.ReadByType(_selectedType);


            }
            //any type, specific category
            else if (_selectedType == "All" && _selectedCategory != "All")
            {
                list = DatabaseHelper.ReadByCategory(_selectedCategory);

            }
            // specific category, specific type - no blanks, no Alls
            else if (_selectedType != "All" && _selectedCategory != "All")
            {
                list = DatabaseHelper.Read(_selectedType, _selectedCategory);

            }

            else
            {

                list = DatabaseHelper.Read();
            }



            return list;
        }
    }
}
