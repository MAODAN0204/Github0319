using Microcharts;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wyer.Models;
using wyer.Views;
using Xamarin.Forms;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using SkiaSharp;

namespace wyer.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class ChartViewModel : BaseViewModel
    {
        public Chart Chart { get; set; }

        public ObservableCollection<Item> Items { get; }

        public ChartViewModel()
        {
            Title = "BarChart";

        }
    }
}