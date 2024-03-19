using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ERP2
{

    public partial class WebForm2 : System.Web.UI.Page
    {
        //讀取資料庫資料
        public static string connectionString = "data source=192.168.1.32; initial catalog=sn_erp;user id=wyerap;password=8G43kgh";
        public SqlConnection connection = new SqlConnection(connectionString);
        public static string S_firstSN = "";
        public static string S_EndSN = "";

        protected void Page_Load(object sender, EventArgs e)
        {
	    
            //只允一位進入系統

            if (Request.Cookies["UserName"].Value != null)
            { //當session有值時, 取值
                LoginNM.Text = Request.Cookies["UserName"].Value;
            }
            else
            {   //重回登入畫面
                Response.Redirect("Default.aspx", false);
            }

            //取得sql資料
            Put_SQLDATA();

            //寫入年份
            Label1.Text = DateTime.Now.ToString("yyyy");
            YearLabel.Text = Label1.Text.Substring(2, 2);

            //寫入月份
            Label2.Text = DateTime.Now.ToString("MM");
            MonLabel1.Text = GetMon(Label2.Text);

            if (!IsPostBack)
            {
                this.SearchData.Visible = false;
                this.RecentlyData.Visible = true;
            }
		

        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            //上線人數減一
            Application.Lock();
            Application["OnlineUsers"] = (int)Application["OnlineUsers"] - 1;
            Application.UnLock();

            Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);    //清除Cookie
            Response.Redirect("Default.aspx");  //跳回登入晝面
        }
        
        protected void Put_SQLDATA()
        {
            //寫入產品
            string sql = "select * from INVNA order by UDF11";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Connection.Open();
            SqlDataReader dr;

            if (ProNM.Items.Count < 1)   //因為類別及區域在組數已輸入的狀況下, 重覆選取也要可以讓序號也顯示出來.故要做判斷
            { 
                 dr = command.ExecuteReader();
                try
                {
                    int i = 1;
                    ProNM.Items.Clear();    //清空資料;
                    ProNM.Items.Insert(0, new ListItem("請選擇...", ""));  //新增第一筆

                    //新增在查詢中的類別選項
                    Poduct_start.Items.Clear();
                    Poduct_end.Items.Clear();

                    while (dr.Read())
                    {
                        ProNM.Items.Insert(i, new ListItem(dr["MA003"].ToString().Trim(), dr["MA001"].ToString()));
                        Poduct_start.Items.Insert(i-1, new ListItem(dr["MA003"].ToString().Trim(), dr["MA001"].ToString()));
                        Poduct_end.Items.Insert(i-1, new ListItem(dr["MA003"].ToString().Trim(), dr["MA001"].ToString()));
                        i++;
                    }
                    
                }
                finally
                {
                    dr.Close();
                }
            }
            command.Dispose();
            command.Connection.Close();

            //寫入區域
            sql = "select * from INVNC order by MC003";
            command = new SqlCommand(sql, connection);
            command.Connection.Open();

            if (AreaNM.Items.Count < 1)   //因為類別及區域在組數已輸入的狀況下, 重覆選取也要可以讓序號也顯示出來.故要做判斷
            {
                dr = command.ExecuteReader();
                try
                {
                    int i = 1;
                    AreaNM.Items.Clear();    //清空資料;
                    AreaNM.Items.Insert(0, new ListItem("請選擇...", ""));  //新增第一筆

                    //新增在查詢中的類別選項
                    Area_start.Items.Clear();
                    Area_end.Items.Clear();

                    while (dr.Read())
                    {
                        AreaNM.Items.Insert(i, new ListItem(dr["MC002"].ToString().Trim()+" "+ dr["MC003"].ToString(), dr["MC001"].ToString()));
                        Area_start.Items.Insert(i - 1, new ListItem(dr["MC002"].ToString().Trim() + " "+ dr["MC003"].ToString(), dr["MC001"].ToString()));
                        Area_end.Items.Insert(i - 1, new ListItem(dr["MC002"].ToString().Trim()+" " + dr["MC003"].ToString(), dr["MC001"].ToString()));
                        i++;
                    }

                }
                finally
                {
                    dr.Close();
                }
            }
            command.Dispose();
            command.Connection.Close();

            //最得最近10筆序號異動資料

            //取得各產品區域本月份最後一筆資料
            int index = ProNM.SelectedIndex;
            string protext = ProNM.Items[index].Value.Trim();
            string proname= ProNM.Items[index].Text.Trim();

            index = AreaNM.SelectedIndex;
            string areatext = AreaNM.Items[index].Value.Trim();
            string areaname= AreaNM.Items[index].Text.Trim();

            if ((protext != "") && (areatext != ""))
            {   //如果有選產品及區域時, 顯示產品區域本月取號資料
                string wildcard = protext + areatext + YearLabel.Text + MonLabel1.Text;

                sql = "select TOP (10) TD002,TD003,TD007,TD004,TD005 from PSNTD  where TD003 like '"+ wildcard +"%' order by CREATE_TIME DESC";
            }
            else {  //如果沒有選擇產品及區域時, 顯示全部前十筆資料
                sql = "select TOP (10) TD002,TD003,TD007,TD004,TD005 from PSNTD order by CREATE_TIME DESC";
            }
            command = new SqlCommand(sql, connection);
            command.Connection.Open();

            dr = command.ExecuteReader();
            try
            {
                int n = 0;
                string str = "";
                while (dr.Read())
                {
                    str += "<tr><td>✓</td><td> " + dr[0].ToString() + " </td><td> " + dr[1].ToString() + " </td><td> " + dr[2].ToString() +" </td><td> " + dr[3].ToString() +" </td><td> "+dr[4].ToString()+" </td></tr> ";
                    n++;
                }
                if (n==0)
                {
                    str = "<tr><td colspan=5>無資料</td></tr>";
                }
                this.tablebody.InnerHtml = str;
            }
            finally
            {
                dr.Close();
            }
            command.Dispose();
            connection.Close();

            if ((protext != "") && (areatext != "") )
            {
                string wildcard = protext + areatext + YearLabel.Text + MonLabel1.Text;

                sql = "select max(TD003) from PSNTD where TD003 like '"+ wildcard +"%' ";
                command = new SqlCommand(sql, connection);
                command.Connection.Open();

                dr = command.ExecuteReader();
                string str = "產品： " + proname + "  /  區域： " + areaname + "  /  " ;
                while (dr.Read())
                {
                    
                    if (dr[0].ToString()=="")
                    {
                        str += "本月沒有被取過序號";
                    }
                    else
                    {
                        str += "最後序號：  " + dr[0].ToString();
                    }
                }
                lastSN.Text = str;
            }
            command.Dispose();
            connection.Close();

        }

        //計算取得的序號
        protected void PUTSN(object sender, EventArgs e)
        {
            int index = ProNM.SelectedIndex;
            string protext = ProNM.Items[index].Value.Trim();

            index = AreaNM.SelectedIndex;
            string areatext = AreaNM.Items[index].Value.Trim();

            string SqlFirstSN = "00000";    //初始序號

            if ((protext != "") && (areatext != "") && (SNtxt.Text!=""))
            {
                //取得最後序號 取最後一筆資料的流水號, 當成本次初始序號
                string wildcard = protext + areatext + YearLabel.Text + MonLabel1.Text;

                string sql = "select TOP (1) TD006  from PSNTD where TD002 like '"+ wildcard +"%' order by TD006 DESC";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Connection.Open();

                SqlDataReader dr = command.ExecuteReader();
                try
                {
                    int i = 0;
                    while (dr.Read())
                    {
                        SqlFirstSN = dr[0].ToString();
                        i++;
                    }
                    if (i == 0)
                    {
                        SqlFirstSN = "00000";
                    }
                }
                finally
                {
                    dr.Close();
                }
                command.Dispose();
                command.Connection.Close();
                connection.Close();

                S_firstSN = protext + areatext + YearLabel.Text + MonLabel1.Text + MathSN(SqlFirstSN, 1);
                FirstSN.Text = S_firstSN;   //因為SQL儲存時抓不到欄位值, 故丟到變數中
                S_EndSN = protext + areatext + YearLabel.Text + MonLabel1.Text + MathSN(SqlFirstSN,Int32.Parse(SNtxt.Text) );
                EndSN.Text = S_EndSN;
            }
        }

        protected void Get_SQLDATA(object sender, CommandEventArgs e)
        {
            
            if (e.CommandName == "Click1")
            {//所有產品序號查詢
                this.SearchData.Visible = true;
                this.RecentlyData.Visible = false;

                string year = DateTime.Now.ToString("yyyy");
                int reduce_year = Int32.Parse(year);    //轉成數字做加減的運算
                int i = 0;
                //初始值
                Year_start.Items.Clear();    //清空資料;
                Year_end.Items.Clear();    //清空資料;.Items.Clear();    //清空資料;

                if (Year_start.Items.Count < 1)
                {
                    for (i = 0; i < 10; i++)
                    {
                        Year_start.Items.Insert(i, new ListItem(reduce_year.ToString(), reduce_year.ToString().Substring(2, 2)));
                        Year_end.Items.Insert(i, new ListItem(reduce_year.ToString(), reduce_year.ToString().Substring(2, 2)));
                        reduce_year -= 1;
                    }
                    
                }

                string sql = "select TD012,TD011,TD003,TD004,TD005 FROM PSNTD WHERE (TD003 IN (select max(TD003) FROM PSNTD AS PSNTD_1 WHERE(SUBSTRING(TD002, 5, 3) = '" + YearLabel.Text + MonLabel1.Text + "') GROUP BY SUBSTRING(TD003, 1, 4))) order by CREATE_TIME DESC";
                //int index = DropDownList1.SelectedIndex;
                //if (DropDownList1.Items[index].Value == "0") 
                    
                
                SqlCommand command = new SqlCommand(sql, connection);
                command.Connection.Open();
                SqlDataReader dr;

                dr = command.ExecuteReader();
                try
                {
                    int n = 0;
                    string str = "";
                    this.tablethead.InnerHtml = "<tr class='table-primary'><th scope='col' >#</th><th scope='col'>產品名稱</th><th scope='col'> 區域 </th><th scope='col'>最後序號</th><th scope='col'> 日期 </th><th scope='col'>人員</th></tr>";
                    while (dr.Read())
                    {
                        str += "<tr ><td>✓</td><td> " + dr[0].ToString() + " </td><td> " + dr[1].ToString() + " </td><td> " + dr[2].ToString() + " </td><td> " + dr[3].ToString() + " </td><td> " + dr[4].ToString() + " </td></tr> ";
                        n++;
                    }
                    if (n == 0)
                    {
                        str = "<tr><td colspan=5>無資料</td></tr>";
                    }
                    this.tablebody.InnerHtml = str;
                    this.h2title.InnerHtml = e.CommandArgument.ToString();
                }
                finally
                {
                    dr.Close();
                }
                command.Dispose();
                connection.Close();
            }
            else
            {//不限產品最後10筆資料
                this.SearchData.Visible = false;
                this.RecentlyData.Visible = true;
                string sql = "select TOP (10) TD002,TD003,TD007,TD004,TD005 from PSNTD order by CREATE_TIME DESC";

                SqlCommand command = new SqlCommand(sql, connection);
                command.Connection.Open();
                SqlDataReader dr;

                dr = command.ExecuteReader();
                try
                {
                    int n = 0;
                    string str = "";
                    this.tablethead.InnerHtml = "<tr class='table-primary'><th scope='col'>#</th><th scope='col'>起始序號</th><th scope='col'> 結束序號 </th><th scope='col'>組數</th><th scope='col'> 日期 </th><th scope='col'>人員</th></tr>";
                    while (dr.Read())
                    {
                        str += "<tr><td>✓</td><td> " + dr[0].ToString() + " </td><td> " + dr[1].ToString() + " </td><td> " + dr[2].ToString() + " </td><td> " + dr[3].ToString() + " </td><td> " + dr[4].ToString() + " </td></tr> ";
                        n++;
                    }
                    if (n == 0)
                    {
                        str = "<tr><td colspan=5>無資料</td></tr>";
                    }
                    this.tablebody.InnerHtml = str;
                    this.h2title.InnerHtml = "最近10筆存取記錄";
                }
                finally
                {
                    dr.Close();
                }
                command.Dispose();
                connection.Close();
            }
        }

        protected void Clear_field(object sender, EventArgs e)
        {
            SNtxt.Text = "";    //清空流水號組數
            FirstSN.Text = "";  //清空初始序號
            EndSN.Text = "";    //清空尾序號
        }

        protected void OKbtn_Click(object sender, EventArgs e)
        {
            //寫入產品序號

            int index = ProNM.SelectedIndex;
            string proname = ProNM.Items[index].Text.Trim();

            int index1 = AreaNM.SelectedIndex;
            string areaname = AreaNM.Items[index1].Text.Trim();

            string sql = "insert into PSNTD (COMPANY,CREATOR,CREATE_DATE,CREATE_TIME,TD001,TD002,TD003,TD004,TD005,TD006,TD007,TD008,TD010,TD011,TD012,TD013) values ('wyer','" + LoginNM.Text + "'," + DateTime.Now.ToString("yyyyMMdd") +"," + DateTime.Now.ToString("yyyyMMddHHmmss") +",'"+ MonLabel1.Text + "','" + S_firstSN + "','" + S_EndSN + "'," + DateTime.Now.ToString("yyyyMMdd") + ",'" + LoginNM.Text + "','" + S_EndSN.Substring(7, 5) + "','" + SNtxt.Text + "','"+ YearLabel.Text + "','"+ AreaNM.SelectedValue.Trim() +"','"+ areaname.Trim() + "','"+ ProNM.SelectedValue.Trim()+"','"+ proname.Trim() + "')";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Connection.Open();
            command.ExecuteNonQuery();

            command.Dispose();
            command.Connection.Close();
            connection.Close();
            //MessageBox.Show("寫入完成!", "提示");
            Response.Redirect("GetSN.aspx");
            //ResetForm();    //重導入
        }


        protected void Modibtn_Click(object sender, EventArgs e)
        {
            SNtxt.Text = "";
            this.msgbox.Attributes.Add("style", "display:none");
        }

        //查詢所有資料
        protected void Search_click(object sender,EventArgs e)
        {
            String s_product_start, s_product_end, s_area_start, s_area_end, s_year_start, s_year_end, s_mon_start, s_mon_end;

            s_product_start = Poduct_start.SelectedValue.Trim();
            s_product_end = Poduct_end.SelectedValue.Trim();
            s_area_start = Area_start.SelectedValue.Trim();
            s_area_end = Area_end.SelectedValue.Trim();
            s_year_start = Year_start.SelectedValue.Trim();
            s_year_end = Year_end.SelectedValue.Trim();
            s_mon_start = Mon_start.SelectedValue.Trim();
            s_mon_end= Mon_end.SelectedValue.Trim();

            /*產品和區域, 結束產品(區域)代號必須小於起始產品(區域)代號, 若否則互換
             小於0：s_area_start < s_area_end
            以避免找不到資料
            */
            if (string.Compare(s_area_start,s_area_end)>0)
            {
                string temp = s_area_end;
                s_area_end = s_area_start;
                s_area_start = temp;
            }

            if (string.Compare(s_product_start, s_product_end) > 0)
            {
                string temp = s_product_end;
                s_product_end = s_product_start;
                s_product_start = temp;
            }

            /*月份起始與結束不可有任一為全部, 若其中一個為全部, 則搜尋全部資料
            * 開始年 不可 小於 結束年 ( 若有以起始年=結束年 )
            * 結束年月 不可 小於 起始年月 ( 若有 起始年月= 結束年月 )
            */
            if ((s_mon_start == "0") && (s_mon_end != "0"))
                s_mon_end = "0";

            if ((s_mon_start != "0") && (s_mon_end == "0")) 
                s_mon_start = "0";

            if( string.Compare(s_year_end,s_year_start)<0 )
                s_year_end = s_year_start;

            if(string.Compare((s_year_start+ s_mon_start),(s_year_end+ s_mon_end)) >0)
            {
                s_year_end = s_year_start;
                s_mon_end = s_mon_start;
            }

            string sql = "SELECT  TD001, TD002, TD003, TD004, TD005, TD006, TD007, TD008, TD009, TD010, TD011, TD012, TD013 FROM PSNTD AS a WHERE EXISTS (SELECT MAX(TD003) AS Expr1 FROM PSNTD ";
            //若月選擇"全部", 則不加入篩選條件
            if ((s_mon_start=="0")||(s_mon_end=="0"))
                sql+= "WHERE 1=1 and ";
            else
                sql += "WHERE (TD001 BETWEEN '"+ s_mon_start + "' AND '"+ s_mon_end+ "') AND ";

            sql += " (TD008 BETWEEN '"+ s_year_start +"' and '"+ s_year_end +"') AND (TD012 BETWEEN '"+ s_product_start +"' AND '"+ s_product_end +"') AND (TD010 between '"+ s_area_start +"' and '"+ s_area_end +"') and (a.TD003 = TD003)  GROUP BY    TD001, TD008, TD010, TD011, TD012, TD013)";
            
             SqlCommand command = new SqlCommand(sql, connection);
             command.Connection.Open();
             SqlDataReader dr;

            dr = command.ExecuteReader();
            try
            {
                int n = 0;
                string str = "";
                while (dr.Read())
                {
                    str += "<tr><td>✓</td><td> " + dr[12].ToString() + " </td><td> " + dr[10].ToString() + " </td><td> " + dr[1].ToString() + " </td><td> " + dr[2].ToString() + " </td><td> " + dr[6].ToString() + " </td><td> " + dr[3].ToString() + " </td><td> " + dr[4].ToString() + " </td></tr> ";
                    n++;
                }
                if (n == 0)
                {
                    str = "<tr><td colspan=5>無資料</td></tr>";
                }
                this.Tbody1.InnerHtml = str;
            }
            finally
            {
                dr.Close();
            }

            command.Dispose();
            command.Connection.Close();
            connection.Close();

        }

        //計算月份文字
        String GetMon(String str)
        {
            switch (str)
            {
                case "10":
                    str = "A";
                    break;
                case "11":
                    str = "B";
                    break;
                case "12":
                    str = "C";
                    break;
                default:    ///只取右邊元一位 1~9
                    str = str.Substring(1, 1);
                    break;
            }
            return str;
        }

        //計算序號最後一組號碼
        String MathSN(string firstNo,int count)
        {
            int first_item= Int32.Parse(firstNo);
            //取得最後流水號, 變成數字
            int end_sn = first_item + count;
            //流水號加上本次的組數

            return GetSN(end_sn);
        }

        //組合序號文字
        String GetSN(int sn)
        {
            String final_sn = "";

            int i = 0;

            for(i=sn.ToString().Length;i<5;i++)
            {
                final_sn += "0";
            }
            final_sn += sn.ToString();

            return final_sn;
        }
        
    }
}