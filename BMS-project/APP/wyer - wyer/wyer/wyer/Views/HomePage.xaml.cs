using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using wyer.Models;
using wyer.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.TizenSpecific;
using static System.Collections.Specialized.BitVector32;
using static System.Net.WebRequestMethods;

namespace wyer.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    
    public partial class HomePage : ContentPage
    {
        public int dev_type = 0;
        public int utc_local = 0;
        public int checktime = 0;
        public List<String> list = new List<String>();
        public static bool connected = true;

        public HomePage()
        {
            InitializeComponent();

            Type1.IsVisible = false;
            Type2.IsVisible = false;
            nodata.IsVisible = false;

            DeviceSelect.mac = Preferences.Get("DeviceMac", "");
            DeviceSelect.name = Preferences.Get("DeviceName", "");
            DeviceSelect.batteryname = Preferences.Get("batteryname", "");
            var Account = Preferences.Get("UserID", "");
            //GetDeviceName();

            if (DeviceSelect.mac != "")
            {
                LoadImg.IsVisible = true;  //動畫動
                device_name.Text = DeviceSelect.name;
                device_mac.Text= "("+DeviceSelect.mac+")";
                Get_data();
            }
            else
            {
                LoadImg.IsVisible = false;  //動畫停止
                nodata.IsVisible = true;
                nodata_txt.Text = "Please go to List page to add device\n and selecting one device.";
            }

            //台灣和格林威治差8小時, 修正時間
            //抓手機時間
            TimeZoneInfo a = TimeZoneInfo.Local;
            //手機與UTC二地相差時間
            TimeSpan timeoff = a.BaseUtcOffset;
            //取得手機時間和台灣的小時差
            utc_local = 8-timeoff.Hours;
        }

        async void OpenAlert_Clicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new Views.AlertPage(),true);
        }

        async void OpenSystem(object sender, EventArgs args)
        {
            var NextPage = new Views.SystemPage();
            await Navigation.PushAsync(NextPage);
        }

        async void OpenWebCommand(object sender, EventArgs e)
        {
            //await Browser.OpenAsync("https://www.yahoo.com");
            var weburl = new Views.ControlPage("https://www.yahoo.com");
            await Navigation.PushAsync(weburl);
        }

        async Task GetDeviceName()
        {
            var Account = Preferences.Get("UserID", "");
            var client = new HttpClient();
            try
            {
                client.Timeout = TimeSpan.FromSeconds(5);
                var content = new StringContent("json", Encoding.UTF8, "application/json");
                var uri = "http://118.163.50.93/dev_items/?ID=" + Account;
                var result = await client.PostAsync(uri, content);

                // on error throw a exception  
                //result.EnsureSuccessStatusCode();
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    // handling the answer  
                    var resultString = await result.Content.ReadAsStringAsync();
                    if (resultString.IndexOf("<") == -1 && resultString.Length > 0)   //若有錯誤就不做//如逾時或錯誤訊息
                    {
                        var json = JsonConvert.DeserializeObject<DevDataGet[]>(resultString);
                        //塞到item中
                        var s = json.Length;
                        list.Clear(); //list清空

                        bool type1write_1time = false;
                        for (int i = 0; i < s; i++)
                        {   //塞值到Model Class 的 Item中
                            if (json[i].i_type == "1" && type1write_1time == false)
                            {   //type1 只寫一次不用辨視電池代號
                                liname.Items.Add(json[i].dev_name + "(" + json[i].dev_mac + ")" );
                                //將資料存入陣列
                                list.Add(json[i].addr);
                                type1write_1time = true;
                            }
                            if (json[i].i_type != "1")
                            {
                                liname.Items.Add(json[i].dev_name + "(" + json[i].dev_mac + ")-" + json[i].addr.Substring(0, 2));
                                list.Add(json[i].addr);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                client.Dispose();
            }
        }


        private void OnPickerSelectedIndexChanged(object sender, EventArgs args)
        {

            int selectedIndex = liname.SelectedIndex;

            if (selectedIndex == -1)
                return;

            string selectedItem = liname.Items[selectedIndex];

            //記下選擇的設備,之後的頁面可以使用
            var mac = selectedItem.Substring(selectedItem.IndexOf("(") + 1, 17);
            var name = selectedItem.Substring(0, selectedItem.IndexOf("(") );
            var batteryname = list[selectedIndex];
            Preferences.Set("DeviceMac", mac);
            Preferences.Set("DeviceName", name);
            Preferences.Set("batteryname", batteryname);

            //寫入變數中
            DeviceSelect.mac = mac;
            DeviceSelect.name = name;
            DeviceSelect.batteryname = batteryname;
            //寫到home的device name lable中
            device_name.Text = DeviceSelect.name;
            device_mac.Text = "(" + DeviceSelect.mac + ")";
            //神奇的方法
            PropertyInfo propertyInfo = typeof(Keyboard).GetRuntimeProperty(selectedItem);
            //entry.Keyboard = (Keyboard)propertyInfo.GetValue(null);
            Get_data();
        }

        private async Task Get_type(string dev_mac)
        {
            var i_type = 0;
            var client = new HttpClient();
            try
            {
                client.Timeout = TimeSpan.FromSeconds(5);
                var content = new StringContent("json", Encoding.UTF8, "application/json");
                var jsonContent = new StringContent(JsonConvert.SerializeObject(new { Mac = dev_mac }),
                   Encoding.UTF8,
                   "application/json");
                var uri = "http://118.163.50.93/get_type/?Mac=" + dev_mac;
                var result = await client.PostAsync(uri, jsonContent);

                // on error throw a exception  
                result.EnsureSuccessStatusCode();
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    // handling the answer  
                    var resultString = await result.Content.ReadAsStringAsync();
                    if (resultString.IndexOf("<") == -1 && resultString.Length > 0)   //若有錯誤就不做//如逾時或錯誤卜息
                    {
                        var json = JsonConvert.DeserializeObject<Int32>(resultString);
                        i_type = json;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                client.Dispose();
            }
            dev_type= i_type;
        }

        private async void Get_data()
        {
            var mac = "";
            //init時已判斷是否有記憶device mac若沒有不會進入這裡. 在select picker時, 也會填入device mac

            int selectedIndex = liname.SelectedIndex;

            if (DeviceSelect.mac != "")
            {
                mac = DeviceSelect.mac;
            }
            else if (selectedIndex != -1)
            {
                var select_combox = liname.Items[selectedIndex];
                mac = select_combox.Substring(select_combox.IndexOf("(") + 1, 17);
            }

            if (mac != "") //有初值或是有選擇devoice時, 否則為剛login或是新裝機者
            {
                //取得device type
                await Get_type(mac);
                var client = new HttpClient();
                try
                {
                    client.Timeout = TimeSpan.FromSeconds(5);
                    var content = new StringContent("json", Encoding.UTF8, "application/json");
                    var jsonContent = new StringContent(JsonConvert.SerializeObject(new { Mac = mac }),
                       Encoding.UTF8,
                       "application/json");
                    var uri = "http://118.163.50.93/get_realdata/?Mac=" + mac + "&battery=" + DeviceSelect.batteryname + "&itype=" + dev_type;
                    //var uri= "http://118.163.50.93/get_alertdata/";
                    var result = await client.PostAsync(uri, content);

                    // on error throw a exception  
                    //result.EnsureSuccessStatusCode();
                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        // handling the answer  
                        var resultString = await result.Content.ReadAsStringAsync();
                        if (resultString.IndexOf("<") == -1 && resultString.Length > 0)    //若有錯誤就不做//如逾時或錯誤卜息
                        {
                            var json = JsonConvert.DeserializeObject<List<Dev2Array>>(resultString);
                            connected = true;
                            LoadImg.IsVisible = false;  //動畫停止

                            //將資料寫入textl裡
                            if (json.Count > 0)
                            {
                                switch (dev_type)
                                {
                                    case 1:
                                        Type1.IsVisible = true;
                                        Type2.IsVisible = false;
                                        nodata.IsVisible = false;

                                        voltage_value.Text = json[1].A002 + " V";
                                        inputpower_value.Text = json[1].A003 + " ";
                                        outputpower_value.Text = json[1].A004 + " ";
                                        inputfrequency_value.Text = json[1].A005 + " Hz";
                                        OutputVoltage_value.Text = json[1].A006 + " V";
                                        if (json[1].A007.ToString() == "0")
                                            outputcurrent_value.Text = " Charging Current";
                                        else
                                            outputcurrent_value.Text = " Discharging Current";
                                        batteryvoltage_value.Text = json[1].A010 + " %";
                                        Invertormode_value.Text = json[1].A010 + " V";
                                        switch (json[1].A010)
                                        {
                                            case "0":
                                                Invertormode_value.Text = "Mains mode";
                                                break;
                                            case "1":
                                                Invertormode_value.Text = "Battery mode";
                                                break;
                                            case "3":
                                                Invertormode_value.Text = "Bypass mode";
                                                break;
                                            case "4":
                                                Invertormode_value.Text = "Standby mode";
                                                break;
                                            case "5":
                                                Invertormode_value.Text = "Failure mode";
                                                break;
                                            case "6":
                                                Invertormode_value.Text = "Power-on mode";
                                                break;
                                            case "7":
                                                Invertormode_value.Text = "Shutdown in mode";
                                                break;
                                            case "8":
                                                Invertormode_value.Text = "Charging mode";
                                                break;
                                            case "9":
                                                Invertormode_value.Text = "Battery self-test mode";
                                                break;
                                        }
                                        switch (json[1].A010)
                                        {
                                            case "0":
                                                batterymode_value.Text = "No self-test";
                                                break;
                                            case "1":
                                                batterymode_value.Text = "Self-test";
                                                break;
                                            case "2":
                                                batterymode_value.Text = "Results are normal";
                                                break;
                                            case "3":
                                                batterymode_value.Text = "Result alarm";
                                                break;
                                            case "4":
                                                batterymode_value.Text = "Self-test is prohibited";
                                                break;
                                            case "5":
                                                batterymode_value.Text = "Exit during the self-test";
                                                break;
                                            case "6":
                                                batterymode_value.Text = "Reservations";
                                                break;
                                            case "7":
                                                batterymode_value.Text = "Other values";
                                                break;
                                        }

                                        break;
                                    case 2:
                                        Type2.IsVisible = true;
                                        Type1.IsVisible = false;
                                        nodata.IsVisible = false;
                                        float temp1, temp2, temp3, temp4, temp5, temp6;

                                        if (json[0].A008 == "0")
                                            temp1 = 0;
                                        else
                                            temp1 = float.Parse(json[0].A008);
                                        if (json[0].A009 == "0")
                                            temp2 = 0;
                                        else
                                            temp2 = float.Parse(json[0].A009);
                                        if (json[0].A009 == "0")
                                            temp3 = 0;
                                        else
                                            temp3 = float.Parse(json[0].A009);
                                        if (json[0].A010 == "0")
                                            temp4 = 0;
                                        else
                                            temp4 = float.Parse(json[0].A010);
                                        if (json[0].A011 == "0")
                                            temp5 = 0;
                                        else
                                            temp5 = float.Parse(json[0].A011);
                                        if (json[0].A012 == "0")
                                            temp6 = 0;
                                        else
                                            temp6 = float.Parse(json[0].A012);

                                        voltage_value.Text = float.Parse(json[0].A002) * float.Parse("0.001") + "\n V";
                                        current_value.Text = float.Parse(json[0].A003) * float.Parse("0.01") + "\n A";
                                        power_value.Text = (float.Parse(json[0].A002) * float.Parse(json[0].A003)) * float.Parse("0.000001") + "\n Kw";
                                        temp_value.Text = "T1:" + (temp1 * float.Parse("0.01")) + "    T2:" + (temp2 * float.Parse("0.01")) + "\nT3:" + (temp3 * float.Parse("0.01")) + "    T4:" + (temp4 * float.Parse("0.01")) + "\nT5:" + (temp5 * float.Parse("0.01")) + "    T6:" + (temp6 * float.Parse("0.01")) + "\n°C";
                                        if (json[0].A007 == "0")
                                            safety_value.Text = "Normal";
                                        else
                                            safety_value.Text = "Error";

                                        Relative_value.Text = json[0].A006 + "\n %";
                                        cycle_value.Text = json[0].A004;
                                        Health_value.Text = json[0].A005 + "\n %";
                                        break;
                                }

                                var devicetime = new TimeSpan(Convert.ToDateTime(json[0].Dat_time).Ticks);
                                var dtime = new TimeSpan(DateTime.Now.AddHours(utc_local).Ticks);
                                //上方為加上與台灣小時差的時間, 與資料庫即時資料時間比對
                                var ts = dtime.TotalMinutes - devicetime.TotalMinutes;

                                if (ts < 2) //小於2分鐘顯示斷線
                                {
                                    devimg.Source = "green_light.png";
                                }
                                else
                                {
                                    devimg.Source = "red_light.png";
                                }
                                Check_alert(); //檢查是否有alert
                            }
                            else
                            {
                                stop_status();
                            }
                        }
                        else
                        {
                            stop_status();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    if (ex.Message.IndexOf("Failed to connect") != -1)
                    {
                        using (var ping = new Ping())
                        {   //網路斷線
                            var reply = ping.Send("8.8.8.8", 1000);
                            var pingResult = reply != null && reply.Status == IPStatus.Success;
                            if (pingResult == false && connected == true)
                            {
                                connected = false;
                                await DisplayAlert("Info", "Network disconnect! Please confirm the network is available.", "OK");
                                stop_status();
                            }
                        }
                    }
                }
                finally
                {
                    client.Dispose();
                }
            }
            else
            {
                nodata.IsVisible = true;
                nodata_txt.Text = "Please go to List page to add device\n and selecting one device.";
            }
        }

        private async void Check_alert()
        {
            var client = new HttpClient();
            try
            {
                client.Timeout = TimeSpan.FromSeconds(5);
                var content = new StringContent("json", Encoding.UTF8, "application/json");
                var jsonContent = new StringContent(JsonConvert.SerializeObject(new { ID = DeviceSelect.mac }),
                    Encoding.UTF8,
                    "application/json");
                var uri = "http://118.163.50.93/checkalertcount/?MAC=" + DeviceSelect.mac+"&ID="+DeviceSelect.batteryname;
                var result = await client.PostAsync(uri, jsonContent); //.ConfigureAwait(false);

                // on error throw a exception  
                result.EnsureSuccessStatusCode();

                // handling the answer  
                var resultString = await result.Content.ReadAsStringAsync();
                if (resultString.IndexOf("<") == -1 && resultString.Length > 0)   //若有錯誤就不做//如逾時或錯誤卜息
                {
                    var json = JsonConvert.DeserializeObject<Int32>(resultString);

                    //資料只抓取int_status為 0 的數量, 因為若點圖示進入下頁, int_status的數字會變成1, 表示己讀過了, 所以此處只會顯示沒讀過的訊息
                    alert_value.Text = json.ToString()+"\n new";
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                client.Dispose();
            }
        }

        //10秒更新資料
        protected async override void OnAppearing()
        {
            IsBusy = true;
            base.OnAppearing();

            int i = 0;
            double press = 0;
            await Task.Run(async () =>
            {
                await Task.Delay(100);
                Device.BeginInvokeOnMainThread(async () =>
                {
                    while (true)
                    {
                        await Device.InvokeOnMainThreadAsync(() =>
                        {
                            if (checktime >= 10)
                            {
                                Get_data(); //更新資料
                                i = 0;
                                press = 0;
                            }
                            checktime = i;
                            press = 0.1 * i;
                            progressBar.ProgressTo(press, 1000, Easing.BounceIn);
                        });
                        await Task.Delay(1000); //1000=1秒,10000=10秒
                        i++;
                    }
                    
                });
            });
        }

        async void PickOpen_Click(object sender,EventArgs e)
        {
            liname.Items.Clear();
            await GetDeviceName();
            liname.Focus();
        }

        private async void OnFrameTapped(object sender, EventArgs e)
        {
            var parameter = ((TappedEventArgs)e).Parameter;

            //var NexPage = new Views.AlertPage();
            await Navigation.PushAsync(new Views.SingleFieldPage(parameter.ToString()),true);

        }

        private void stop_status()
        {
            LoadImg.IsVisible = false;  //動畫停止
            devimg.Source = "red_light.png";
            Type2.IsVisible = false;
            Type1.IsVisible = false;
            nodata.IsVisible = true;
        }
        
    }

    
}