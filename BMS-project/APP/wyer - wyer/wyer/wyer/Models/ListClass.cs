using System;
using System.Collections.Generic;
using System.Text;

namespace wyer.Models
{
    class ListClass
    {
    }

    class ListViewItem
    {
        public String Title { get; set; }
        public string Dtime { get; set; }
        public string Description { get; set; }
    }

    public class DeviceOrdersCount
    {
        public string Titalname { get; set; }
        public float OrderCount { get; set; }
    }

    class AlertData
    {
        public string dev_mac { get; set; }
        public string dev_name { get; set; }
        public string dat_time { get; set; }
        public string field { get; set; }
        public string stat_name { get; set; }
        public string s_value { get; set; }
        
    }

    public class DeviceBsData_2
    {
        public string addr { get; set; }
        public string temp01 { get; set; }
        public string temp02 { get; set; }
        public string temp03 { get; set; }
        public string temp04 { get; set; }
        public string temp05 { get; set; }
        public string temp06 { get; set; }
        public string temp07 { get; set; }
        public string temp08 { get; set; }
        public string temp09 { get; set; }
        public string temp10 { get; set; }
    }
}
