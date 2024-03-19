using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
//using System.Timers;
using MySql.Data.MySqlClient;

namespace DailySaveToSql
{
    class Program
    {
        public static void Main()
        {
            var autoEvent = new AutoResetEvent(false);

            var statusChecker = new StatusChecker();

            // Create a timer that invokes CheckStatus after one second, 
            Console.WriteLine("{0:h:mm:ss.fff} Creating timer.\n",
                              DateTime.Now);
            var stateTimer = new Timer(statusChecker.CheckStatus,
                                       autoEvent, 1000, 1000);
            //
            // When autoEvent signals the second time, dispose of the timer.
            autoEvent.WaitOne();
            stateTimer.Dispose();
            Console.WriteLine("\nDestroying timer.");
        }
        
    }

    class StatusChecker
    {
        static string connStr = "server=127.0.0.1;user=sa;port=3306;password=Ne16450558";
        public StatusChecker()
        {
        }

        // This method is called by the timer delegate.
        public void CheckStatus(Object stateInfo)
        {
            AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;
            TimedEvent();   //檢查時間執行事件
        }

        private static void TimedEvent()
        {
            var stime = DateTime.Now.ToLongTimeString();    //今天時間
            var sdatetime = DateTime.Now.ToShortDateString();   //今天日期
            var T_year = DateTime.Now.Year.ToString();  //今年度

            //先去檢查今年必要table需先產生
            //每年最後一天晚上新增下年度的table、schema才來的及隔天收資料時寫入
            if (sdatetime == T_year + "/12/31"  && stime == "下午 11:30:00")
            {
                Create_table(); 
            }
            //Console.WriteLine(sdatetime == T_year + "/1/3"  && stime == "下午 12:06:00");
            //存前一日加總平均值
            if (stime == "上午 08:01:00")
            {
                Console.WriteLine("Save Data started at {0:yyyy-MM-d HH:mm:ss.fff}", DateTime.Now);
                String s_year = DateTime.Now.Year.ToString();
                var s_array = new List<String>();   //存電池名
                var pre_day = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");   //前一天
                string sql = "";

                //讀取MAC及電池, 從dev_data來放入字串
                //for loop 去寫入取得的資料
                MySqlConnection conn = new MySqlConnection(connStr);

                try
                {
                    MySqlCommand cmd;
                    conn.Open();
                    sql = "select addr from esp.dev_data group by addr";    //讀取電池資料
                    cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        s_array.Add(rd.GetString(0));
                    }
                    rd.Close();
                    cmd.Dispose();

                    //寫入資料庫
                    /*if (conn.State != System.Data.ConnectionState.Open)
                    {
                        conn.Open();
                    }*/
                    for (var i = 1; i < s_array.Count; i++)
                    {
                        int count = 0;

                        //檢查table是否存在
                        count = check_exist("table", s_year, "day_" + s_array[i]);

                        //如果table不存在就create 
                        if (count == 0)
                            get_sql_data("daily", s_year, s_array[i]);

                        //檢查是否有今天資料
                        sql = "select count(*) from a" + s_year + ".day_" + s_array[i] + " where addr='" + s_array[i] + "' and dat_time='" + pre_day + "'";
                        cmd = new MySqlCommand(sql, conn);
                        rd = cmd.ExecuteReader();

                        while (rd.Read())
                            count = rd.GetInt32(0);

                        rd.Close();
                        cmd.Dispose();

                        //如果沒有重覆資料時再存檔
                        if (count == 0)
                        {
                            sql = "INSERT INTO a" + s_year + ".day_" + s_array[i] + "(dev_mac,addr,dat_time,a001,a002,a003,a004,a005,a006,a007,a008,a009,a010,a011,a012,a013,a014,a015) " +
                            " select dev_mac,A001 ,'" + pre_day + "' ,IFNULL(round(avg(A002),2),0) as a001,IFNULL(round(avg(A003),2),0) as a002,IFNULL(round(avg(A004),2),0) as a003,IFNULL(round(avg(A005),2),0) as a004 " +
                            ",IFNULL(round(avg(A006),2),0) as a005,IFNULL(round(avg(A009),2),0) as a006,IFNULL(round(avg(A010),2),0) as a007,IFNULL(round(avg(A011),2),0) as a008,IFNULL(round(avg(A013),2),0) as a009" +
                            ",IFNULL(round(avg(A037),2),0) as a010,IFNULL(round(avg(A038),2),0) as a011,IFNULL(round(avg(A039),2),0) as a012,IFNULL(round(avg(A040),2),0) as a013,IFNULL(round(avg(A041),2),0) as a04";
                            if (Int32.Parse(s_year) > 2023)
                            {//2024 後讀取電池by 年的分析table資料, 2023是抓rec_data
                                sql = sql + ",IFNULL(round(avg(A042),2),0) as a015 from a" + s_year + ".rec_" + s_array[i] + " where h_today like '" + pre_day + "%' and a" + s_year + ".rec_" + s_array[i] + ".A001='" + s_array[i] + "' group by dev_mac,a" + s_year + ".rec_" + s_array[i] + ".A001 ,'" + pre_day + "';";
                            }
                            else
                            {
                                sql = sql + ",IFNULL(round(avg(A042),2),0) as a015 from esp.rec_data where h_today like '" + pre_day + "%' and a" + s_year + ".rec_" + s_array[i] + ".A001='" + s_array[i] + "' group by dev_mac,a" + s_year + ".rec_" + s_array[i] + ".A001 ,'" + pre_day + "';";
                            }
                            cmd = new MySqlCommand(sql, conn);
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            Console.WriteLine("Save " + s_array[i] + " Data end at {0:HH:mm:ss.fff}", DateTime.Now);
                        }
                        else
                        {
                            Console.WriteLine(s_array[i] + "No data Save ");
                        }
                    }
                    conn.Close();
                    conn.Dispose();
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        private static int check_exist(string type, string s_year,string tablename)
        {
            int count = 0;
            string sql = "";

            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            try
            {
                conn.Open();
                switch (type)
                {
                    case "schema":
                        sql = " select count(*) from INFORMATION_SCHEMA.SCHEMATA where SCHEMA_NAME='a" + s_year + "';";
                        break;
                    case "table":
                        sql = " select count(TABLE_NAME) from INFORMATION_SCHEMA.TABLES where table_schema='a" + s_year + "' and table_name='" + tablename + "'";
                        break;
                }

                cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                    count = rd.GetInt32(0);

                rd.Close();
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return count;
        }

        private static void Create_table()
        {
            //每年最後一天晚上新增下年度的table、schema才來的及隔天收資料時寫入
            var a_array = new List<String>();                   //存電池名
            var N_year = DateTime.Now.AddYears(1).Year.ToString();  //下一年度
            //var N_year = DateTime.Now.Year.ToString();  //今年度
            string sql = "";

            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            try
            {
                Console.WriteLine("Create table started at {0:HH:mm:ss.fff}", DateTime.Now);
                //產生schema
                if (check_exist("schema", N_year, "")==0)
                    get_sql_data("schema", N_year, "");


                //產生必要table
                sql = "select addr from esp.dev_data group by addr";    //取得所有設備的電池 //註消的設備要備註
                conn.Open(); 
                cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    a_array.Add(rd.GetString(0)); //讀取電池資料
                }
                rd.Close();
                cmd.Dispose();

                //新增年度原始資料table org不分電池組
                if (check_exist("table", N_year, "org_data") == 0)
                    get_sql_data("org", N_year, "1");  

                //產生每日電池總平均table、年度接收table、年度原始資料table
                for (var i=0;i<a_array.Count;i++)
                {
                    if (check_exist("table", N_year, "day_"+ a_array[i] ) == 0)
                        get_sql_data("daily", N_year, a_array[i]); //新增每日加總平均table

                    if (check_exist("table", N_year, "rec_" + a_array[i]) == 0)
                        get_sql_data("rec", N_year, a_array[i]);  //新增年度接收table
                    
                }
                Console.WriteLine("Create table end at {0:HH:mm:ss.fff}", DateTime.Now);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void get_sql_data(string type,string s_year,string s_battery)
        {
            String sql = "";

            switch (type)
            {
                case "org":
                    sql= "use a"+s_year+"; CREATE TABLE `org_data` (  `i_index` INT NOT NULL AUTO_INCREMENT,`DAT_TIME` DATETIME NOT NULL , `A001` CHAR(32) NULL, `A002` CHAR(32) NULL, `A003` CHAR(32) NULL,"
                        + " `A004` CHAR(32) NULL,`A005` CHAR(32) NULL,`A006` CHAR(32) NULL, `A007` CHAR(32) NULL, `A008` CHAR(32) NULL, `A009` CHAR(32) NULL, `A010` CHAR(32) NULL, `B001` TEXT NOT NULL, `B002` TEXT NOT NULL,"
                        + " `B003` TEXT NULL,`B004` TEXT NULL, `B005` TEXT NULL,`B006` TEXT NULL, `B007` TEXT NULL, `B008` TEXT NULL, `B009` TEXT NULL, `B010` TEXT NULL, `C001` TEXT NULL, `C002` TEXT NULL, `C003` TEXT NULL,"
                        + " `C004` TEXT NULL,`C005` TEXT NULL, `C006` TEXT NULL, `C007` TEXT NULL, `C008` TEXT NULL, `C009` TEXT NULL, `C010` TEXT NULL, PRIMARY KEY (`i_index`), INDEX(`DAT_TIME`),INDEX(`A002`) );";
                    break;
                case "rec":
                    sql = "use a"+s_year+"; CREATE TABLE `rec_" + s_battery + "` ( `i_index` INT NOT NULL AUTO_INCREMENT, `DAT_TIME` DATETIME NOT NULL, `dev_mac` CHAR(32) NOT NULL, `i_type` INT(8) NOT NULL,"
                        +"`A001` CHAR(20) NOT NULL, `A002` CHAR(20) NULL, `A003` CHAR(20) NULL, `A004` CHAR(20) NULL, `A005` CHAR(20) NULL, `A006` CHAR(20) NULL, `A007` CHAR(20) NULL, `A008` CHAR(20) NULL," 
                        +"`A009` CHAR(20) NULL, `A010` CHAR(20) NULL, `A011` CHAR(20) NULL, `A012` CHAR(20) NULL, `A013` CHAR(20) NULL, `A014` CHAR(20) NULL, `A015` CHAR(20) NULL, `A016` CHAR(20) NULL," 
                        +"`A017` CHAR(20) NULL, `A018` CHAR(20) NULL, `A019` CHAR(20) NULL, `A020` CHAR(20) NULL,`A021` CHAR(20) NULL, `A022` CHAR(20) NULL, `A023` CHAR(20) NULL, `A024` CHAR(20) NULL," 
                        +"`A025` CHAR(20) NULL, `A026` CHAR(20) NULL, `A027` CHAR(20) NULL, `A028` CHAR(20) NULL, `A029` CHAR(20) NULL, `A030` CHAR(20) NULL, `A031` CHAR(20) NULL, `A032` CHAR(20) NULL," 
                        +"`A033` CHAR(20) NULL, `A034` CHAR(20) NULL, `A035` CHAR(20) NULL, `A036` CHAR(20) NULL, `A037` CHAR(20) NULL, `A038` CHAR(20) NULL, `A039` CHAR(20) NULL, `A040` CHAR(20) NULL," 
                        +"`A041` CHAR(20) NULL, `A042` CHAR(20) NULL, `A043` CHAR(20) NULL, `A044` CHAR(20) NULL, `A045` CHAR(20) NULL, `A046` CHAR(20) NULL, `A047` CHAR(20) NULL, `A048` CHAR(20) NULL," 
                        +"`A049` CHAR(20) NULL, `A050` CHAR(20) NULL, `A051` CHAR(20) NULL, `A052` CHAR(20) NULL, `A053` CHAR(20) NULL, `A054` CHAR(20) NULL, `A055` CHAR(20) NULL, `A056` CHAR(20) NULL," 
                        +"`A057` CHAR(20) NULL, `A058` CHAR(20) NULL, `A059` CHAR(20) NULL, `A060` CHAR(20) NULL, `A061` CHAR(20) NULL, `A062` CHAR(20) NULL, `A063` CHAR(20) NULL, `A064` CHAR(20) NULL," 
                        +"`A065` CHAR(20) NULL, `A066` CHAR(20) NULL, `A067` CHAR(20) NULL, `A068` CHAR(20) NULL, `A069` CHAR(20) NULL, `A070` CHAR(20) NULL, `A071` CHAR(20) NULL, `A072` CHAR(20) NULL," 
                        +"`A073` CHAR(20) NULL, `A074` CHAR(20) NULL, `A075` CHAR(20) NULL, `A076` CHAR(20) NULL, `A077` CHAR(20) NULL, `A078` CHAR(20) NULL, `A079` CHAR(20) NULL, `A080` CHAR(20) NULL," 
                        +"`A081` CHAR(20) NULL, `A082` CHAR(20) NULL, `A083` CHAR(20) NULL, `A084` CHAR(20) NULL, `A085` CHAR(20) NULL, `A086` CHAR(20) NULL, `A087` CHAR(20) NULL, `A088` CHAR(20) NULL," 
                        +"`A089` CHAR(20) NULL, `A090` CHAR(20) NULL, `A091` CHAR(20) NULL, `A092` CHAR(20) NULL, `A093` CHAR(20) NULL, `A094` CHAR(20) NULL, `A095` CHAR(20) NULL, `A096` CHAR(20) NULL," 
                        +"`A097` CHAR(20) NULL, `A098` CHAR(20) NULL, `A099` CHAR(20) NULL, `A100` CHAR(20) NULL,`h_today` VARCHAR(13) NOT NULL,  PRIMARY KEY (`i_index`), INDEX(`dev_mac`), INDEX(`A001`),"
                        +"INDEX(`i_type`), INDEX(`h_today`), INDEX(`DAT_TIME`) ); ";
                    break;
                case "daily":
                    sql = "use a" + s_year + ";CREATE TABLE `day_" + s_battery + "` ( `i_index` INT NOT NULL AUTO_INCREMENT, `dev_mac` CHAR(32) NOT NULL,  `DAT_TIME` DATETIME NOT NULL,"
                        + "`addr` CHAR(20) NOT NULL,`A001` VARCHAR(10) NULL, `A002` VARCHAR(10) NULL, `A003` VARCHAR(10) NULL, `A004` VARCHAR(10) NULL, `A005` VARCHAR(10) NULL, `A006` VARCHAR(10) NULL,"
                        + " `A007` VARCHAR(10) NULL,`A008` VARCHAR(10) NULL, `A009` VARCHAR(10) NULL, `A010` VARCHAR(10) NULL, `A011` VARCHAR(10) NULL, `A012` VARCHAR(10) NULL, `A013` VARCHAR(10) NULL, "
                        + "`A014` VARCHAR(10) NULL,`A015` VARCHAR(10) NULL, `A016` VARCHAR(10) NULL, `A017` VARCHAR(10) NULL, `A018` VARCHAR(10) NULL, `A019` VARCHAR(10) NULL, `A020` VARCHAR(10) NULL,"
                        + " PRIMARY KEY(`i_index`),INDEX(`dev_mac`),INDEX(`DAT_TIME`),INDEX(`addr`)); ";
                    break;
                case "schema":
                    sql = "Create schema a" + s_year;
                    break;
            }

            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd;
            try
            {
                conn.Open();
                cmd = new MySqlCommand(sql, conn);
                var ans=cmd.ExecuteNonQuery();

                cmd.Dispose();
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
