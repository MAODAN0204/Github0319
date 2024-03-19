using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms.PlatformConfiguration;

namespace wyer.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string dev_mac { get; set; }
        public string Description { get; set; }
        public string Remark { get; set; }
    }

    public static class DeviceSelect
    {
        public static String mac { get; set; }
        public static String name { get; set; }
        public static string batteryname { get; set; }
    }

    public class UserInfos
    {
        public string Id { get; set; }
        public string usname { get; set; }
        public string nkname { get; set; }
    }

    public class UserData
    {
        public string Id { get; set; }
        public string usname { get; set; }
        public string pswd { get; set; }
        public string nkname { get; set; }
        public string tel { get; set; }
        public string mobile { get; set; }
        public string e_mail { get; set; }
    }

    public class TempData
    {
        public static string T_id;
        public static string T_name;
        public static string T_nick;
        public static string T_mac;
        public static string T_battery;
    }

    public class DevDataGet
    {
        public string dev_mac { get; set; }
        public string dev_name { get; set; }
        public string addr { get; set; }
        public string i_type { get; set; }
        public string Remark { get; set; }
    }

    public class DevDataDB
    {
        public string dev_mac { get; set; }
        public string dev_name { get; set; }
        public string Remark { get; set; }
    }

    public class DevArray
    {
        public string Dat_time { get; set; }
        public  string A001 { get; set; }
        public  string A002 { get; set; }
        public  string A003 { get; set; }
        public  string A004 { get; set; }
        public  string A005 { get; set; }
        public  string A006 { get; set; }
        public  string A007 { get; set; }
        public  string A008 { get; set; }
        public  string A009 { get; set; }
        public  string A010 { get; set; }
        
    }

    public class Dev2Array
    {
        public string Dat_time { get; set; }
        public string A001 { get; set; }
        public string A002 { get; set; }
        public string A003 { get; set; }
        public string A004 { get; set; }
        public string A005 { get; set; }
        public string A006 { get; set; }
        public string A007 { get; set; }
        public string A008 { get; set; }
        public string A009 { get; set; }
        public string A010 { get; set; }
        public string A011 { get; set; }
        public string A012 { get; set; }
        public string A013 { get; set; }
    }

    public class SinglefieldArray
    {
        public string dat_time { get; set; }
        public string A001 { get; set; }
        public string A002 { get; set; }
        public string A003 { get; set; }
        public string A004 { get; set; }
        public string A005 { get; set; }
        public string A006 { get; set; }
        public string A007 { get; set; }
        public string A008 { get; set; }
        public string A009 { get; set; }
        public string A010 { get; set; }
        public string A011 { get; set; }
        public string A012 { get; set; }
        public string A013 { get; set; }
    }
    public class RealList
    {
        public string AllDb { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public string ImageUrl { get; set; }
    }
    public class tempList
    {
        public string temp1 { get; set; }
        public string temp2 { get; set; }
        public string temp3 { get; set; }
        public string temp4 { get; set; }
        public string temp5 { get; set; }
        public string temp6 { get; set; }
        public string ImageUrl { get; set; }
    }
}