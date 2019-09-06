using System;
using System.Collections.Generic;
using System.IO;
using SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeApp.Model
{
    public class DatabaseHelper
    {
        //  string db_name = "FIN_db.sqlite";
        //  string folderPath = Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        //   public readonly string my_db_path = Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "FIN_db.sqlite");
        //    private SQLiteConnection database;

        //  string my_db_path;
        static string my_db_path = Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "FIN_db.sqlite");

       static SQLiteConnection database = new SQLiteConnection(my_db_path);

 
        public DatabaseHelper()
        {
           // database = new SQLiteAsyncConnection(my_db_path);
          //  database.CreateTableAsync<Transaction>().Wait();
        }

        #region reads
        public static List<Transaction> ReadByCategory(string category)
        {
            List<Transaction> _fullList = DatabaseHelper.Read();
            List<Transaction> list = new List<Transaction>();
            list = _fullList.FindAll(tr => tr.Category.Equals(category));
            return list;
        }

        public static List<Transaction> ReadByType(string type)
        {
            List<Transaction> _fullList = DatabaseHelper.Read();
            List<Transaction> list = new List<Transaction>();
            list = _fullList.FindAll(tr => tr.Type.Equals(type));
            return list;
        }

        public static List<Transaction> Read(string type, string category)
        {
            List<Transaction> _fullList = DatabaseHelper.Read();
            List<Transaction> list = new List<Transaction>();
            list = _fullList.FindAll(tr => tr.Type.Equals(type) && tr.Category.Equals(category));
            return list;
        }


        #endregion
        //Read Table
        public static List<Transaction> Read()
        {
            List<Transaction> transactions = new List<Transaction>();
            using (var conn = new SQLite.SQLiteConnection(my_db_path))
            {
                conn.CreateTable<Transaction>();
                transactions = conn.Table<Transaction>().ToList();
                transactions = transactions.OrderBy(o => o.CreatedAt).ToList();
               

            }
            return transactions;
        }

        public static List<string> ReadCategoriesNames()
        {
            List<String> categories = new List<String>();
            using (var conn = new SQLite.SQLiteConnection(my_db_path))
            {
            
                conn.CreateTable<Category>();
                categories = conn.Table<Category>().Select(cat => cat.Name).ToList();

        

            }
 
            return categories;
        }




        //Get Specific Transaction

        public static Transaction GetTransactionData(int id)
        {
            Transaction my_transaction = new Transaction();

            using (var conn = new SQLite.SQLiteConnection(my_db_path))
            {
                my_transaction = conn.Table<Transaction>().FirstOrDefault(t => t.Id == id);
            }
            return my_transaction;
          
        }


        //Insert Transaction
        public static bool Insert<T>(ref T data)
        {
            using (var conn = new SQLite.SQLiteConnection(my_db_path))
            {
                conn.CreateTable<T>();
                
                if (conn.Insert(data) != 0)
                    return true;
            }
            return false;
        }


        //Delete Data
        public static bool Delete<T>(ref T data)
        {
            using (var conn = new SQLite.SQLiteConnection(my_db_path))
            {
                if (conn.Delete(data) != 0)
                    return true;
            }
            return false;
        }

        //Delete Data
        public static bool Delete(Transaction data)
        {
            using (var conn = new SQLite.SQLiteConnection(my_db_path))
            {
                if (conn.Delete(data) != 0)
                    return true;
            }
            return false;
        }


        //Delete Data
        public static bool DeleteCategory (string categoryName)
        {
            Category cat = new Category();
            using (var conn = new SQLite.SQLiteConnection(my_db_path))
            {
                foreach (Category i in conn.Table<Category>().ToList())
                {
                    if( i.Name == categoryName)
                    {
                        cat = i;
                        break;
                    }
                }
                Delete(ref cat);
            }
            return false;
        }


        //Delete Table Transaction
        public static void DeleteTableTransaction()
        {
            using (var conn = new SQLite.SQLiteConnection(my_db_path))
            {

                conn.DeleteAll<Transaction>();
            }
        }

        public static void DeleteTableCategory()
        {
            using (var conn = new SQLite.SQLiteConnection(my_db_path))
            {

                conn.DeleteAll<Category>();
            }
        }



        public static void UpdateTransaction(Transaction transaction)
        {

            using (var conn = new SQLite.SQLiteConnection(my_db_path))
            {
                // Transaction foundTransaction = conn.Table<Transaction>().FirstOrDefault(t => t.Id == transaction.Id);
                if (transaction.Type == "Expense" && transaction.Amount < 0 || transaction.Type == "Income" && transaction.Amount > 0)
                {
                    conn.Update(transaction);
                }
                else if (transaction.Type == "Expense" && transaction.Amount > 0 || transaction.Type == "Income" && transaction.Amount < 0)
                {
                    transaction.Amount = transaction.Amount * -1;
                    conn.Update(transaction);
                }


                // conn.Update(transaction);
            }
        }





        public static decimal GetBalance()
        {
            decimal balance = 0;

            //SQLiteCommand cmd;

            using (var conn = new SQLite.SQLiteConnection(my_db_path))
            {

                //conn.Table<Transaction>().Sum<Transaction>(Fun)   

                try
                {
                    balance = conn.ExecuteScalar<decimal>("SELECT SUM(Amount) FROM 'Transaction'");
                    return balance;
                }
                catch
                {
                    return balance;
                }
            }


        }

        public static decimal GetTotal(string type)
        {
            decimal total = 0;
            //type = "Expense";

            //SQLiteCommand cmd;

            using (var conn = new SQLite.SQLiteConnection(my_db_path))
            {

                //conn.Table<Transaction>().Sum<Transaction>(Fun)   

                try
                {


                    total = conn.ExecuteScalar<decimal>("SELECT SUM(Amount) FROM 'Transaction' WHERE Type = ?", type);
                    return total;


                }
                catch
                {
                    return total;
                }
            }
        }

        public static decimal GetTotal(string type, string category)
        {
            decimal total = 0;
            //type = "Expense";

            //SQLiteCommand cmd;

            using (var conn = new SQLite.SQLiteConnection(my_db_path))
            {

                //conn.Table<Transaction>().Sum<Transaction>(Fun)   

                try
                {
                    total = conn.ExecuteScalar<decimal>("SELECT SUM(Amount) FROM 'Transaction' WHERE Type = ? AND Category = ?", type, category);
                    return total;
                }
                catch
                {
                    return total;
                }
            }
        }



    }
}
