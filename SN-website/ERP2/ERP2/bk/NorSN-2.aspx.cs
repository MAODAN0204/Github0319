﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ERP2
{

    public partial class NorSN : System.Web.UI.Page
    {
        //讀取資料庫資料
        public static string connectionString = "data source=127.0.0.1; initial catalog=web-erp;user id=sa;password=8G43kgh";
        public SqlConnection connection = new SqlConnection(connectionString);
        public static string S_firstSN = "";
        public static string S_EndSN = "";

        public static string GetModiData(string id)
        {
            string s_id = id.Substring(2,id.Length-2);

            string sql = "select TR002,TR003,TR007,TR004,TR005,TR011 from osntr where TR011 like '" + id + "%' order by create_time DESC";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sql, connection);
            command.Connection.Open();
            SqlDataReader dr;

            dr = command.ExecuteReader();
            try
            {
                int n = 0;
                while (dr.Read())
                {
                    
                }
                if (n == 0)
                {
                }
            }
            finally
            {
                dr.Close();
            }
            command.Dispose();
            connection.Close();

            return "1";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["UserName"].Value != "")
            { //當session有值時, 取值
                LoginNM.Text = Request.Cookies["UserName"].Value;
            }
            else
            {   //重回登入畫面
                Response.Redirect("Default.aspx", false);
            }

            //寫入年份
            Label3.Text = DateTime.Now.ToString("yyyy");
            YearLabel.Text = Label3.Text.Substring(2, 2);

            //寫入月份
            Label5.Text = DateTime.Now.ToString("MM");
            MonLabel1.Text = GetMon(Label5.Text);

            //取得sql資料
            Put_SQLDATA();

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
            //最得最近10筆序號異動資料

            //取得各產品區域本月份最後一筆資料

            string wildcard =  YearLabel.Text + MonLabel1.Text;
            string sql = "select TOP (10) TR002,TR003,TR007,TR004,TR005,TR011 from osntr where TR003 like '" + wildcard + "%' order by create_time DESC";

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
                    str += "<tr data-toggle='tooltip' data-placement='bottom' title='" + dr[5].ToString() + "'><td>✓</td><td> " + dr[0].ToString() + " </td><td> " + dr[1].ToString() + " </td><td> " + dr[2].ToString() + " </td><td> " + dr[3].ToString() + " </td><td> " + dr[4].ToString() + " </td></tr> ";
                    n++;
                }
                if (n == 0)
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

        }

        protected void PUTSN(object sender, EventArgs e)
        {

            string SqlFirstSN = "00000";    //初始序號

            if (SNtxt.Text != "")
            {
                //取得最後序號 取最後一筆資料的流水號, 當成本次初始序號
                string wildcard = YearLabel.Text + MonLabel1.Text;

                string sql = "select TOP (1) TR006  from osntr where TR002 like '" + wildcard + "%' order by TR006 DESC";
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
                        SqlFirstSN = "000000000000";
                    }
                }
                finally
                {
                    dr.Close();
                }
                command.Dispose();
                command.Connection.Close();
                connection.Close();

                S_firstSN =  YearLabel.Text + MonLabel1.Text + MathSN(SqlFirstSN, 1);
                FirstSN.Text = S_firstSN;   //因為SQL儲存時抓不到欄位值, 故丟到變數中
                S_EndSN = YearLabel.Text + MonLabel1.Text + MathSN(SqlFirstSN, Int32.Parse(SNtxt.Text));
                EndSN.Text = S_EndSN;
            }
        }

        protected void OKbtn_Click(object sender, EventArgs e)
        {
            //寫入序號
            if (S_firstSN !="" && S_EndSN !="")
            {
                string sql = "insert into osntr (company,creator,create_date,create_time,TR002,TR003,TR004,TR005,TR006,TR007,TR008,TR009,TR011) values ('wyer','" + LoginNM.Text + "','" + DateTime.Now.ToString("yyyyMMdd") + "','" + DateTime.Now.ToString("yyyyMMddHHmmss") + "','" + S_firstSN + "','" + S_EndSN + "','" + DateTime.Now.ToString("yyyyMMdd") + "','" + LoginNM.Text + "','" + S_EndSN.Substring(3, 12) + "','" + SNtxt.Text + "','" + YearLabel.Text + "','" + MonLabel1.Text.Trim() + "','"+ TextBox1.Text.Trim() + "')";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();

                command.Dispose();
                command.Connection.Close();
                connection.Close();
                //MessageBox.Show("寫入完成!", "提示");
                Response.Redirect("NorSN.aspx");
                //ResetForm();    //重導入
            }
            else
            {

            }
        }

        protected void Modibtn_Click(object sender, EventArgs e)
        {
            SNtxt.Text = "";
            this.msgbox.Visible = false;
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

                string sql = "select TR005 from osntr group by TR005 order by TR005";

                SqlCommand command = new SqlCommand(sql, connection);
                command.Connection.Open();
                SqlDataReader dr;
                dr = command.ExecuteReader();
                //取得人員名字
                try
                {
                    member.Items.Clear();
                    member.Items.Insert(0, new ListItem("全部...", ""));  //新增第一筆
                    i = 1;
                    while (dr.Read())
                    {
                        member.Items.Insert(i, new ListItem(dr["TR005"].ToString().Trim(), dr["TR005"].ToString()));
                        i++;
                    }
                }
                finally
                {
                    dr.Close();
                }
                command.Dispose();
                command.Connection.Close();
            }
            else
            {//不限產品最後10筆資料
                this.SearchData.Visible = false;
                this.RecentlyData.Visible = true;
                string sql = "select TOP(10) TR002,TR003,TR007,TR004,TR005,TR011 from osntr order by create_time DESC";
                
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
                        str += "<tr data-toggle='tooltip' data-placement='bottom' title='" + dr[5].ToString() + "'><td>✓</td><td> " + dr[0].ToString() + " </td><td> " + dr[1].ToString() + " </td><td> " + dr[2].ToString() + " </td><td> " + dr[3].ToString() + " </td><td> " + dr[4].ToString() + " </td></tr> ";
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

        //查詢所有資料
        protected void Search_click(object sender, EventArgs e)
        {
            String  s_year_start, s_year_end, s_mon_start, s_mon_end ,s_member;

            s_year_start = Year_start.SelectedValue.Trim();
            s_year_end = Year_end.SelectedValue.Trim();
            s_mon_start = Mon_start.SelectedValue.Trim();
            s_mon_end = Mon_end.SelectedValue.Trim();
            s_member = "";

            /*月份起始與結束不可有任一為全部, 若其中一個為全部, 則搜尋全部資料
            * 開始年 不可 小於 結束年 ( 若有以起始年=結束年 )
            * 結束年月 不可 小於 起始年月 ( 若有 起始年月= 結束年月 )
            */
            if ((s_mon_start == "0") && (s_mon_end != "0"))
                s_mon_end = "0";

            if ((s_mon_start != "0") && (s_mon_end == "0"))
                s_mon_start = "0";

            if (string.Compare(s_year_end, s_year_start) < 0)
                s_year_end = s_year_start;

            if (string.Compare((s_year_start + s_mon_start), (s_year_end + s_mon_end)) > 0)
            {
                s_year_end = s_year_start;
                s_mon_end = s_mon_start;
            }

            if (member.SelectedItem.Value != "")
            {
                s_member = member.SelectedValue.Trim();
            }
            string sql = "SELECT TR002,TR003,TR007,TR004,TR005,TR011,UDF11 from osntr ";
            //若月選擇"全部", 則不加入篩選條件
            if ((s_mon_start == "0") || (s_mon_end == "0"))
                sql += "WHERE 1=1 and ";
            else
                sql += "WHERE (TR009 BETWEEN '" + s_mon_start + "' AND '" + s_mon_end + "') AND ";

            if (s_member !="")
            {
                sql+= " TR005 ='"+s_member+"' AND ";
            }
            sql += " (TR008 BETWEEN '" + s_year_start + "' and '" + s_year_end + "') order by create_time desc";

            SqlCommand command = new SqlCommand(sql, connection);
            command.Connection.Open();
            SqlDataReader dr;

            dr = command.ExecuteReader();
            try
            {
                int n = 1;
                string str = "";
                // Add table header row
              /*  TableRow headerRow = new TableRow();
                TableHeaderCell header = new TableHeaderCell();
                header.Text = "備註";
                header.Attributes.Add("class", "text-center");
                headerRow.Cells.Add(header);
                header = new TableHeaderCell();
                header.Text = "起始序號";
                headerRow.Cells.Add(header);
                header = new TableHeaderCell();
                header.Text = "結束序號";
                headerRow.Cells.Add(header);
                header = new TableHeaderCell();
                header.Text = "組數";
                headerRow.Cells.Add(header);
                header = new TableHeaderCell();
                header.Text = "日期";
                headerRow.Cells.Add(header);
                header = new TableHeaderCell();
                header.Text = "人員";
                headerRow.Cells.Add(header);
                headerRow.CssClass = "table-danger";    //加入CSS套表
                Table1.Rows.AddAt(0, headerRow);*/
                //end add header
                //<asp:Button runat = "server" id="ModifyBtn" class="btn btn-dark" disabled="true" text="修改"/>
               
                while (dr.Read())
                {
                    /*TableRow row = new TableRow();  //新增一行
                    TableCell cell = new TableCell();

                    //add row attrubutes
                    row.Attributes.Add("title", dr[5].ToString());
                    row.Attributes.Add("data-toggle", "tooltip");
                    row.Attributes.Add("data-placement", "bottom");

                    //add button
                    System.Web.UI.WebControls.RadioButton btn = new System.Web.UI.WebControls.RadioButton();
                    btn.ID = "bt" + dr[6].ToString();
                    btn.GroupName = "GroupRadio";
                    //btn.CommandArgument = 1;
                    cell.Attributes.Add("class", "text-center");
                    cell.Controls.Add(btn);         //加入按鍵
                    row.Cells.Add(cell);

                    //add cell text
                    for (int i = 0; i < 5; i++) {
                        cell = new TableCell(); //新增一欄
                        cell.Text = dr[i].ToString();
                        row.Cells.Add(cell);
                    }
                    Table1.Rows.AddAt(n,row);*/

                    str = "<tr title='"+dr[5].ToString () +"' data-toggle='tooltip' data-placement='bottom'><td><input type='radio' name='GroiupRadio' id='bt"+dr[6].ToString() +"</td><td>"+dr[1].ToString()+ "</td><td>" + dr[2].ToString() + "</td><td>" + dr[3].ToString() + "</td><td>" + dr[3].ToString() + "</td><td>" + dr[4].ToString() + "</td></tr>";
                    n++;
                }
                if (n == 0)
                {
                    /*TableRow row = new TableRow();  //新增一行
                    TableCell cell = new TableCell();
                    cell.ColumnSpan = 5;
                    cell.Text = "無資料";
                    row.Cells.Add(cell);

                    Table1.Rows.AddAt(1, row);*/
                    str = " < tr columspan='5'>< td >無資料</ td > ";
                }
                Tbody2.InnerHtml = str;
            }
            finally
            {
                dr.Close();
            }

            command.Dispose();
            command.Connection.Close();
            connection.Close();


        }
       

        protected void ModifyBtn(object sender,EventArgs e)
        {
            //確定修改
            
        }

        protected void CancelModify(object sender, EventArgs e)
        {   //取消修改, 關閉修改畫面
            this.psmodisection.Visible = false;
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
        String MathSN(string firstNo, int count)
        {
            int first_item = Int32.Parse(firstNo);
            //取得最後流水號, 變成數字
            int end_sn = first_item + count;
            //流水號加上本次的組數

            return GetSN(end_sn);
        }

        //組合序號文字
        String GetSN(int sn)
        {
            String final_sn = "";

            int i=0;
            // 拼出流水號前的0
            for (i = sn.ToString().Length; i < 12; i++) 
            {
                final_sn += "0";
            }

            final_sn += sn.ToString();
            
            return final_sn;
        }

        
    }
}