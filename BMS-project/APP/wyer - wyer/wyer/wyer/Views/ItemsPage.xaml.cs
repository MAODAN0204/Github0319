using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using wyer.Models;
using wyer.ViewModels;
using wyer.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace wyer.Views
{
    public partial class ItemsPage : ContentPage
    {
        //ItemsViewModel _viewModel;
        public string FrameValue { get; set; }
        public string get_mac="";
        
        public ItemsPage()
        {
            InitializeComponent();
            Title = "Device List";

            LoadItemId();
            //BindingContext = _viewModel = new ItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LoadItemId();
            //_viewModel.OnAppearing();
        }

        public async void LoadItemId()
        {
            
            
            var client = new HttpClient();
            try
            {
                var items1 = new List<Item>();
                var Account = Preferences.Get("UserID", "");
                client.Timeout = TimeSpan.FromSeconds(5);
                //var uri = "http://118.163.50.93/dev_items_baseDB/?ID=" + Account;
                var uri = "http://118.163.50.93/dev_items_baseDB/?ID=" + Account;
                var response = await client.GetAsync(uri);
                // on error throw a exception  
                //var result = response.EnsureSuccessStatusCode();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    // handling the answer  
                    LoadImg.IsVisible = true;
                    var responString = await response.Content.ReadAsStringAsync();
                    if (responString.IndexOf("<") == -1 && responString.Length > 0)
                    {
                        var json = JsonConvert.DeserializeObject<DevDataDB[]>(responString);

                        LoadImg.IsVisible = false;
                        //塞到item中
                        
                      var s = json.Length;
                        for (int i = 0; i < s; i++)
                        {   //塞值到Model Class 的 Item中
                            items1.Add(new Item { Id = Guid.NewGuid().ToString(), Text = json[i].dev_name.ToUpper(), dev_mac = json[i].dev_mac.ToUpper(), Description = json[i].Remark, Remark = json[i].Remark });
                            FrameValue = json[i].dev_name;
                        }
                        ItemsList.ItemsSource = items1;
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            finally
            {
                client.Dispose();
            }
        }

        public async void OnFrameTapped(object sender, ItemTappedEventArgs e)
        {
            Item selectitem = e.Item as Item;
            get_mac = selectitem.dev_mac; //因為只有更換選項時才會改MAC, 所以存在變數中, 以免重覆點選時不會開啟page
            await Navigation.PushAsync(new Views.ItemDetailPage(get_mac), true);
            //因為只有更換選項時才會改MAC, 所以存在變數中, 以免重覆點選時不會開啟page
        }

        public async void DeleteItem_Clicked(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var parameter = mi.CommandParameter;
            var Account = Preferences.Get("UserID", "");
            bool answer =await DisplayAlert("Are you sure delete Device ", mi.CommandParameter + " ?", "Yes", "No");

            //run sql to delete
            if(answer ==true)
            {
                var client = new HttpClient();
                try
                {
                    client.Timeout = TimeSpan.FromSeconds(5);
                    string uri = "http://118.163.50.93/del_items/?Mac=" + parameter+"&ID="+Account;
                    var response = await client.GetAsync(uri);
                    // on error throw a exception  
                    //var result = response.EnsureSuccessStatusCode();
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        await DisplayAlert("Delete ", mi.CommandParameter + " completed. ", "OK");
                    }
                    LoadItemId();//重新整device
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                finally
                {
                    client.Dispose();
                }
            }
        }

        private async void OnAddItem(object sender,EventArgs e)
        {
            await Navigation.PushAsync(new Views.NewItemPage(),true);
        }

    }
}