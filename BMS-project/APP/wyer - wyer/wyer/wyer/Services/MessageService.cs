using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace wyer.Services
{
    public class MessageService : AlertService
    {
        public async Task ShowAsync(string message)
        {
            await App.Current.MainPage.DisplayAlert("Message", message, "Ok");
        }
    }
}
