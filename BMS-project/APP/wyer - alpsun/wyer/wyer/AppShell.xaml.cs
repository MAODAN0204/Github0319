using System;
using System.Collections.Generic;
using wyer.ViewModels;
using wyer.Views;
using Xamarin.Forms;

namespace wyer
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            
            Routing.RegisterRoute(nameof(ItemsPage), typeof(ItemsPage));
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(SingleFieldPage), typeof(SingleFieldPage));
            Routing.RegisterRoute(nameof(ItemChartPage), typeof(ItemChartPage));
        }

    }
}
