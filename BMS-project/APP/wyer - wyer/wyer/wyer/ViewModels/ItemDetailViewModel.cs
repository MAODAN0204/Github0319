using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using wyer.Models;
using Xamarin.Forms;

namespace wyer.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    [QueryProperty(nameof(ItemText), nameof(ItemText))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string itemText;
        private string text;
        private string description;
        private string remark;
        public string Id { get; set; }
        private List<DateTime> dates;

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string Remark
        {
            get => description;
            set => SetProperty(ref remark, value);
        }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                //LoadItemId(value);
                
            }
        }
        public string ItemText
        {
            get
            {
                return itemId;
            }
            set
            {
                itemText = value;
                Title=value;

            }
        }

        public async void LoadItemId(string caldate)
        {
            
            //var item = await DataStore.GetItemAsync(itemId); 

            //取得MAC 去資料庫找資料 取得最新資料
            var client = new HttpClient();
            try
            {
                var mac = itemId;
                var uri = "http://118.163.50.93/get_realdata/?Mac=" + mac;
                client.Timeout = TimeSpan.FromSeconds(5);
                var response = await client.GetAsync(uri);

                // on error throw a exception  
                //var result = response.EnsureSuccessStatusCode();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    // handling the answer  
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var responString = await response.Content.ReadAsStringAsync();
                        var json = JsonConvert.DeserializeObject<List<DevArray>>(responString);
                        //塞到item中
                    }
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
            finally
            {
                client.Dispose();
            }
        }
    }
}
