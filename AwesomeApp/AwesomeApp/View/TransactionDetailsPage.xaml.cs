using System;
using System.IO;
using AwesomeApp.Model;
using Xamarin.Forms;
using AwesomeApp.Viewmodel;


namespace AwesomeApp
{
    public partial class TransactionDetailsPage : ContentPage
    {
        public TransactionDetailsPage(int transactionID)
        {

            InitializeComponent();
            this.Title = "FIN - Details";
            BindingContext = new TransactionDetailsViewModel(Navigation, transactionID);

        }




        //private void Insert_Clicked( object sender, EventArgs args)
        //{
        //    // throw new NotImplementedException();

        //    //  string db_name = "FIN_db.sqlite";
        //    // string folderPath = Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        //    // string db_path = Path.Combine(folderPath, db_name);
        //     string my_db_path = Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "FIN_db.sqlite");


        //    try
        //    {
        //        Transaction newTransaction = new Transaction()
        //        {
        //            Category = transactionCategory.SelectedItem.ToString(),
        //            Amount = decimal.Parse(transactionAmount.Text),
        //            Type = transactionType.SelectedItem.ToString()

        //        };
        //            if (DatabaseHelper.Insert(ref newTransaction, my_db_path))
        //            Console.WriteLine("SUCCESS");
        //        else
        //            Console.WriteLine("FAILURE");
        //    }
        //    catch
        //    {
        //        Console.WriteLine("WRONG ANSWER");
        //    }
        //}


        // private async void Delete_Clicked(object sender, EventArgs args)
        //{
        //    string my_db_path = Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "FIN_db.sqlite");

        //    DatabaseHelper.DeleteTableTransaction(my_db_path);

        //    await Navigation.PushAsync(new GetAllTransactions());

        //}


        //private async void Get_Clicked(object sender, EventArgs args)
        //{
        //    string my_db_path = Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "FIN_db.sqlite");

        //    // throw new NotImplementedException();
        //    await Navigation.PushAsync(new GetAllTransactions());
        //   //DatabaseHelper.GetBalance(my_db_path);
        //}


    }
}
