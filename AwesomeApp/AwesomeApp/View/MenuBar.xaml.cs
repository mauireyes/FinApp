using System;
using System.Collections.Generic;
using AwesomeApp.Viewmodel;
using Xamarin.Forms;

namespace AwesomeApp.View
{
    public partial class MenuBar : ContentView
    {
        public MenuBar()
        {
            InitializeComponent();
            BindingContext = new MenuBarViewModel(Navigation);
        }
    }
}
