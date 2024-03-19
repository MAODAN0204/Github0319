using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace alpsun.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class webpage : ContentPage
    {
        public webpage(String url)
        {
            InitializeComponent();
            weburl.Source = url;
        }
    }
}