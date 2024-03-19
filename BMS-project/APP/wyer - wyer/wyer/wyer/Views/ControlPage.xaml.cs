using System;
using System.Collections.Generic;
using System.Text;
using wyer.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace wyer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ControlPage : ContentPage
    {
        public ControlPage(String URL)
        {
            InitializeComponent();

            Title = "Go Back";

            webView.Source = URL;

        }

       
    }
}