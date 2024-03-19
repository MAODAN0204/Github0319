using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using wyer.Models;
using wyer.Views;
using Xamarin.Forms;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace wyer.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Item _selectedItem;

        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Item> ItemTapped { get; }

        public ItemsViewModel()
        {
            Title = "Device List";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Item>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            
            Items.Clear();
            /*****/
            var items1 = new List<Item>();
            var client = new HttpClient();
            try
            {
                client.Timeout = TimeSpan.FromSeconds(5);
                var Account = Preferences.Get("UserID", "");
                var uri = "http://118.163.50.93/dev_items/?ID=" + Account;
                var response = await client.GetAsync(uri);
                bool type1write_1time = false;
                // on error throw a exception  
                //var result = response.EnsureSuccessStatusCode();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {


                    // handling the answer  
                    var responString = await response.Content.ReadAsStringAsync();
                    if (responString.IndexOf("<") == -1 && responString.Length > 0)
                    {
                        var json = JsonConvert.DeserializeObject<DevDataGet[]>(responString);
                        //塞到item中
                        var s = json.Length;
                        for (int i = 0; i < s; i++)
                        {   //塞值到Model Class 的 Item中
                            if (json[i].i_type == "1" && type1write_1time == false)
                            {
                                items1.Add(new Item { Id = Guid.NewGuid().ToString(), Text = json[i].dev_name, Description = json[i].dev_mac, Remark = json[i].addr });
                                type1write_1time = true;
                            }
                            if (json[i].i_type != "1")
                            {
                                items1.Add(new Item { Id = Guid.NewGuid().ToString(), Text = json[i].dev_name, Description = json[i].dev_mac, Remark = json[i].addr });
                            }
                        }

                    }
                }
                var items2 = items1;// await DataStore.GetItemsAsync(true);
                foreach (var item in items2)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                client.Dispose();
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailPage.ItemId)}={item.Description}&{nameof(ItemDetailPage.ItemText)}={item.Text}&{nameof(ItemDetailPage.Remark)}={item.Remark}");
        }
    }
}