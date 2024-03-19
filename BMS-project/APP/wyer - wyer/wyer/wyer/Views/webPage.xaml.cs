using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace wyer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class webPage : ContentPage
    {
        public webPage(String url)
        {
            
            InitializeComponent();
            weburl.Source = url;
        }
    }
}