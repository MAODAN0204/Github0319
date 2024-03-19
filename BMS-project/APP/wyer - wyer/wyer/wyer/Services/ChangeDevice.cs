using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace wyer.Services
{
    public class ChangeDevice : ContentView
    {
        public ChangeDevice()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Welcome to Xamarin.Forms!" }
                }
            };
        }
    }
}