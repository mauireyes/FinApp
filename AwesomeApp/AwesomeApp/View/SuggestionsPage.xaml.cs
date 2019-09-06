using System;
using System.Collections.Generic;
using AwesomeApp.Viewmodel;
using Xamarin.Forms;

namespace AwesomeApp.View
{
    public partial class SuggestionsPage : ContentPage
    {
        public SuggestionsPage()
        {
            InitializeComponent();
            this.BindingContext = new SuggestionsViewModel(Navigation);
        }
    }
}
