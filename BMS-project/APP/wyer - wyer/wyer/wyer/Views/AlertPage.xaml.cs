using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wyer.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Xamarin.Essentials;
using System.ComponentModel;
using System.Diagnostics;

namespace wyer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class AlertPage : ContentPage
    {
         List<ListViewItem> ListCount=new List<ListViewItem>();

        public AlertPage()
        {
            InitializeComponent();
            Title = "Alert Page";
            DeviceSelect.mac = Preferences.Get("DeviceMac", "");
            Get_Data();
        }

        async private void Get_Data()
        {
            int i = 0;
            var Account = Preferences.Get("UserID", "");
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(10);
                    var content = new StringContent("json", Encoding.UTF8, "application/json");
                    var uri = "http://118.163.50.93/get_alertdata/?ID=" + DeviceSelect.mac + "&battery=" + DeviceSelect.batteryname;
                    var result = await client.PostAsync(uri, content);

                    // on error throw a exception  
                    result.EnsureSuccessStatusCode();

                    // handling the answer  
                    var resultString = await result.Content.ReadAsStringAsync();
                    if (resultString.IndexOf("<") == -1 && resultString.Length > 0)    //若有錯誤就不做//如逾時或錯誤訊息
                    {
                        var json = JsonConvert.DeserializeObject<List<AlertData>>(resultString);

                        for (i = 0; i < json.Count; i++)
                        {
                            var sDate = Convert.ToDateTime(json[i].dat_time).ToString("yyy/MM/dd \n hh:mm:s");
                            ListCount.Add(new ListViewItem { Title = json[i].dev_name, Dtime = sDate, Description = json[i].s_value });
                        }
                        AlertList.ItemsSource = ListCount;
                        //資料抓取時會將資料庫的int_status數字由0變1, 代表己讀取, 資料由新到舊排序

                        //沒資料時
                        if (json.Count == 0)
                        {
                            msgText.IsVisible = true;
                        }
                    }

                }
            } catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}