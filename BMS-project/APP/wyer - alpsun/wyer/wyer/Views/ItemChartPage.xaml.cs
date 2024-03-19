using Microcharts.Forms;
using Microcharts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using wyer.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp.Views.Forms;
using wyer.ViewModels;
using SkiaSharp;
using System.ComponentModel.Design;
using System.Diagnostics;

namespace wyer.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemChartPage : ContentPage
	{
        public int dev_type = 0;
        public List<String> list = new List<String>();
        public bool select_check = false;

        List<DeviceOrdersCount> orderCounts;

        public ItemChartPage ()
		{
            Title = "Data Chart";
            InitializeComponent ();

            Type1.IsVisible = false;
            Type2.IsVisible = false;
            GetDeviceName();
            if (Preferences.Get("UserID", "") != "")
            {
                DeviceSelect.mac = Preferences.Get("DeviceMac", "");
                DeviceSelect.name = Preferences.Get("DeviceName", "");
                DeviceSelect.batteryname = Preferences.Get("batteryname", "");
            }
            else
            {
                DeviceSelect.name=TempData.T_name ;
                DeviceSelect.mac=TempData.T_mac ;
                DeviceSelect.batteryname=TempData.T_battery ;
            }

            if (DeviceSelect.mac != "")
            {
                Get_data("today");
                Title = "Device- "+ DeviceSelect.mac;
                
                select_check = true;
                //liname.SelectedIndex= Int32.Parse(Preferences.Get("SelectIndex",""));

            }
            else
            {
                LoadImg.IsVisible = false;
            }
            selectday.IsVisible = false;    //單獨選擇日期顯示日期, 初入畫面不顯示
        }

        protected override void OnAppearing()
        {
            IsBusy = true;
            base.OnAppearing();
            //_ChartviewModel.OnAppearing();
            int selectedIndex = liname.SelectedIndex;

            if (selectedIndex != -1)
                select_check = true;
        }

        public void Search_DataClick(object sender, EventArgs e)
        {
            var Gdate = CalDate.Date.ToString("yyyy-MM-dd");

            Get_data(Gdate);
        }

        public void onDayClicked(object sender,EventArgs e)
        {
            CalDate.Focus();
        }

        async Task GetDeviceName()
        {
            bool type1write_1time = false;
            var client = new HttpClient();
            try
            {
                client.Timeout = TimeSpan.FromSeconds(5);
                var content = new StringContent("json", Encoding.UTF8, "application/json");
                var Account = Preferences.Get("UserID", "");
                if (Preferences.Get("UserID", "") == "")
                    Account = TempData.T_id;

                var uri = "http://118.163.50.93/dev_items/?ID=" + Account;
                var result = await client.PostAsync(uri, content);

                // on error throw a exception  
                //result.EnsureSuccessStatusCode();
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {// handling the answer  
                    var resultString = await result.Content.ReadAsStringAsync();
                    if (resultString.IndexOf("<") == -1 && resultString.Length > 0)    //若有錯誤就不做//如逾時或錯誤卜息
                    {
                        var json = JsonConvert.DeserializeObject<DevDataGet[]>(resultString);
                        //塞到item中
                        var s = json.Length;
                        for (int i = 0; i < s; i++)
                        {   //塞值到Model Class 的 Item中
                            if (json[i].i_type == "1" && type1write_1time == false)
                            {   //type1 只寫一次不用辨視電池代號
                                liname.Items.Add(json[i].dev_name + "(" + json[i].dev_mac + ")");
                                list.Add(json[i].addr);
                                type1write_1time = true;
                            }
                            if (json[i].i_type != "1")
                            {
                                liname.Items.Add(json[i].dev_name + "(" + json[i].dev_mac + ")-" + i);
                                list.Add(json[i].addr);
                            }
                        }
                    }
                }
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                client.Dispose();
            }
        }

        void OnPickerSelectedIndexChanged(object sender, EventArgs args)
        {
            int selectedIndex = liname.SelectedIndex;

            if (selectedIndex == -1)
                return;
            else
                select_check = true;

            string selectedItem = liname.Items[selectedIndex];

            var mac = selectedItem.Substring(selectedItem.IndexOf("(") + 1, 17);
            var name = selectedItem.Substring(0, selectedItem.IndexOf("(") );
            var batteryname = list[selectedIndex];
            Preferences.Set("DeviceMac", mac);
            Preferences.Set("DeviceName", name);
            Preferences.Set("batteryname", batteryname);
            Preferences.Set("SelectIndex", selectedIndex);
            DeviceSelect.mac = mac;
            DeviceSelect.name = name;
            DeviceSelect.batteryname = batteryname;
            Title = "Device- " +  name;
            //神奇的方法
            PropertyInfo propertyInfo = typeof(Keyboard).GetRuntimeProperty(selectedItem);
            //entry.Keyboard = (Keyboard)propertyInfo.GetValue(null);

            //自動跳到today btn
            Get_data("today");
            Clear_ButtonColor();
            TodayBtn.BackgroundColor = Color.FromHex("28487C");
            TodayBtn.TextColor = Color.FromHex("fff");
        }

        async void onPickerClicked(object sender,EventArgs e)
        {
            liname.Items.Clear();
            await GetDeviceName();
            liname.Focus();
        }

        private async Task Get_type(string dev_mac)
        {
            var i_type = 0;
            var client = new HttpClient();
            try
            {
                client.Timeout = TimeSpan.FromSeconds(5);
                var content = new StringContent("json", Encoding.UTF8, "application/json");
                var Account = Preferences.Get("UserID", "");
                if (Preferences.Get("UserID", "") == "")
                    Account = TempData.T_id;

                var uri = "http://118.163.50.93/get_type/?Mac=" + dev_mac;
                var result = await client.PostAsync(uri, content);

                // on error throw a exception  
                //result.EnsureSuccessStatusCode();
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {// handling the answer  
                    var resultString = await result.Content.ReadAsStringAsync();
                    if (resultString.IndexOf("<") == -1 && resultString.Length > 0)    //若有錯誤就不做//如逾時或錯誤卜息
                    {
                        var json = JsonConvert.DeserializeObject<Int32>(resultString);
                        i_type = json;
                    }
                }
                dev_type = i_type;
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                client.Dispose();
            }
            
        }

        private async void Get_data(String rang)
        {
            nodata.IsVisible = false;
            LoadImg.IsVisible = true;
            Type1.IsVisible = false;
            Type2.IsVisible = false;
            int selectedIndex = liname.SelectedIndex;
            orderCounts = new List<DeviceOrdersCount>();

            if (selectedIndex != -1 || DeviceSelect.mac != "")
            {
                
                var mac = DeviceSelect.mac;
                if (DeviceSelect.mac == "")
                {
                    var select_combox = liname.Items[selectedIndex];
                    mac = select_combox.Substring(select_combox.IndexOf("(") + 1, 17);
                }

                //取得device type
                await Get_type(mac);

                var client = new HttpClient();
                try
                {
                    client.Timeout = TimeSpan.FromSeconds(5);
                    var content = new StringContent("json", Encoding.UTF8, "application/json");
                    var Account = Preferences.Get("UserID", "");
                    if (Preferences.Get("UserID", "") == "")
                        Account = TempData.T_id;

                    var uri = "http://118.163.50.93/analyze/?Mac=" + mac + "&battery=" + DeviceSelect.batteryname + "&itype=" + rang;
                    var result = await client.PostAsync(uri, content);


                    // on error throw a exception  
                    //result.EnsureSuccessStatusCode();
                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    { // handling the answer  
                        var resultString = await result.Content.ReadAsStringAsync();
                        if (resultString.IndexOf("<") == -1 && resultString.Length > 0)    //若有錯誤就不做//如逾時或錯誤卜息
                        {
                            var json = JsonConvert.DeserializeObject<List<DevArray>>(resultString);

                            //loging 關閉, 開啟圖表
                            LoadImg.IsVisible = false;
                            DateTime dtDate;

                            var day = DateTime.TryParse(rang.ToString(), out dtDate);
                            if (day == true)
                            {
                                selectday.IsVisible = true;
                                selectday.Text = rang;
                                Clear_ButtonColor();
                            }

                            //將資料寫入textl裡
                            if (json != null && json.Count > 0) //有值
                            {
                                switch (dev_type)
                                {
                                    case 1:
                                        Type1.IsVisible = true;
                                        Type2.IsVisible = false;

                                        var entries = new List<ChartEntry>();
                                        var entries1 = draw_entries(json, entries, "voltage1", rang);
                                        var lineChart1 = new DonutChart()
                                        {
                                            Entries = entries1,
                                            LabelTextSize = 24,
                                        };
                                        entries = new List<ChartEntry>();
                                        draw_entries(json, entries, "InputPower", rang);
                                        var lineChart2 = new RadarChart()
                                        {
                                            Entries = entries,
                                            LabelTextSize = 24,
                                        };
                                        entries = new List<ChartEntry>();
                                        draw_entries(json, entries, "OutputPower", rang);
                                        var lineChart3 = new RadarChart()
                                        {
                                            Entries = entries,
                                            LabelTextSize = 24,
                                        };
                                        entries = new List<ChartEntry>();
                                        draw_entries(json, entries, "InputFrequency", rang);
                                        var lineChart4 = new RadialGaugeChart()
                                        {
                                            Entries = entries,
                                            LabelTextSize = 24,
                                        };
                                        entries = new List<ChartEntry>();
                                        draw_entries(json, entries, "OutputVoltage", rang);
                                        var lineChart5 = new LineChart()
                                        {
                                            Entries = entries,
                                            LabelTextSize = 24,
                                        };
                                        entries = new List<ChartEntry>();
                                        draw_entries(json, entries, "OutputCurrent", rang);
                                        var lineChart6 = new LineChart()
                                        {
                                            Entries = entries,
                                            LabelTextSize = 24,
                                        };
                                        entries = new List<ChartEntry>();
                                        draw_entries(json, entries, "BatteryVoltage", rang);
                                        var lineChart7 = new LineChart()
                                        {
                                            Entries = entries,
                                            LabelTextSize = 24,
                                        };

                                        this.voltage1Chart.Chart = lineChart1;
                                        this.InputPowerChart.Chart = lineChart2;
                                        this.OutputPowerChart.Chart = lineChart3;
                                        this.InputFrequencyChart.Chart = lineChart4;
                                        this.OutputVoltageChart.Chart = lineChart5;
                                        this.OutputCurrentChart.Chart = lineChart6;
                                        this.BatteryVoltageChart.Chart = lineChart7;
                                        break;
                                    case 2:
                                        Type2.IsVisible = true;
                                        Type1.IsVisible = false;

                                        entries = new List<ChartEntry>();
                                        var entries11 = draw_entries(json, entries, "power2", rang);
                                        var lineChart11 = new LineChart()
                                        {
                                            Entries = entries11,
                                            LabelTextSize = 24,
                                        };
                                        this.power2Chart.Chart = lineChart11;

                                        entries = new List<ChartEntry>();
                                        var entries12 = draw_entries(json, entries, "battery", rang);
                                        var lineChart12 = new BarChart()
                                        {
                                            Entries = entries12,
                                            LabelTextSize = 24,
                                        };
                                        this.batteryChart.Chart = lineChart12;

                                        var entries13 = new List<ChartEntry>();
                                        entries13 = draw_entries(json, entries13, "temp1", rang);
                                        var lineChart13 = new LineChart()
                                        {
                                            Entries = entries13,
                                            LabelTextSize = 24,
                                        };
                                        this.Temp1Chart.Chart = lineChart13;

                                        var entries14 = new List<ChartEntry>();
                                        entries14 = draw_entries(json, entries14, "temp2", rang);
                                        var lineChart14 = new LineChart()
                                        {
                                            Entries = entries14,
                                            LabelTextSize = 24,
                                        };

                                        var entries15 = new List<ChartEntry>();
                                        entries15 = draw_entries(json, entries15, "temp3", rang);
                                        var lineChart15 = new LineChart()
                                        {
                                            Entries = entries15,
                                            LabelTextSize = 24,
                                        };

                                        var entries16 = new List<ChartEntry>();
                                        entries16 = draw_entries(json, entries16, "temp4", rang);
                                        var lineChart16 = new LineChart()
                                        {
                                            Entries = entries16,
                                            LabelTextSize = 24,
                                        };
                                        var entries17 = new List<ChartEntry>();
                                        entries15 = draw_entries(json, entries17, "temp5", rang);
                                        var lineChart17 = new LineChart()
                                        {
                                            Entries = entries17,
                                            LabelTextSize = 24,
                                        };

                                        var entries18 = new List<ChartEntry>();
                                        entries16 = draw_entries(json, entries18, "temp6", rang);
                                        var lineChart18 = new LineChart()
                                        {
                                            Entries = entries18,
                                            LabelTextSize = 24,
                                        };
                                        this.Temp2Chart.Chart = lineChart14;
                                        this.Temp3Chart.Chart = lineChart15;
                                        this.Temp4Chart.Chart = lineChart16;
                                        this.Temp5Chart.Chart = lineChart17;
                                        this.Temp6Chart.Chart = lineChart18;
                                        break;
                                }
                            }
                            else
                            {
                                nodata.IsVisible = true;
                                nodata.Text = "No Data!";
                                //沒資料時
                            }
                        }
                        else
                        {
                            LoadImg.IsVisible = false;
                            nodata.IsVisible = true;
                            nodata.Text = "No Data!";
                        }
                    }
                }catch(Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                finally
                {
                    client.Dispose();
                }

            }

        }

        private List<ChartEntry> draw_entries(List<DevArray> json, List<ChartEntry> entries, string str,string stime)
        {
            orderCounts.Clear();

            for (int i = 0; i < json.Count(); i++)
            {
                var shorttime = "";
                float temp = 0;
                var myDate = DateTime.Now;
                string myDateString = myDate.ToString("yyyy-MM-dd  ") + i.ToString();
                switch (stime)
                {
                    case "today":
                        shorttime = i.ToString();
                        break;
                    default:
                        var total = i + 1;
                        shorttime = total.ToString();
                        break;
                }

                switch (str)
                {
                    case "voltage1":
                        orderCounts.Add(new DeviceOrdersCount { Titalname = json[i].Dat_time.Substring(6, (json[i].Dat_time.Length-6)), OrderCount = float.Parse(json[i].A002) });
                        break;
                    case "InputPower":
                        orderCounts.Add(new DeviceOrdersCount { Titalname = shorttime, OrderCount = float.Parse(json[i].A003) });
                        break;
                    case "OutputPower":
                        orderCounts.Add(new DeviceOrdersCount { Titalname = shorttime, OrderCount = float.Parse(json[i].A005) });
                        break;
                    case "InputFrequency":
                        orderCounts.Add(new DeviceOrdersCount { Titalname = shorttime, OrderCount = float.Parse(json[i].A006) });
                        break;
                    case "OutputVoltage":
                        orderCounts.Add(new DeviceOrdersCount { Titalname = shorttime, OrderCount = float.Parse(json[i].A007) });
                        break;
                    case "OutputCurrent":
                        orderCounts.Add(new DeviceOrdersCount { Titalname = shorttime, OrderCount = float.Parse(json[i].A010) });
                        break;
                    case "BatteryVoltage":
                        orderCounts.Add(new DeviceOrdersCount { Titalname = shorttime, OrderCount = float.Parse(json[i].A010) });
                        break;
                    case "InvertorWorkMode":
                        orderCounts.Add(new DeviceOrdersCount { Titalname = shorttime, OrderCount = float.Parse(json[i].A010) });
                        break; 
                    case "BatteryCheckMode":
                        orderCounts.Add(new DeviceOrdersCount { Titalname = shorttime, OrderCount = float.Parse(json[i].A010) });
                        break; 

                    case "power2":
                        float totalvalue = 0;
                        totalvalue = float.Parse(json[i].A002) * float.Parse(json[i].A003);
                        switch (stime)
                        {
                            case "today":
                                totalvalue=float.Parse(Math.Round((totalvalue * float.Parse("0.000001") * float.Parse("0.00111")), 2).ToString());
                                break;
                            case "week":
                                totalvalue = float.Parse(Math.Round((totalvalue * float.Parse("0.000001") * float.Parse("0.0000463")), 2).ToString());
                                break;
                            case "month":
                                totalvalue = float.Parse(Math.Round((totalvalue * float.Parse("0.000001") * float.Parse("0.0000463")), 2).ToString());
                                break;
                            case "year":
                                totalvalue = float.Parse(Math.Round((totalvalue * float.Parse("0.000001") * float.Parse("0.00000154")), 2).ToString());
                                break;
                            default:
                                totalvalue = float.Parse(Math.Round((totalvalue * float.Parse("0.000001") * float.Parse("0.00111")), 2).ToString());
                                break;
                        }
                        orderCounts.Add(new DeviceOrdersCount { Titalname = shorttime, OrderCount = totalvalue });
                        break;
                    case "battery":
                        orderCounts.Add(new DeviceOrdersCount { Titalname = shorttime, OrderCount = float.Parse(json[i].A004) });
                        break;
                    case "temp1":
                        if (float.Parse(json[i].A005) == 0)
                            temp = 0;
                        else
                            temp = float.Parse(Math.Round((float.Parse(json[i].A005) - 273) * float.Parse("0.01"), 2).ToString());
                        orderCounts.Add(new DeviceOrdersCount { Titalname = shorttime, OrderCount = temp });
                        break;
                    case "temp2":
                        if (float.Parse(json[i].A006) == 0)
                            temp = 0;
                        else
                            temp = float.Parse(Math.Round((float.Parse(json[i].A006) - 273) * float.Parse("0.01"), 2).ToString());
                        orderCounts.Add(new DeviceOrdersCount { Titalname = shorttime, OrderCount = temp });
                        break;
                    case "temp3":
                        if (float.Parse(json[i].A007) == 0)
                            temp = 0;
                        else
                            temp = float.Parse(Math.Round((float.Parse(json[i].A007) - 273) * float.Parse("0.01"), 2).ToString());
                        orderCounts.Add(new DeviceOrdersCount { Titalname = shorttime, OrderCount = temp });
                        break;
                    case "temp4":
                        if (float.Parse(json[i].A008) == 0)
                            temp = 0;
                        else
                            temp = float.Parse(Math.Round((float.Parse(json[i].A008) - 273) * float.Parse("0.01"), 2).ToString());
                        orderCounts.Add(new DeviceOrdersCount { Titalname = shorttime, OrderCount = temp});
                        break;
                    case "temp5":
                        if (float.Parse(json[i].A009) == 0)
                            temp = 0;
                        else
                            temp = float.Parse(Math.Round((float.Parse(json[i].A009) - 273) * float.Parse("0.01"), 2).ToString());
                        orderCounts.Add(new DeviceOrdersCount { Titalname = shorttime, OrderCount = temp });
                        break;
                    case "temp6":
                        if (float.Parse(json[i].A010) == 0)
                            temp = 0;
                        else
                            temp = float.Parse(Math.Round((float.Parse(json[i].A010) - 273) * float.Parse("0.01"), 2).ToString());
                        orderCounts.Add(new DeviceOrdersCount { Titalname = shorttime, OrderCount = temp });
                        break;
                }
                
            }
            foreach (var data in orderCounts)
            {
                Random ran = new Random();
                SKColor randomColor = SKColor.FromHsv(ran.Next(256), ran.Next(256), ran.Next(256), 0xFF);
                var entry = new ChartEntry(data.OrderCount)
                {
                    Label = data.Titalname,
                    ValueLabel = data.OrderCount.ToString(),
                    Color = randomColor
                };
                entries.Add(entry);
            }
            return entries;
        }

        private List<RealList> Getlabel(List<SinglefieldArray> json, String str)
        {
            var lab = new List<RealList>();
            var i = 0;
            var i_img = i;
            for (i = 0; i < json.Count(); i++)
            {
                var myDate = DateTime.Now;
                string myDateString = myDate.ToString("yyyy-MM-dd  ") + i.ToString();
                if (i == 12)
                    i_img = 0;
                switch (str)
                {
                    case "power2":
                        float totalvalue2 = 0;
                        totalvalue2 = float.Parse((Math.Round(float.Parse(json[i].A002) * float.Parse(json[i].A003) * float.Parse("0.000001") * float.Parse("0.00111"), 2)).ToString());
                        lab.Add(new RealList { AllDb = myDateString, Name = totalvalue2.ToString(), Unit = "Kw", ImageUrl = "clock" + i_img + ".png" });
                        break;
                    case "Temp1":
                        lab.Add(new RealList { AllDb = myDateString, Name = Math.Round(float.Parse(json[i].A008) * float.Parse("0.01"), 2).ToString(), Unit = "C", ImageUrl = "clock" + i_img + ".png" });
                        break;
                    case "Temp2":
                        lab.Add(new RealList { AllDb = myDateString, Name = Math.Round(float.Parse(json[i].A009) * float.Parse("0.01"), 2).ToString(), Unit = "C", ImageUrl = "clock" + i_img + ".png" });
                        break;
                    case "Temp3":
                        lab.Add(new RealList { AllDb = myDateString, Name = Math.Round(float.Parse(json[i].A010) * float.Parse("0.01"), 2).ToString(), Unit = "C", ImageUrl = "clock" + i_img + ".png" });
                        break;
                    case "Temp4":
                        lab.Add(new RealList { AllDb = myDateString, Name = Math.Round(float.Parse(json[i].A011) * float.Parse("0.01"), 2).ToString(), Unit = "C", ImageUrl = "clock" + i_img + ".png" });
                        break;
                }
                i_img++;
            }
            return lab;
        }

        private void Today_Click(object sender,EventArgs e)
        {
            if (select_check == true)
            {
                selectday.Text = "";
                Get_data("today");
                Clear_ButtonColor();
                TodayBtn.BackgroundColor = Color.FromHex("28487C");
                TodayBtn.BorderColor = Color.FromHex("fff");
                TodayBtn.BorderWidth = 1;
                TodayBtn.TextColor = Color.FromHex("fff");
            }
            else
            {
                DisplayAlert("Info","Please select Device fisrt!", "OK");
            }
        }

        private void Week_Click(object sender, EventArgs e)
        {
            if (select_check == true)
            {
                selectday.Text = "";
                Get_data("week");
                Clear_ButtonColor();
                WeekBtn.BackgroundColor = Color.FromHex("28487C");
                WeekBtn.TextColor = Color.FromHex("fff");
                WeekBtn.BorderColor = Color.FromHex("fff");
                WeekBtn.BorderWidth = 1;
            }
            else
            {
                DisplayAlert("Info", "Please select Device fisrt!", "OK");
            }
        }

        private void Month_Click(object sender, EventArgs e)
        {
            if (select_check == true)
            {
                selectday.Text = "";
                Get_data("month");
                Clear_ButtonColor();
                MonthBtn.BackgroundColor = Color.FromHex("28487C");
                MonthBtn.TextColor = Color.FromHex("fff");
                MonthBtn.BorderColor = Color.FromHex("fff");
                MonthBtn.BorderWidth = 1;
            }
            else
            {
                DisplayAlert("Info", "Please select Device fisrt!", "OK");
            }
        }

        private void Year_Click(object sender, EventArgs e)
        {
            if (select_check == true)
            {
                selectday.Text = "";
                Get_data("year");
                Clear_ButtonColor();
                YearBtn.BackgroundColor = Color.FromHex("28487C");
                YearBtn.TextColor = Color.FromHex("fff");
                YearBtn.BorderColor = Color.FromHex("fff");
                YearBtn.BorderWidth = 1;
            }
            else
            {
                DisplayAlert("Info", "Please select Device fisrt!", "OK");
            }
        }

        private void Clear_ButtonColor()
        {
            TodayBtn.BackgroundColor = Color.FromHex("B9EDFF");
            TodayBtn.BorderColor = Color.FromHex("fff");
            TodayBtn.BorderWidth = 1;
            TodayBtn.TextColor = Color.FromHex("0070C0");
            WeekBtn.BackgroundColor = Color.FromHex("B9EDFF");
            WeekBtn.BorderColor = Color.FromHex("fff");
            WeekBtn.BorderWidth = 1;
            WeekBtn.TextColor = Color.FromHex("0070C0");
            MonthBtn.BackgroundColor = Color.FromHex("B9EDFF");
            MonthBtn.BorderColor = Color.FromHex("fff");
            MonthBtn.BorderWidth = 1;
            MonthBtn.TextColor = Color.FromHex("0070C0");
            YearBtn.BackgroundColor = Color.FromHex("B9EDFF");
            YearBtn.BorderColor = Color.FromHex("fff");
            YearBtn.BorderWidth = 1;
            YearBtn.TextColor = Color.FromHex("0070C0");
        }

    }
}