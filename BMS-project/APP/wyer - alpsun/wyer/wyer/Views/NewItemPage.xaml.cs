using System;
using System.Text;
using wyer.Models;
using wyer.ViewModels;
using Xamarin.Forms;

namespace wyer.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }
        private string text;
        private string description;
        private string remark;

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }


        private async void OnCancel(object sender,EventArgs e)
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave(object sender ,EventArgs e)
        {
            //在 newitemviewmodel.cs存資料了


            //await DataStore.AddItemAsync(newItem);
            /*using (var client = new HttpClient())
            {
                var content = new StringContent("json", Encoding.UTF8, "application/json");

                var uri = "http://118.163.50.93/checkdev/?dev=" + devmac.Text;
                var result = await client.GetAsync(uri);

                // on error throw a exception  
                result.EnsureSuccessStatusCode();

                // handling the answer  
                var resultString = await result.Content.ReadAsStringAsync();


                if (resultString.IndexOf("<")==-1 && resultString.Length>0) 
                    await DisplayAlert("Error", "UserName or Password Error! \n \nPls Check!", "OK");
                else
                {
                    //資料正確存檔
                    await DisplayAlert("Error", "UserName or Password Error! \n \nPls Check!", "OK");
                    await Shell.Current.GoToAsync("..");
                }

            }*/

            // This will pop the current page off the navigation stack

        }


    }
}