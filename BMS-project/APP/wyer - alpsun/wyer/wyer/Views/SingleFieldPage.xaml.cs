using Microcharts;
using Newtonsoft.Json;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using wyer.Models;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using UltimateXF.Widget.Charts.Models;
using UltimateXF.Widget.Charts.Models.Formatters;
using UltimateXF.Widget.Charts.Models.LineChart;
using UltimateXF.Widget.Charts.Models.BarChart;
using UltimateXF.Widget.Charts.Models.Component;
using UltimateXF.Widget.Charts;
using UltimateXF.Widget.Charts.Models.CombinedChart;
using Xamarin.Forms.Internals;
using UltimateXF.Widget.Charts.Models.PieChart;
using System.Runtime.InteropServices.ComTypes;
using UltimateXF.Widget.Charts.Models.BubbleChart;
using UltimateXF.Widget.Charts.Models.ScatterChart;
using System.Threading;
using System.Net;
using static Xamarin.Forms.Internals.Profile;
using UltimateXF.Widget.Charts.Models.CandleStickChart;
using System.Diagnostics;

namespace wyer.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class SingleFieldPage : ContentPage
	{
		public String parameter = "";
        List<DeviceOrdersCount> orderCounts=new List<DeviceOrdersCount>();

        public SingleFieldPage (String field)
		{
			Title = DeviceSelect.name+" - "+field;

            InitializeComponent ();
            Type1.IsVisible = false;
            Type2.IsVisible = false;
            Type3.IsVisible = false;

			parameter = field;
            RunChart();
            //LoadBarChart(); ///untimate-xf
        }

        private async Task<int> Get_type(string dev_mac)
        {
            var i_type = 0;
            var client = new HttpClient();
            try
            {
                client.Timeout = TimeSpan.FromSeconds(15);
                var content = new StringContent("json", Encoding.UTF8, "application/json");
                var Account = Preferences.Get("UserID", "");
                if (Preferences.Get("UserID", "") == "")
                    Account = TempData.T_id;
                var uri = "http://118.163.50.93/get_type/?Mac=" + dev_mac;
                var result = await client.PostAsync(uri, content);

                // on error throw a exception  
                //result.EnsureSuccessStatusCode();
                if (result.StatusCode == HttpStatusCode.OK)
                {// handling the answer  
                    var resultString = await result.Content.ReadAsStringAsync();
                    if (resultString.IndexOf("<") == -1 && resultString.Length > 0)    //若有錯誤就不做//如逾時或錯誤卜息
                    {

                        var json = JsonConvert.DeserializeObject<Int32>(resultString);
                        i_type = json;
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
            return i_type;
        }
        
        private async void RunChart()
        {
            IsBusy = true;
            var dev_type = 0;
            orderCounts = new List<DeviceOrdersCount>();

            if (DeviceSelect.mac != "")
            {

                //取得device type
                dev_type = await Get_type(DeviceSelect.mac);
                //取得欄位對應的SQL欄位名
                var sfield = await Get_field(parameter);

                draw_chart(dev_type, sfield, "today");
               /* draw_chart(dev_type, sfield, "week");
                draw_chart(dev_type, sfield, "month");
                draw_chart(dev_type, sfield, "year");*/
            }

        }

        private async Task<String> Get_field(String field)
        {
            var client = new HttpClient();
            try
            {
                client.Timeout = TimeSpan.FromSeconds(5);
                var content = new StringContent("json", Encoding.UTF8, "application/json");
                var Account = Preferences.Get("UserID", "");
                if (Preferences.Get("UserID", "") == "")
                    Account = TempData.T_id;
                var uri = "http://118.163.50.93/get_field/?field=" + field;
                var result = await client.PostAsync(uri, content);
                var json = "";


                // on error throw a exception  
                //result.EnsureSuccessStatusCode();
                if (result.StatusCode == HttpStatusCode.OK)
                {    // handling the answer  
                    var resultString = await result.Content.ReadAsStringAsync();
                    if (resultString.IndexOf("<") == -1 && resultString.Length > 0)    //若有錯誤就不做//如逾時或錯誤卜息
                    {
                        json = JsonConvert.DeserializeObject<String>(resultString);
                        if (json == null || json == "")
                            json = "1";

                    }
                }
                return json;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return ex.Message;
            }
            finally
            {
                client.Dispose();
            }
        }

        private async void draw_chart(int dev_type,String field,String dtime)
        {
            var mac = DeviceSelect.mac;
            CancellationTokenSource _cts = new CancellationTokenSource();
            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(15);  //逾時設定為15秒
            try
            {
                client.Timeout = TimeSpan.FromSeconds(5);
                var content = new StringContent("json", Encoding.UTF8, "application/json");
                var uri = "http://118.163.50.93/singlefield/?Mac=" + mac + "&battery=" + DeviceSelect.batteryname + "&itype="+dev_type+"&rang=" + dtime+"&field="+field;
                var result = await client.PostAsync(uri, content, _cts.Token);

                // on error throw a exception  
                //result.EnsureSuccessStatusCode();
                if (result.StatusCode == HttpStatusCode.OK)
                {// handling the answer  
                    var resultString = await result.Content.ReadAsStringAsync();
                    if (resultString.IndexOf("<") == -1 && resultString.Length > 0)    //若有錯誤就不做//如逾時或錯誤卜息
                    {

                        var json = JsonConvert.DeserializeObject<List<SinglefieldArray>>(resultString);
                        LoadImg.IsVisible = false;

                        //將資料寫入textl裡
                        if (json != null && json.Count > 0) //有值
                        {
                            if (dev_type == 2 && parameter != "Temperatures")
                            {
                                Type1.IsVisible = false;
                                Type2.IsVisible = true;
                                Type3.IsVisible = false;

                                XFCharts(json, parameter);
                                drawlist.ItemsSource = Getlabel(json, parameter);

                                /*switch (dtime)
                                {
                                    case "today":

                                        var entries = new List<ChartEntry>();
                                        var lineChart1 = new LineChart()
                                        {
                                            Entries = entries1,
                                            LabelTextSize = 24,
                                        };
                                        this.today2Chart.Chart = lineChart1;
                                        drawlist.ItemsSource = Getlabel(json, parameter);


                                        break;
                                    case "week":
                                        entries = new List<ChartEntry>();
                                        draw_entries(json, entries, parameter, dtime);
                                        var lineChart2 = new RadarChart()
                                        {
                                            Entries = entries,
                                            LabelTextSize = 24,
                                        };
                                        this.week2Chart.Chart = lineChart2;
                                        break;
                                    case "month":
                                        entries = new List<ChartEntry>();
                                        draw_entries(json, entries, parameter, dtime);
                                        var lineChart3 = new RadarChart()
                                        {
                                            Entries = entries,
                                            LabelTextSize = 24,
                                        };
                                        this.month2Chart.Chart = lineChart3;
                                        break;
                                    case "year":
                                        entries = new List<ChartEntry>();
                                        draw_entries(json, entries, parameter, dtime);
                                        var lineChart4 = new RadialGaugeChart()
                                        {
                                            Entries = entries,
                                            LabelTextSize = 24,
                                        };
                                        this.year2Chart.Chart = lineChart4;
                                        break;
                                }*/
                            }
                            if (dev_type == 2 && parameter == "Temperatures")
                            {
                                Type1.IsVisible = false;
                                Type3.IsVisible = true;
                                Type2.IsVisible = false;

                                var t1entries = new List<ChartEntry>();// new List<EntryChart>();
                                var t2entries = new List<EntryChart>();
                                var t3entries = new List<EntryChart>();
                                var t4entries = new List<EntryChart>();
                                var labels = new List<String>();
                                var entries = new List<ChartEntry>();

                                LoadBarChart(json);

                                /*switch (dtime)
                                {
                                    case "today":

                                        var entries1 = draw_entries(json, entries, parameter + "1", dtime);
                                        var lineChart1 = new BarChart()
                                        {
                                            Entries = entries1,
                                            LabelTextSize = 24,
                                        };
                                        //this.Temp1Chart.Chart = lineChart1;
                                        entries = new List<ChartEntry>();
                                        var entries11 = draw_entries(json, entries, parameter + "2", dtime);
                                        var lineChart11 = new BarChart()
                                        {
                                            Entries = entries11,
                                            LabelTextSize = 24,
                                        };
                                        //this.Temp2Chart.Chart = lineChart11;

                                        entries = new List<ChartEntry>();
                                        var entries12 = draw_entries(json, entries, parameter + "3", dtime);
                                        var lineChart12 = new BarChart()
                                        {
                                            Entries = entries12,
                                            LabelTextSize = 24,
                                        };
                                        this.Temp3Chart.Chart = lineChart12;

                                        entries = new List<ChartEntry>();
                                        var entries13 = draw_entries(json, entries, parameter + "4", dtime);
                                        var lineChart13 = new BarChart()
                                        {
                                            Entries = entries13,
                                            LabelTextSize = 24,
                                        };
                                        this.Temp4Chart.Chart = lineChart13;
                                        break;

                                        /* case "week":
                                       t2entries = temperatureCharts(json, "Temperatures2", dtime);
                                        labels = Getlabel(json);
                                        tempset( T2chartXF, labels, dtime);
                                        break;
                                    case "month":
                                        t3entries = temperatureCharts(json, "Temperatures3", dtime);
                                        labels = Getlabel(json);
                                        tempset( T3chartXF, labels, dtime);
                                        break;
                                    default:
                                        t4entries = temperatureCharts(json, "Temperatures4", dtime);
                                        labels = Getlabel(json);
                                        tempset( T4chartXF, labels, dtime);
                                        break;
                                }*/


                                /*switch (dtime)
                                {
                                    case "today":
                                        var entries = new List<ChartEntry>();
                                        var entries1 = draw_entries(json, entries, parameter + "1", dtime);
                                        var lineChart1 = new DonutChart()
                                        {
                                            Entries = entries1,
                                            LabelTextSize = 24,
                                        };
                                        this.todayTemp1.Chart = lineChart1;
                                        entries = new List<ChartEntry>();
                                        var entries11 = draw_entries(json, entries, parameter + "2", dtime);
                                        var lineChart11 = new DonutChart()
                                        {
                                            Entries = entries11,
                                            LabelTextSize = 24,
                                        };
                                        this.todayTemp2.Chart = lineChart11;

                                        entries = new List<ChartEntry>();
                                        var entries12 = draw_entries(json, entries, parameter + "3", dtime);
                                        var lineChart12 = new DonutChart()
                                        {
                                            Entries = entries12,
                                            LabelTextSize = 24,
                                        };
                                        this.todayTemp3.Chart = lineChart12;

                                        entries = new List<ChartEntry>();
                                        var entries13 = draw_entries(json, entries, parameter + "4", dtime);
                                        var lineChart13 = new DonutChart()
                                        {
                                            Entries = entries13,
                                            LabelTextSize = 24,
                                        };
                                        this.todayTemp4.Chart = lineChart13;
                                        break;
                                    case "week":
                                        entries = new List<ChartEntry>();
                                        var entries2 = draw_entries(json, entries, parameter + "1", dtime);
                                        var lineChart2 = new DonutChart()
                                        {
                                            Entries = entries2,
                                            LabelTextSize = 24,
                                        };
                                        this.weekTemp1.Chart = lineChart2;

                                        entries = new List<ChartEntry>();
                                        var entries21 = draw_entries(json, entries, parameter + "2", dtime);
                                        var lineChart21 = new DonutChart()
                                        {
                                            Entries = entries21,
                                            LabelTextSize = 24,
                                        };
                                        this.weekTemp2.Chart = lineChart21;

                                        entries = new List<ChartEntry>();
                                        var entries22 = draw_entries(json, entries, parameter + "3", dtime);
                                        var lineChart22 = new DonutChart()
                                        {
                                            Entries = entries22,
                                            LabelTextSize = 24,
                                        };
                                        this.weekTemp3.Chart = lineChart22;

                                        entries = new List<ChartEntry>();
                                        var entries23 = draw_entries(json, entries, parameter + "4", dtime);
                                        var lineChart23 = new DonutChart()
                                        {
                                            Entries = entries23,
                                            LabelTextSize = 24,
                                        };
                                        this.weekTemp4.Chart = lineChart23;
                                        break;
                                    case "month":
                                        entries = new List<ChartEntry>();
                                        var entries3 = draw_entries(json, entries, parameter + "1", dtime);
                                        var lineChart3 = new DonutChart()
                                        {
                                            Entries = entries3,
                                            LabelTextSize = 24,
                                        };
                                        this.monthTemp1.Chart = lineChart3;

                                        entries = new List<ChartEntry>();
                                        var entries31 = draw_entries(json, entries, parameter + "2", dtime);
                                        var lineChart31 = new DonutChart()
                                        {
                                            Entries = entries31,
                                            LabelTextSize = 24,
                                        };
                                        this.monthTemp2.Chart = lineChart31;

                                        entries = new List<ChartEntry>();
                                        var entries32 = draw_entries(json, entries, parameter + "3", dtime);
                                        var lineChart32 = new DonutChart()
                                        {
                                            Entries = entries32,
                                            LabelTextSize = 24,
                                        };
                                        this.monthTemp3.Chart = lineChart32;

                                        entries = new List<ChartEntry>();
                                        var entries33 = draw_entries(json, entries, parameter + "4", dtime);
                                        var lineChart33 = new DonutChart()
                                        {
                                            Entries = entries33,
                                            LabelTextSize = 24,
                                        };
                                        this.monthTemp4.Chart = lineChart33;
                                        break;
                                    case "year":
                                        entries = new List<ChartEntry>();
                                        var entries4 = draw_entries(json, entries, parameter + "1", dtime);
                                        var lineChart4 = new DonutChart()
                                        {
                                            Entries = entries4,
                                            LabelTextSize = 24,
                                        };
                                        this.yearTemp1.Chart = lineChart4;

                                        entries = new List<ChartEntry>();
                                        var entries41 = draw_entries(json, entries, parameter + "2", dtime);
                                        var lineChart41 = new DonutChart()
                                        {
                                            Entries = entries41,
                                            LabelTextSize = 24,
                                        };
                                        this.yearTemp2.Chart = lineChart41;

                                        entries = new List<ChartEntry>();
                                        var entries42 = draw_entries(json, entries, parameter + "3", dtime);
                                        var lineChart42 = new DonutChart()
                                        {
                                            Entries = entries42,
                                            LabelTextSize = 24,
                                        };
                                        this.yearTemp3.Chart = lineChart42;

                                        entries = new List<ChartEntry>();
                                        var entries43 = draw_entries(json, entries, parameter + "4", dtime);
                                        var lineChart43 = new DonutChart()
                                        {
                                            Entries = entries43,
                                            LabelTextSize = 24,
                                        };
                                        this.yearTemp4.Chart = lineChart43;
                                        break;
                                }*/
                            }
                        }
                    }
                    else
                    {
                        LoadImg.IsVisible = false;
                        nodata.Text = "No Data!";
                        //沒資料時
                    }
                }
            }
            catch (OperationCanceledException ex) when (_cts.IsCancellationRequested)
            {
                Console.WriteLine(ex.Message);
            }
            _cts.Cancel();
        }

        private List<ChartEntry> draw_entries(List<SinglefieldArray> json, List<ChartEntry> entries,String str,String stime)
        {
            orderCounts.Clear();
           
            for (int i = 0; i < json.Count(); i++)
            {
                
                switch (str)
                {
                    case "Voltage":
                        orderCounts.Add(new DeviceOrdersCount { Titalname = i.ToString(), OrderCount = float.Parse(Math.Round((float.Parse(json[i].A002) * float.Parse("0.001")), 2).ToString()) });
                        break;
                    case "Current":
                        float totalvalue = 0;
                        switch (stime)
                        {
                            case "today":
                                totalvalue = float.Parse((Math.Round(float.Parse(json[i].A003) * float.Parse("0.01")*float.Parse("0.00111"), 2)).ToString());
                                break;
                            case "week":
                                totalvalue = float.Parse((Math.Round(float.Parse(json[i].A003) * float.Parse("0.01")*float.Parse("0.0000463"), 2)).ToString());
                                break;
                            case "month":
                                totalvalue = float.Parse(Math.Round(float.Parse(json[i].A003) * float.Parse("0.01")* float.Parse("0.0000463"), 2).ToString());
                                break;
                            case "year":
                                totalvalue = float.Parse(Math.Round(float.Parse(json[i].A003) * float.Parse("0.01")* float.Parse("0.00000154"), 2).ToString());
                                break;
                        }
                            orderCounts.Add(new DeviceOrdersCount { Titalname = i.ToString(), OrderCount =  totalvalue});
                        break;
                    case "Power":
                        float totalvalue2 = 0;

                        switch (stime)
                        {
                            case "today":
                                totalvalue2 = float.Parse((Math.Round(float.Parse(json[i].A002) * float.Parse(json[i].A003) * float.Parse("0.000001") * float.Parse("0.00111"), 2)).ToString());
                                break;
                            case "week":
                                totalvalue2 = float.Parse((Math.Round(float.Parse(json[i].A002) * float.Parse(json[i].A003) * float.Parse("0.000001") * float.Parse("0.0000463"), 2)).ToString());
                                break;
                            case "month":
                                totalvalue2 = float.Parse(Math.Round(float.Parse(json[i].A002) * float.Parse(json[i].A003) * float.Parse("0.000001") * float.Parse("0.0000463"), 2).ToString());
                                break;
                            case "year":
                                totalvalue2 = float.Parse(Math.Round(float.Parse(json[i].A002) * float.Parse(json[i].A003) * float.Parse("0.000001") * float.Parse("0.00000154"), 2).ToString());
                                break;
                        }
                        orderCounts.Add(new DeviceOrdersCount { Titalname = i.ToString(), OrderCount = totalvalue2 });
                        break;
                    case "Temperatures1":
                        float temp = 0;
                        if (float.Parse(json[i].A008) == 0)
                                temp = 0;
                            else
                                temp = float.Parse(Math.Round((float.Parse(json[i].A008) - 273) * float.Parse("0.01"), 2).ToString());
                        
                        orderCounts.Add(new DeviceOrdersCount { Titalname = i.ToString(), OrderCount = temp });
                        break;
                    case "Temperatures2":
                        if (float.Parse(json[i].A009) == 0)
                            temp = 0;
                        else
                            temp = float.Parse(Math.Round((float.Parse(json[i].A009) - 273) * float.Parse("0.01"), 2).ToString());
                        orderCounts.Add(new DeviceOrdersCount { Titalname = i.ToString(), OrderCount = temp });
                        break;
                    case "Temperatures3":
                        if (float.Parse(json[i].A010) == 0)
                            temp = 0;
                        else
                            temp = float.Parse(Math.Round((float.Parse(json[i].A010) - 273) * float.Parse("0.01"), 2).ToString());
                        orderCounts.Add(new DeviceOrdersCount { Titalname = i.ToString(), OrderCount = temp });
                        break;
                    case "Temperatures4":
                        if (float.Parse(json[i].A011) == 0)
                            temp = 0;
                        else
                            temp = float.Parse(Math.Round((float.Parse(json[i].A011) - 273) * float.Parse("0.01"), 2).ToString());
                        orderCounts.Add(new DeviceOrdersCount { Titalname = i.ToString(), OrderCount = temp });
                        break;
                    case "BMSSafetyStatus":
                        orderCounts.Add(new DeviceOrdersCount { Titalname = i.ToString(), OrderCount = float.Parse(json[i].A007) });
                        break; 
                    case "RelativeStateOfCharge":
                        orderCounts.Add(new DeviceOrdersCount { Titalname = i.ToString(), OrderCount = float.Parse(json[i].A006) });
                        break;
                    case "CycleCount":
                        orderCounts.Add(new DeviceOrdersCount { Titalname = i.ToString(), OrderCount = float.Parse(json[i].A005) });
                        break;
                    case "StateOfHealth":
                        orderCounts.Add(new DeviceOrdersCount { Titalname = i.ToString(), OrderCount = float.Parse(json[i].A004) });
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
        
        private void XFCharts(List<SinglefieldArray> json, String str)
        {
            List<EntryChart> e_entry = new List<EntryChart>();
            var labels = new List<string>();
            Random random = new Random();
            try
            {
                int i_img = 0;
                for (int i = 0; i < json.Count(); i++)
                {
                    float temp = 0;
                    if (i_img == 12)
                        i_img = 0;
                    switch (str)
                    {
                        case "Voltage":
                            e_entry.Add(new EntryChart(i, float.Parse(Math.Round((float.Parse(json[i].A002) * float.Parse("0.001")), 2).ToString())));
                            break;
                        case "Current":
                            float totalvalue = 0;
                            totalvalue = float.Parse((Math.Round(float.Parse(json[i].A003) * float.Parse("0.01") * float.Parse("0.00111"), 2)).ToString());
                            e_entry.Add(new EntryChart(i, totalvalue));
                            break;
                        case "Power":
                            float totalvalue2 = 0;
                            totalvalue2 = float.Parse((Math.Round(float.Parse(json[i].A002) * float.Parse(json[i].A003) * float.Parse("0.000001") * float.Parse("0.00111"), 2)).ToString());
                            e_entry.Add(new EntryChart(i, totalvalue2));
                            break;
                        case "BMSSafetyStatus":
                            e_entry.Add(new EntryChart(i, float.Parse(json[i].A007)));
                            break;
                        case "RelativeStateOfCharge":
                            e_entry.Add(new EntryChart(i, float.Parse(json[i].A006)));
                            break;
                        case "CycleCount":
                            e_entry.Add(new EntryChart(i, float.Parse(json[i].A005)));
                            break;
                        case "StateOfHealth":
                            e_entry.Add(new EntryChart(i, float.Parse(json[i].A004)));
                            break;
                        case "Temperatures1":
                            if (float.Parse(json[i].A008) == 0)
                                temp = 0;
                            else
                                temp = float.Parse(Math.Round((float.Parse(json[i].A008) - 273) * float.Parse("0.01"), 2).ToString());
                            e_entry.Add(new EntryChart(i, temp));
                            break;
                        case "Temperatures2":
                            if (float.Parse(json[i].A009) == 0)
                                temp = 0;
                            else
                                temp = float.Parse(Math.Round((float.Parse(json[i].A009) - 273) * float.Parse("0.01"), 2).ToString());
                                e_entry.Add(new EntryChart(i, temp));
                            break;
                        case "Temperatures3":
                            if (float.Parse(json[i].A010) == 0)
                                temp = 0;
                            else
                                temp = float.Parse(Math.Round((float.Parse(json[i].A010) - 273) * float.Parse("0.01"), 2).ToString());
                            e_entry.Add(new EntryChart(i, temp));
                            break;
                        case "Temperatures4":
                            if (float.Parse(json[i].A011) == 0)
                                temp = 0;
                            else
                                temp = float.Parse(Math.Round((float.Parse(json[i].A011) - 273) * float.Parse("0.01"), 2).ToString());
                            e_entry.Add(new EntryChart(i, temp));
                            break;
                    }
                    i_img++;
                    labels.Add(i.ToString());
                }
                var T1dataSetLine = new LineDataSetXF(e_entry, str)
                {
                    CircleRadius = 0,
                    CircleHoleRadius = 0f,
                    GradientColor = new GradientColor(Color.Blue,Color.Red,30),
                    DrawFilled = true,
                    FillColor = Color.FromRgba(random.Next(256), random.Next(256), random.Next(256), 1),
                    CircleColors = new List<Color>(){
                        Color.FromRgb(random.Next(256),random.Next(256),random.Next(256))
                    },
                    Colors = new List<Color>{
                        Color.FromRgb(random.Next(256),random.Next(256),random.Next(256))
                        },
                    CircleHoleColor = Color.FromRgb(random.Next(256), random.Next(256), random.Next(256)),
                    Mode = LineDataSetMode.CUBIC_HORIZONTAL
                };
                var TdataLine = new LineChartData(new List<ILineDataSetXF>() { T1dataSetLine});

                chart2XF.ChartData = TdataLine;
                chart2XF.AxisLeft.DrawGridLinesBehindData = false;
                chart2XF.AxisLeft.Enabled = true;

                chart2XF.AxisRight.DrawGridLinesBehindData = false;
                chart2XF.AxisRight.Enabled = false;
                chart2XF.XAxis.XAXISPosition = XAXISPosition.BOTTOM;
                chart2XF.XAxis.DrawGridLines = false;
                chart2XF.XAxis.TextSize = 8;
                chart2XF.XAxis.AxisValueFormatter = new TextByIndexXAxisFormatter(labels);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }
        }

        private List<RealList> Getlabel(List<SinglefieldArray> json, String str)
        {
            var lab = new List<RealList>();
            var i = 0;
            var i_img = i;
            for ( i=0; i < json.Count(); i++)
            {
                var myDate = DateTime.Now;
                float temp = 0;
                string myDateString = myDate.ToString("yyyy-MM-dd  ")+i.ToString();
                if (i == 12)
                    i_img = 0;
                switch (str)
                {
                    case "Voltage":
                        lab.Add(new RealList { AllDb = myDateString,Name = Math.Round((float.Parse(json[i].A002) * float.Parse("0.001")), 2).ToString(), Unit = "V", ImageUrl = "clock" + i_img + ".png" });
                        break;
                    case "Current":
                        float totalvalue = 0;
                        totalvalue = float.Parse((Math.Round(float.Parse(json[i].A003) * float.Parse("0.01") * float.Parse("0.00111"), 2)).ToString());
                        lab.Add(new RealList { AllDb = myDateString, Name = totalvalue.ToString(), Unit = "A", ImageUrl = "clock" + i_img + ".png" });
                        break;
                    case "Power":
                        float totalvalue2 = 0;
                        totalvalue2 = float.Parse((Math.Round(float.Parse(json[i].A002) * float.Parse(json[i].A003) * float.Parse("0.000001") * float.Parse("0.00111"), 2)).ToString());
                        lab.Add(new RealList { AllDb = myDateString, Name = totalvalue2.ToString(), Unit = "Kw", ImageUrl = "clock" + i_img + ".png" });
                        break;
                    case "BMSSafetyStatus":
                        lab.Add(new RealList { AllDb = myDateString, Name = json[i].A007, Unit = "", ImageUrl = "clock" + i_img + ".png" });
                        break;
                    case "RelativeStateOfCharge":
                        lab.Add(new RealList { AllDb = myDateString, Name = json[i].A006, Unit = "%", ImageUrl = "clock" + i_img + ".png" });
                        break;
                    case "CycleCount":
                        lab.Add(new RealList { AllDb = myDateString, Name = json[i].A004, Unit = "times", ImageUrl = "clock" + i_img + ".png" });
                        break;
                    case "StateOfHealth":
                        lab.Add(new RealList { AllDb = myDateString, Name = json[i].A005, Unit = "%", ImageUrl = "clock" + i_img + ".png" });
                        break;
                    case "Temperatures1":
                        if (float.Parse(json[i].A008) == 0)
                            temp = 0;
                        else
                            temp = float.Parse(Math.Round((float.Parse(json[i].A008) - 273) * float.Parse("0.01"), 2).ToString());
                        lab.Add(new RealList { AllDb = myDateString, Name = temp.ToString(), Unit = "C", ImageUrl = "clock" + i_img + ".png" });
                        break;
                    case "Temperatures2":
                        if (float.Parse(json[i].A009) == 0)
                            temp = 0;
                        else
                            temp = float.Parse(Math.Round((float.Parse(json[i].A009) - 273) * float.Parse("0.01"), 2).ToString());
                        lab.Add(new RealList { AllDb = myDateString, Name = temp.ToString(), Unit = "C", ImageUrl = "clock" + i_img + ".png" });
                        break;
                    case "Temperatures3":
                        if (float.Parse(json[i].A010) == 0)
                            temp = 0;
                        else
                            temp = float.Parse(Math.Round((float.Parse(json[i].A010) - 273) * float.Parse("0.01"), 2).ToString());
                        lab.Add(new RealList { AllDb = myDateString, Name = temp.ToString(), Unit = "C", ImageUrl = "clock" + i_img + ".png" });
                        break;
                    case "Temperatures4":
                        if (float.Parse(json[i].A011) == 0)
                            temp = 0;
                        else
                            temp = float.Parse(Math.Round((float.Parse(json[i].A011) - 273) * float.Parse("0.01"), 2).ToString());
                        lab.Add(new RealList { AllDb = myDateString, Name = temp.ToString(), Unit = "C", ImageUrl = "clock" + i_img + ".png" });
                        break;
                }
                i_img++;
            }
            return lab;
        }

        public void LoadBarChart(List<SinglefieldArray> json)
        {
            try
            {
                var entrieLine = new List<EntryChart>();
                var entrieLine2 = new List<EntryChart>();
                var entrieLine3 = new List<EntryChart>();
                var entrieLine4 = new List<EntryChart>();
                var entrieLine5 = new List<EntryChart>();
                var entrieLine6 = new List<EntryChart>();
                var labels = new List<string>();
                var tempframe = new List<tempList>();

                Random random = new Random();

                var i_img = 0;
                float temp = 0;
                for (int i = 0; i < json.Count(); i++)
                {
                    entrieLine.Add(new EntryChart(i, float.Parse(Math.Round(float.Parse(json[i].A008) * float.Parse("0.01"), 2).ToString() ) ));
                    entrieLine2.Add(new EntryChart(i, float.Parse(Math.Round(float.Parse(json[i].A009) * float.Parse("0.01"), 2).ToString()) ));
                    entrieLine3.Add(new EntryChart(i, float.Parse(Math.Round(float.Parse(json[i].A010) * float.Parse("0.01"), 2).ToString()) ));
                    entrieLine4.Add(new EntryChart(i, float.Parse(Math.Round(float.Parse(json[i].A011) * float.Parse("0.01"), 2).ToString()) ));
                    entrieLine5.Add(new EntryChart(i, float.Parse(Math.Round(float.Parse(json[i].A012) * float.Parse("0.01"), 2).ToString())));
                    entrieLine6.Add(new EntryChart(i, float.Parse(Math.Round(float.Parse(json[i].A013) * float.Parse("0.01"), 2).ToString())));
                    labels.Add(i.ToString());
                    if (float.Parse(json[i].A008) == 0)
                        temp = 0;
                    else
                        temp = float.Parse(Math.Round((float.Parse(json[i].A008) - 273) * float.Parse("0.01"), 2).ToString());

                    var temp1 = temp;
                    if (float.Parse(json[i].A009) == 0)
                        temp = 0;
                    else
                        temp = float.Parse(Math.Round((float.Parse(json[i].A009) - 273) * float.Parse("0.01"), 2).ToString());
                    var temp2 = temp;
                    if (float.Parse(json[i].A010) == 0)
                        temp = 0;
                    else
                        temp = float.Parse(Math.Round((float.Parse(json[i].A010) - 273) * float.Parse("0.01"), 2).ToString());
                    var temp3 = temp;
                    if (float.Parse(json[i].A011) == 0)
                        temp = 0;
                    else
                        temp = float.Parse(Math.Round((float.Parse(json[i].A011) - 273) * float.Parse("0.01"), 2).ToString());
                    var temp4 = temp;
                    if (float.Parse(json[i].A012) == 0)
                        temp = 0;
                    else
                        temp = float.Parse(Math.Round((float.Parse(json[i].A012) - 273) * float.Parse("0.01"), 2).ToString());
                    var temp5 = temp;
                    if (float.Parse(json[i].A013) == 0)
                        temp = 0;
                    else
                        temp = float.Parse(Math.Round((float.Parse(json[i].A012) - 273) * float.Parse("0.01"), 2).ToString());
                    var temp6 = temp;
                    if (i_img == 12)
                        i_img = 0;
                    tempframe.Add(new tempList{ ImageUrl = "clock" + i_img + ".png", temp1 ="Temperature 1 (" +temp1+ "°C)", temp2 = "Temperature 2 (" + temp2 + "°C)", temp3 = "Temperature 3 (" + temp3 + "°C)", temp4 = "Temperature 4 (" + temp4 + "°C)", temp5 = "Temperature 5 (" + temp4 + "°C)", temp6 = "Temperature 6 (" + temp4 + "°C)" });
                    i_img++;
                }
                
                templist.ItemsSource = tempframe;   //溫度 資料存入frame裡

                var T1dataSetLine = new LineDataSetXF(entrieLine, "Temp.1")
                {
                    CircleRadius = 3,
                    CircleHoleRadius = 0f,
                    CircleColors = new List<Color>(){
                    Color.Black
                },
                    Colors = new List<Color>{
                    Color.Black
                    },
                    CircleHoleColor = Color.LightGreen,
                    Mode = LineDataSetMode.CUBIC_BEZIER
                };

                var T2dataSetLine = new LineDataSetXF(entrieLine2, "Temp.2")
                {
                    CircleRadius = 3,
                    CircleHoleRadius = 0f,
                    CircleColors = new List<Color>(){
                    Color.Green
                },
                    Colors = new List<Color>{
                    Color.Green
                    },
                    CircleHoleColor = Color.LightGreen,
                    Mode = LineDataSetMode.CUBIC_BEZIER
                };

                var T3dataSetLine = new LineDataSetXF(entrieLine3, "Temp.3")
                {
                    CircleRadius = 3,
                    CircleHoleRadius = 0f,
                    CircleColors = new List<Color>(){
                    Color.Blue
                },
                    Colors = new List<Color>{
                    Color.Blue
                    },
                    CircleHoleColor = Color.Yellow,
                    Mode = LineDataSetMode.CUBIC_BEZIER
                };

                var T4dataSetLine = new LineDataSetXF(entrieLine4, "Temp.4")
                {
                    CircleRadius = 3,
                    CircleHoleRadius = 0f,
                    CircleColors = new List<Color>(){
                    Color.Red
                },
                    Colors = new List<Color>{
                    Color.Red
                    },
                    CircleHoleColor = Color.LightGreen,
                    Mode = LineDataSetMode.CUBIC_BEZIER
                };
                var T5dataSetLine = new LineDataSetXF(entrieLine5, "Temp.5")
                {
                    CircleRadius = 3,
                    CircleHoleRadius = 0f,
                    CircleColors = new List<Color>(){
                    Color.Purple
                },
                    Colors = new List<Color>{
                    Color.Purple
                    },
                    CircleHoleColor = Color.Yellow,
                    Mode = LineDataSetMode.CUBIC_BEZIER
                };

                var T6dataSetLine = new LineDataSetXF(entrieLine6, "Temp.6")
                {
                    CircleRadius = 3,
                    CircleHoleRadius = 0f,
                    CircleColors = new List<Color>(){
                    Color.Orange
                },
                    Colors = new List<Color>{
                    Color.Orange
                    },
                    CircleHoleColor = Color.LightGreen,
                    Mode = LineDataSetMode.CUBIC_BEZIER
                };

                var TdataLine = new LineChartData(new List<ILineDataSetXF>() { T1dataSetLine, T2dataSetLine, T3dataSetLine,T4dataSetLine, T5dataSetLine, T6dataSetLine });

                
                chartXF.ChartData = TdataLine;
                chartXF.AxisLeft.DrawGridLinesBehindData = false;
                chartXF.AxisLeft.Enabled = true;

                chartXF.AxisRight.DrawGridLinesBehindData = false;
                chartXF.AxisRight.Enabled = false;

                chartXF.XAxis.XAXISPosition = XAXISPosition.BOTTOM;
                chartXF.XAxis.DrawGridLines = false;
                chartXF.XAxis.TextSize = 8;
                chartXF.XAxis.AxisValueFormatter = new TextByIndexXAxisFormatter(labels);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }
        }

        
    }

}