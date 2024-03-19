using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Timers;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace SocketReceive
{
    class Program
    {
        static Socket SocketListener;                   //建立一個監聽的socket,以取得外傳進來的資料
        private static System.Timers.Timer aTimer;      //計時器
        static string T_year = "";

        static void Main(string[] args)
        {
            ServerEnd(6600, 10000);                         //1萬個應用程序可以連線到此
            Thread th = new Thread(ServerCommunity);
            th.Start(SocketListener);
            SetTimer();
        }

        private static void SetTimer()
        {
            // Create a timer with a 60 second interval.
            aTimer = new System.Timers.Timer(1000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            var stime = DateTime.Now.ToLongTimeString();    //檢查MAC連線狀態
            var sec = stime.Substring(stime.Length - 2, 2);
            if (sec == "00")
            {
                 CheckDisConnection();
            }
        }

        private static void ServerEnd(int port, int allowNum)
        {   //server端設定內容

            /*==============================
             AddressFamily.InterNetwork表示利用IP4協議
             SocketType.Stream 因為我們要使用TCP協議，需使用流式的Socket
             ProtocolType.Tcp 選用TCP協議 
            =================================*/
            SocketListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress myip = IPAddress.Any;             //指定伺服器IP, IPAddress.Any是取得本機的IP
            int Listen_port = port;                     //設定傳輸端口

            IPEndPoint point = new IPEndPoint(myip, Listen_port); //將IP和port帶到socketListener中
            SocketListener.Bind(point);                 //將point綁定給socketListener
            //ShowMsg("Listening...");                    //開始文字
            SocketListener.Listen(allowNum);            //設定有幾個程序可以連線

        }

        private static void ServerCommunity(object obListener)  //開始傳輸
        {

            Socket temp = (Socket)obListener;           //Server用來傳送資料給客戶的 Socket

            while (true)
            {
                Socket socketSender = temp.Accept();    //用Accept接收監聽用 Socket 的資料

                //ShowMsg(("Client IP：" + socketSender.RemoteEndPoint.ToString()) + " Connect Success!");
                Thread ReceiveMsg = new Thread(ReceiveClient);  //New一個接收執行緒
                ReceiveMsg.IsBackground = true;         //接收程序在背景執行
                ReceiveMsg.Start(socketSender);         //讀取客戶端訊息
            }
        }

        private static void ReceiveClient(object socketSender)  //接受客戶端文字, 直到客戶端離開下(斷)線
        {
            Socket GsocketSender = socketSender as Socket;
            try
            {
                if (GsocketSender != null && GsocketSender.Connected)
                {
                    while (true)
                    {
                        if (GsocketSender.Poll(-1, SelectMode.SelectRead))
                        {
                            byte[] buffer = new byte[2048];                 //創立一個數組來儲存客戶端所回傳的訊息
                            int rece = GsocketSender.Receive(buffer);       //讀取字節數

                            //檢查是否網路斷線
                            if (rece == 0)                                    //如果客戶端離開所得到的字節數會等於0，跳出此循環
                            {
                                //ShowMsg(string.Format("Client： {0} + 下線了", GsocketSender.RemoteEndPoint.ToString()));
                                break;

                            }
                            string clientMsg = Encoding.UTF8.GetString(buffer, 0, rece);//第一個引數代表要讀取的byte[], 第二個引數代表從左邊數來第幾個字開始讀取, 每次讀取的字節數
                            ShowMsg(string.Format("{0}", clientMsg));

                            Split_type(clientMsg);
                        }
                    }
                }
            }
            catch (System.Net.Sockets.SocketException)
            {
                Console.WriteLine("");
            }
        }

        private static void ShowMsg(string str)
        {    //文字輸出
            Console.WriteLine(str);
        }

        private static void Split_type(string str)
        {
            string[] para = str.Split('*');

            for (int i=0; i < para.Length-1; i++)
            {
                string s_str = para[i].Replace("/r", "").Replace("/n","");
                string[] stray = s_str.Split(',');

                save_orgdata(stray,str);

                if (stray.Length > 11 && stray[11].Length > 0)
                {
                    switch (stray[2].ToString())
                    {
                        case "1":
                            CheckData_type1(stray);
                            break;
                        case "2":
                            CheckData_type2(stray);
                            break;
                    }
                }
            }
        }

        private static void save_orgdata(string[] str,string nosplit)
        {
            string[] stray = str;
            T_year = DateTime.Now.Year.ToString();  //今年度
            string connStr = "";//舊寫法2023
            string sql = "";
            if(T_year=="2023")
                connStr= "server=127.0.0.1;user=sa;database=esp;port=3306;password=Ne16450558";  //2024收的資料會歸到年份資料庫中
            else
                connStr = "server=127.0.0.1;user=sa;database= a" + T_year + ";port=3306;password=Ne16450558";  //2024收的資料會歸到年份資料庫中

            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            try
            {
                conn.Open();
                //原始資料寫入資料庫
                if (stray.Length > 11 && stray[11].Length > 0)
                {
                    if (T_year == "2023")
                        sql = "INSERT INTO org_data (Dat_time,A001,A002,A003,A004,A005,A006,A007,A008,A009,A010,B001,B002) VALUES (now(),'"; //2023年資料, 以下是2024改成一年一個檔
                    else
                        sql = "INSERT INTO a" + T_year + ".org_data (Dat_time,A001,A002,A003,A004,A005,A006,A007,A008,A009,A010,B001,B002) VALUES (now(),'";

                    sql=sql+stray[1].ToString() + "','" + stray[2].ToString() + "','" + stray[3].ToString() + "','" + stray[4].ToString() +
                    "','" + stray[5].ToString() + "','" + stray[6].ToString() + "','" + stray[7].ToString() + "','" + stray[8].ToString() +
                    "','" + stray[9].ToString() + "','" + stray[10].ToString() + "','" + stray[11].ToString() + "','" + stray[12].ToString() + "')";
                }
                else  //如果資料有錯就直接將值填入B002
                {
                    //sql = "INSERT INTO org_data (Dat_time,A001,A002,A003,B001,B002) VALUES (now(),'-','-','-','-','"+nosplit + "')"; //2023年資料
                    if (T_year == "2023")
                        sql = "INSERT INTO esp.org_data (Dat_time,A001,A002,A003,B001,B002) VALUES (now(),'-','','-','-','" + nosplit + "')";
                    else
                        sql = "INSERT INTO a"+T_year+".org_data (Dat_time,A001,A002,A003,B001,B002) VALUES (now(),'-','','-','-','" + nosplit + "')";
                }
                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void CheckData_type1(string[] str)
        {
            string connStr = "server=127.0.0.1;user=sa;database=esp;port=3306;password=Ne16450558";   //2023 舊寫法
            string sql = "";
            string[] stray = str;
            T_year = DateTime.Now.Year.ToString();
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            
            string[] spec_str = new string[1];  //存放type1各參數的位元數

            int str_leng=0;
            sql = "select spe_array,int_leng from esp.device_type where dev_type=" + stray[2].ToString() +" and sub_type='"+stray[11].ToString()+"'";
            try
            {
                cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    spec_str = rd[0].ToString().Split(',');
                    str_leng = Int32.Parse(rd[1].ToString());
                }   //取得type的格式的長度array
                rd.Close();
                cmd.Dispose();

               /* for (int i = 0; i < spec_str.Length - 1; i++)
                {   //計算取值的總長度
                    i_count += Int32.Parse(spec_str[i].ToString());
                }*/

                string recive_str = stray[12].ToString().Replace("\r", "").Replace("/n", "").Replace("(","");
                var s=stray[11].ToString();
                var count_str = recive_str.Length;
                if (count_str >= str_leng) //如字串內容長度有錯就不往下執行
                {
                    var array_str = new string[spec_str.Length]; //存放從socket傳來的第12組的內容

                    /*if (stray[12].ToString().Length < i_count)
                    {   //異常回傳
                        //array_str[0] = recive_str.Substring(0, Int32.Parse(spec_str.Length));
                    }
                    else   //正常取值
                    {*/
                        int temp = 0;
                        for (int i = 0; i < spec_str.Length ; i++)
                        {
                            array_str[i] = recive_str.Substring(temp, Int32.Parse(spec_str[i].ToString()));
                            /*if(s== "QFC")
                            {
                                array_str[i] = recive_str.Substring((recive_str.Length-Int32.Parse(spec_str[i].ToString())), Int32.Parse(spec_str[i].ToString())); ;
                             }*/
                            temp = temp + Int32.Parse(spec_str[i].ToString()) + 1;
                        }
                    //}
                    if (s == "QFC")
                    {
                        var r = 0;
                    }

                    sql = Getsqlstr(array_str, stray[1].ToString(), stray[11].ToString(), '1');
                    cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    if(s=="RT")
                    {
                        chekmacexist(array_str, stray[1].ToString(), stray[11].ToString(), '1');
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            conn.Close();
            conn.Dispose();
        }

        private static void CheckData_type2(string[] str)
        {
            //拆字串
            string[] stray = str;
            

            if (stray.Length > 11 && stray[12].Length >10 && stray[11].Length > 0) //陣列要大於11, 因為資料在第12. 若沒資料就不存
            {
                string connStr = "server=localhost;user=sa;database=esp;port=3306;password=Ne16450558"; //2023 舊寫法
                string sql = "";
                MySqlConnection conn = new MySqlConnection(connStr);
                MySqlCommand cmd;
                try 
                { 
                    conn.Open();
                    //檢查總長度是否正確
                    bool B_save=CheckDataSaveOrNot(2, stray[12].Length);

                    if (B_save == true)
                    {
                        //將機器回應值 array[12] 依不同類別放入array 
                        string[] spec_str = new string[1];
                        int totallength = 0;
                        sql = "select spe_array,tol_length from esp.dev_type where dev_type=" + stray[2].ToString();
                        cmd = new MySqlCommand(sql, conn);
                        MySqlDataReader rd = cmd.ExecuteReader();
                        if (rd.Read())
                        {
                            spec_str = rd[0].ToString().Split(',');
                            totallength = Int32.Parse(rd[1].ToString());
                        }   //取得type的格式
                        rd.Close();
                        cmd.Dispose();

                        int temp_i = 0;     //存最substring第二個值(位數)
                        string[] trans_word = new string[spec_str.Length];
                        for (int i = 0; i < spec_str.Length; i++)
                        {
                            int s, e;
                            s = Int16.Parse(spec_str[i]) * 2 * i;    //substring第一個值(起始位置)
                            e = Int16.Parse(spec_str[i]) * 2;

                            string save_str = stray[12].ToString().Substring(temp_i, e);
                            if (Int32.Parse(spec_str[i]) > 5 || i==75 )
                            {
                                trans_word[i] = GetBytes(save_str).Replace("\0", "");//hax TO ASCII
                            }
                            else
                            {
                                switch (i)
                                {
                                    case 4:
                                        trans_word[i] = Int16.Parse(save_str, System.Globalization.NumberStyles.HexNumber).ToString(); //hax TO dec(有正負)
                                        break;
                                    case 35:
                                        trans_word[i] = Int16.Parse(save_str, System.Globalization.NumberStyles.HexNumber).ToString(); //hax TO dec(有正負)
                                        break;
                                    case 36:
                                        trans_word[i] = Int16.Parse(save_str, System.Globalization.NumberStyles.HexNumber).ToString(); //hax TO dec(有正負)
                                        break;
                                    case 37:
                                        trans_word[i] = Int16.Parse(save_str, System.Globalization.NumberStyles.HexNumber).ToString(); //hax TO dec(有正負)
                                        break;
                                    case 38:
                                        trans_word[i] = Int16.Parse(save_str, System.Globalization.NumberStyles.HexNumber).ToString(); //hax TO dec(有正負)
                                        break;
                                    case 39:
                                        trans_word[i] = Int16.Parse(save_str, System.Globalization.NumberStyles.HexNumber).ToString(); //hax TO dec(有正負)
                                        break;
                                    case 40:
                                        trans_word[i] = Int16.Parse(save_str, System.Globalization.NumberStyles.HexNumber).ToString(); //hax TO dec(有正負)
                                        break;
                                    default:
                                        trans_word[i] = Int32.Parse(save_str, System.Globalization.NumberStyles.HexNumber).ToString(); //hax TO dec(只有正值)
                                        break;
                                }
                                if (i == 11 )    //檢查BMS safety status 寫入資料庫
                                {
                                    string err_msg = "";
                                    switch (trans_word[11].ToString())
                                    {
                                        case "1":
                                            err_msg = "Cell UnderVoltage";
                                            break;
                                        case "2":
                                            err_msg = "Cell OverrVoltage";
                                            break;
                                        case "4":
                                            err_msg = "Overcurrent During Charge";
                                            break;
                                        case "8":
                                            err_msg = "Overcurrent During Discharge";
                                            break;
                                        case "16":
                                            err_msg = "Overload During Discharge";
                                            break;
                                        case "32":
                                            err_msg = "Overload During Discharge Latch";
                                            break;
                                        case "64":
                                            err_msg = "Short Circult During DisCharge";
                                            break;
                                        case "128":
                                            err_msg = "Short Circult During Discharge Latch";
                                            break;
                                        case "256":
                                            err_msg = "Overtemperature During Charge";
                                            break;
                                        case "512":
                                            err_msg = "Overtemperature During Discharge";
                                            break;
                                        case "1024":
                                            err_msg = "Undertemperature During Charge";
                                            break;
                                        case "2048":
                                            err_msg = "Undertemperature During Discharge";
                                            break;
                                        case "0":
                                            err_msg = "BMS-Normal";
                                            break;
                                    }
                                    CheckConnection(stray[1].ToString(), stray[11].ToString());  //先去確認是否上線,若斷線後上線就寫入, 若上線己寫入就不重覆
                                    CheckBMSsafeStatus(err_msg, stray[1].ToString(), stray[11].ToString()); //檢查是否有重覆寫, 若有不寫入, 若沒就寫入
                                }
                            }
                            temp_i = temp_i + e;
                        }

                        if(stray[12].Length- (totallength*2)>0) //如果丟過來的資料大於規畫中的欄位, 多的資料全存在規劃中的欄位的下個欄位中
                        {
                            string last = stray[12].Substring((totallength * 2) - 1, stray[12].Length - (totallength * 2));
                            Array.Resize(ref trans_word, spec_str.Length + 1);
                            trans_word[spec_str.Length]=last;
                        }

                        //存入資料庫
                        sql = Getsqlstr(trans_word, stray[1].ToString(), stray[11].ToString(),'2');
                        cmd = new MySqlCommand(sql, conn);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();

                        //檢查是否有這筆device 並存入到dev_data裡
                        chekmacexist(trans_word,stray[1].ToString(), stray[11].ToString(), '2');
                    }
                    conn.Close();
                    conn.Dispose();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                
                
            }

        }
        
        private static void chekmacexist(string[] array_str, string mac, string s_name, char c_type)
        {
           string connStr = "server=localhost;user=sa;database=esp;port=3306;password=Ne16450558";
            string sql = "";
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            try
            {
                conn.Open();
                //原始資料寫入資料庫  只有 type 2 或type1 && RT才檢查
                sql = "select count(*) from esp.dev_data where dev_mac='" + mac + "' and addr='" + s_name + "'";
                cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rd = cmd.ExecuteReader();
                int has_mac = 0;
                if (rd.Read())
                {
                    has_mac = Int32.Parse(rd[0].ToString());

                }   //取得type的格式
                rd.Close();
                cmd.Dispose();
                if (has_mac == 0)  //新增一筆device 基本資料
                {
                    if (c_type == '2')
                    {
                        sql = "INSERT INTO esp.dev_data (i_type,dev_mac,addr,temp01,temp02,temp03,temp04,temp05) VALUES (" + c_type + ",'" + mac + "','" + s_name + "','"
                            + array_str[67].ToString() + array_str[68].ToString() + array_str[69].ToString() + "','" + array_str[70].ToString() + array_str[71].ToString() +
                            array_str[72].ToString() + "','" + array_str[73].ToString() + "','" + array_str[76].ToString() + array_str[77].ToString() + "','" + array_str[78].ToString() + "')";
                    }
                    if (c_type == '1')
                    {
                        sql = "INSERT INTO esp,dev_data (i_type,dev_mac,addr,temp01,temp02,temp03,temp04,temp05,temp06,temp07,temp08,temp09,temp10) VALUES (" + c_type + ",'" + mac + "','" + s_name + "','"
                            + array_str[1].ToString() +"','"+ array_str[2].ToString() +"','"+ array_str[10].ToString() + "','" + array_str[0].ToString() +"','"+ array_str[3].ToString() +"','"
                            +array_str[4].ToString() + "','" + array_str[5].ToString() + "','" + array_str[6].ToString() +"','"+ array_str[7].ToString() + "','" + array_str[8].ToString() + "')";
                    }
                    cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                else
                {       //修改Device 基本資料的版本和版本日期
                    if (c_type == '2')
                    {
                        sql = "SET SQL_SAFE_UPDATES=0; UPDATE esp.dev_data SET temp04='" + array_str[76].ToString() + array_str[77].ToString() + "',temp05='"
                                + array_str[78].ToString() + "',dev_mac='" + mac + "' where  addr='" + s_name + "' and dev_mac='"+ mac + "';";
                    }
                    /*if (c_type == '1')
                    {
                        sql = "SET SQL_SAFE_UPDATES=0; UPDATE dev_data SET bms_ver='" + array_str[76].ToString() + array_str[77].ToString() + "',bms_date='"
                                + array_str[78].ToString() + "' where dev_mac='" + mac + "' and addr='" + s_name + "'";
                    }*/
                }
                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
       
        static string GetBytes(string str)
        {
            string Result = "";
            string temp;
            for(int i=0 ;i< str.Length;i+=2)
            {
                temp = str.Substring(i, 2);
                Result += Convert.ToChar(Convert.ToInt32(temp, 16));
            }
            return Result;
        }

        static bool CheckDataSaveOrNot(int type, int count)
        {
            //檢查傳來的總長度, 並計算是否合乎長度, 若否就不存檔

            /*int check_s = Int32.Parse(str, System.Globalization.NumberStyles.HexNumber)+5; //前三欄各為1byte和後面的CRC長度共5碼
            if ((check_s*2) != count) //str是substr(stay[11],4,2)資料傳來的長度
                return false;
            else
                return true;*/ //舊寫法
            //新寫法, 與dev_type比對
            string connStr = "server=localhost;user=sa;database=esp;port=3306;password=Ne16450558";
            string sql = "";
            int spec_length = 0;
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            try
            {
                conn.Open();
                sql = "select tol_length from esp.dev_type where dev_type=" + type;
                cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    spec_length = rd.GetInt32("tol_length")*2;

                }   //取得type的格式
                rd.Close();
                cmd.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if (spec_length <= count)
                return true;
            else
                return false;
        }

        static string Getsqlstr(string[] array_str,string mac,string s_name,char c_type)
        {
            string connStr = "";
            if (T_year == "2023")
                connStr = "server=127.0.0.1;user=sa;database=esp;port=3306;password=Ne16450558";  //2024收的資料會歸到年份資料庫中
            else
                connStr = "server=127.0.0.1;user=sa;database= a" + T_year + ";port=3306;password=Ne16450558";  //2024收的資料會歸到年份資料庫中

            int i = 0;
            string sql = "";
            T_year = DateTime.Now.Year.ToString();
            if (array_str.Length > 1)
            {   //正常回傳值為陣列
                string str1, str2;
                str1 = "dev_mac,Dat_time,i_type,h_today,A001";
                str2 = "'" + mac + "',now(),'" + c_type + "',Date_format(now(),'%Y-%m-%d %k'),'" + s_name + "'";

                for (i = 2; i < array_str.Length + 2; i++)
                {
                    switch (i.ToString().Length)
                    {
                        case 1:
                            str1 += "," + "A00" + (i); //從A002開始存
                            break;
                        case 2:
                            str1 += "," + "A0" + i;
                            break;
                        default:
                            str1 += "," + "A" + i;
                            break;
                    }
                    str2 += ",'" + array_str[i - 2].ToString() + "'";
                }
                if(T_year=="2023")
                    sql=  "INSERT INTO esp.rec_data (" + str1 + ") VALUES (" + str2 + ")";
                else
                    sql = "INSERT INTO a" + T_year + ".rec_" + s_name + " (" + str1 + ") VALUES (" + str2 + ")";
            }
            else   //傳回異常值時, 只有一個字串
            {
                if(T_year=="2023")
                    sql=  "INSERT INTO esp.rec_data (dev_mac,Dat_time,i_type,h_today,A001,A002) VALUES ('" + mac + "',now(),'"+ c_type + "',Date_format(now(),'%Y-%m-%d %k'),'" + s_name + "','" + array_str[0] + "')";
                else
                    sql = "INSERT INTO a"+T_year+".rec_" + s_name + " (dev_mac,Dat_time,i_type,h_today,A001,A002) VALUES ('" + mac + "',now(),'" + c_type + "',Date_format(now(),'%Y-%m-%d %k'),'" + s_name + "','" + array_str[0] + "')";
            }
            return sql;
        }

        private static void CheckConnection(string mac,string addr)
        {
            string sql = "select case when s_value='OFFLINE' then 1 else 0 end checksum ";
                sql+=" from (select s_value from esp.alert_msg where dev_mac = '"+mac+"' and temp01 = '"+addr+"' order by i_index desc limit 1)a1";
            T_year = DateTime.Now.Year.ToString();
            string connStr = "server=localhost;user=sa;database=esp;port=3306;password=Ne16450558";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                MySqlCommand cmd;
                cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rd = cmd.ExecuteReader();

                bool checksum = false;
                while (rd.Read())
                {
                    if (rd.GetString("checksum") == "1")  //上線
                    {
                        /*如果上線時, alert msg 的table的 msg是offline 表示己上線中, 就不儲存 */
                        checksum = true;
                    }
                    
                }   
                rd.Close();
                cmd.Dispose();

                //儲存
                if (checksum == true)
                {
                    sql = "Insert Into esp.alert_msg (dev_mac,dat_time,s_value,temp01) VALUES ('"+mac+"',now(),'ONLINE','"+addr+"')";
                    cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                conn.Close();
                conn.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            
        }

        private static void CheckDisConnection()
        {
            string sql = "select dev_data.addr,dev_mac,case when a.s_value='OFFLINE' then 0 else 1 end checksum from esp.dev_data";
            sql = sql + " left join(select s_value, temp01 from esp.alert_msg where i_index in (select max(i_index) from esp.alert_msg group by temp01) )as a";
            sql = sql + " on dev_data.addr = a.temp01";

            T_year = DateTime.Now.Year.ToString();
            string connStr  = "server=localhost;user=sa;database=esp;port=3306;password=Ne16450558";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                MySqlCommand cmd;
                cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rd = cmd.ExecuteReader();

                List<String> mac = new List<String>();
                List<String> addr = new List<String>();

                while (rd.Read())
                {
                    if (rd.GetString("checksum") == "1")  //狀態不為offline的電池
                    {
                        /*如果已斷線 就不儲存 */
                        mac.Add(rd.GetString("dev_mac"));
                        addr.Add(rd.GetString("addr"));
                    }
                }
                rd.Close();
                cmd.Dispose();

                //儲存多筆資料
                string str_value = "";
                for (int m = 0; m < mac.Count; m++)
                {
                    if(T_year=="2023")
                        sql = "select dat_time from esp.rec_data where a001='"+ addr[m] +"' order by i_index desc limit 1";
                    else
                        sql = "select dat_time from a" + T_year + ".rec_" + addr[m] + " where a001='" + addr[m] + "' order by i_index desc limit 1";
                    String checktime = "";

                    cmd = new MySqlCommand(sql, conn);
                    rd = cmd.ExecuteReader();
                    
                    if(rd.Read())
                    {
                        checktime = rd.GetDateTime("dat_time").ToString("yyy/MM/d hh:mm:ss");
                    }
                    if (checktime != "")
                    {
                        System.TimeSpan R = (DateTime.Now) - (DateTime.Parse(checktime));
                        //比對完, 時間差10秒以上的就存
                        if (R.TotalSeconds > 10)
                            str_value += "('" + mac[m] + "', now(), 'OFFLINE', '" + addr[m] + "'),";
                    }
                    rd.Close();
                    cmd.Dispose();
                    
                }
                //儲存
                if (str_value.Length > 0)
                {
                    sql = "Insert Into esp.alert_msg (dev_mac,dat_time,s_value,temp01) VALUES " + str_value.Substring(0, str_value.Length - 1);
                    cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                conn.Close();
                conn.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }

        private static void CheckBMSsafeStatus(String str,string mac,string addr)
        {
            string sql = "select case when s_value='" + str+"' then 0 else 1 end checksum ";
                    sql += " from (select s_value from esp.alert_msg where dev_mac = '" + mac + "' and temp01 = '" + addr + "' and s_value not in ('ONLINE', 'OFFLINE')";
                    sql +=" order by i_index desc limit 1)a1";

            string connStr = "server=localhost;user=sa;database=esp;port=3306;password=Ne16450558";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                MySqlCommand cmd;
                cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rd = cmd.ExecuteReader();

                bool checksum = false;

                while (rd.Read())
                {
                    if (rd.GetString("checksum") == "1")  //要存
                    {
                        /*如果斷線時, alert msg 的table的offline 時間 大於 online 表示還在斷線中, 就不儲存 */
                        //記入資料庫
                        checksum = true;
                    }
                }
                rd.Close();
                cmd.Dispose();

                //儲存
                if (checksum == true)
                {
                    sql = "Insert Into esp.alert_msg (dev_mac,dat_time,s_value,temp01) VALUES ('" + mac + "',now(),'" + str + "','" + addr + "')";
                    cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                conn.Close();
                conn.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
