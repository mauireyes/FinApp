using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using AwesomeApp.Model;
using Syncfusion.PMML;
using Xamarin.Forms;

namespace AwesomeApp.Viewmodel
{
    public class SuggestionsViewModel : BaseTransactionViewModel
    {
        public SuggestionsViewModel()
        {
        }
         public ICommand AskCommand { get; private set; }

        // string my_db_path = Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "FIN_db.sqlite");

        public SuggestionsViewModel(INavigation navigation)
        {
            _navigation = navigation;

            _transaction = new Transaction();
            _dbhelper = new DatabaseHelper();

            AskCommand = new Command(async () => await Ask());

        }


        readonly List<string> categories = new List<string>() { "Books & Education", "Clothing", "Food & Drink",
                                                             "Health & Beauty", "Personal Development", "Techonology", "Transport" };

        private List<string> _askcategories;

        public List<string> AskCategories
        {
            get => categories;
            set
            {
                _askcategories = value;
                OnPropertyChanged("AskCategories");
            }
        }

        protected string _askneedorwant;
        public string AskNeedOrWant
        {
            get => _askneedorwant;
            set
            {
                _askneedorwant = value;
                OnPropertyChanged("AskNeedOrWant");
            }
        }

        protected string _askdesire;
        public string AskDesire
        {
            get => _askdesire;
            set
            {
                _askdesire = value;
                OnPropertyChanged("AskDesire");
            }
        }

        protected string _selectedaskcategory;
        public string SelectedAskCategory
        {
            get => _selectedaskcategory;
            set
            {
                _selectedaskcategory = value;
                OnPropertyChanged("SelectedAskCategory");
            }
        }

        protected decimal _askprice;
        public decimal AskPrice
        {
            get => _askprice;
            set
            {
                _askprice = value;
                OnPropertyChanged("AskPrice");
            }
        }

        protected int _askmonthlyneeds;
        public int AskMonthlyNeeds
        {
            get => _askmonthlyneeds;
            set
            {
                _askmonthlyneeds = value;
                OnPropertyChanged("AskMonthlyNeeds");
            }
        }




        async Task Ask()
        {
            var inputData = new
            {
                price = _askprice,
                balance = Balance,
                category = _selectedaskcategory,
                needorwant = _askneedorwant,
                desire =_askdesire,
                monthlyincome = Income,
                monthlyneeds = _askmonthlyneeds
            };

            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream pmmlStream = assembly.GetManifestResourceStream("AwesomeApp.glm.pmml");
            PMMLEvaluator PMMLEvaluator = new PMMLEvaluatorFactory().GetPMMLEvaluatorInstance(pmmlStream, PMMLValidationType.Schema);

            //Get the predicted result            
            PredictedResult predictedResult = PMMLEvaluator.GetResult(inputData, null);
            string predvalue = predictedResult.PredictedValue.ToString();

            if (predvalue == "1") predvalue = "Yes";
            else predvalue = "No";
            await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("We recommend:", predvalue, "Got it!");
        }
    }
}
