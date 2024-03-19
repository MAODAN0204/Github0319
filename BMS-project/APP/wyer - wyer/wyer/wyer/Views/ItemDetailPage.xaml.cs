using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using wyer.Models;
using wyer.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using static Xamarin.Essentials.Permissions;

namespace wyer.Views
{
    public partial class ItemDetailPage : ContentPage
    {

        public ItemDetailPage(String dev_mac)
        {

            InitializeComponent();
            type1.IsVisible = false;
            type2.IsVisible = false;
            Title = dev_mac;
            Get_Data(dev_mac);
            //一進入就抓今天的值
            //BindingContext = new ItemDetailViewModel();
        }

        private async void Get_Data(string mac)
        { 
           var i_type=await Get_type(mac);

           //取得MAC 去資料庫找資料 取得最新資料
           var client = new HttpClient();
           try
            {
                client.Timeout = TimeSpan.FromSeconds(5);
                var uri = "http://118.163.50.93/ItemDetailData/?Mac=" + mac+"&itype="+i_type.ToString();
                var response = await client.GetAsync(uri);

                // on error throw a exception  
                //var result = response.EnsureSuccessStatusCode();
                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                { 
                // handling the answer  
                    var responString = await response.Content.ReadAsStringAsync();
                    if (responString.IndexOf("<") == -1 && responString.Length > 0)    //若有錯誤就不做//如逾時或錯誤卜息
                    {
                        var json = JsonConvert.DeserializeObject<List<DeviceBsData_2>>(responString);

                        LoadImg.IsVisible = false;

                        if (i_type == 1)
                        {
                            type1.IsVisible = true;
                            type2.IsVisible = false;

                            var list = new List<DeviceBsData_2>();
                            for (int i = 0; i < json.Count; i++)
                            {
                                list.Add(new DeviceBsData_2
                                {
                                    addr = json[i].addr,
                                    temp01 = json[i].temp01,
                                    temp02 = json[i].temp02,
                                    temp03 = json[i].temp03,
                                    temp04 = json[i].temp04,
                                    temp05 = json[i].temp05,
                                    temp06 = json[i].temp06,
                                    temp07 = json[i].temp07,
                                    temp08 = json[i].temp08,
                                    temp09 = json[i].temp09,
                                    temp10 = json[i].temp10,
                                });
                            }
                            type1.ItemsSource = list;
                        }
                        else
                        {
                            type1.IsVisible = false;
                            type2.IsVisible = true;
                            var list = new List<DeviceBsData_2>();

                            for (int i = 0; i < json.Count; i++)
                            {
                                list.Add(new DeviceBsData_2
                                {
                                    addr = json[i].addr,
                                    temp01 = json[i].temp01,
                                    temp02 = json[i].temp02,
                                    temp03 = json[i].temp03,
                                    temp04 = json[i].temp04,
                                    temp05 = json[i].temp05,
                                });
                            }
                            type2.ItemsSource = list;
                        }
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

        private async Task<int> Get_type(string dev_mac)
        {
            var mac = dev_mac.Substring(dev_mac.IndexOf("(") + 1, 17);
            var i_type = 0;
            var client = new HttpClient();

            try{
                client.Timeout = TimeSpan.FromSeconds(5);
                var content = new StringContent("json", Encoding.UTF8, "application/json");
                var Account = Preferences.Get("UserID", "");
                var uri = "http://118.163.50.93/get_type/?Mac=" + mac;
                var result = await client.PostAsync(uri, content);

                // on error throw a exception  
                //result.EnsureSuccessStatusCode();
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {

                    // handling the answer  
                    var resultString = await result.Content.ReadAsStringAsync();
                    if (resultString.IndexOf("<") == -1 && resultString.Length > 0)    //若有錯誤就不做//如逾時或錯誤卜息
                    {
                        var json = JsonConvert.DeserializeObject<Int32>(resultString);
                        i_type = json;
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                client.Dispose();
            }
            return i_type;
        }

        
    }
}