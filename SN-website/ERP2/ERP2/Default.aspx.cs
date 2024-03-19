using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.DirectoryServices;
using System.Security.Principal;


namespace ERP2
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.RemoveAll();    //清除session

            this.menulist.Visible = false;
        }
        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Username.Text != "") || (ps.Text != ""))
                {   //如果帳密都有輸入, 進行驗證
                    string adname = "neotec.nts";
                    string adPath = string.Format(@"LDAP://DC01.neotec.nts", adname);
                    string userSid = "";
                    DirectoryEntry adentry = new DirectoryEntry(adPath, Username.Text, ps.Text);
                    try
                    {
                        userSid = (new SecurityIdentifier((byte[])adentry.Properties["objectSid"].Value, 0).Value);
                    }
                    catch
                    { userSid = "Fail"; }
                    finally
                    { adentry.Dispose(); }

                    if (userSid != "Fail")
                    { /*login successfull 寫入session
                        並將使用者名稱回傳到主頁的登入者顯示區
                        再跳到下一頁面*/

                        //Session["Username"] = Username.Text;
                        HttpCookie UserName= new HttpCookie("UserName");
                        UserName.Value = Server.UrlEncode(Username.Text);
                        UserName.Expires = DateTime.Now.AddDays(2);
                        Response.Cookies.Add(UserName);

                        //Response.Redirect("GetSN.aspx");
                        this.menulist.Visible = true;
                        this.login.Visible = false;
                    }
                    else
                    { //login error    登入失敗
                        Label1.Text = "錯誤：登入的帳號或密碼有誤!";
                    }

                }
                else
                {   //No keyin
                    Label1.Text = "錯誤：請輸入帳號及密碼.";
                }
            }
            catch (Exception ex)
            {   //錯誤
                Label1.Text = ex.Message;
                //error infomation
            }
        }
        protected void ClearBtn_Click(object sender, EventArgs e)
        {   // clear InputBox
            Username.Text = "";
            ps.Text = "";
            Label1.Text = "";
        }
        protected void ProClick(object sender, EventArgs e)
        {
            Response.Redirect("GetSN.aspx");
        }

        protected void OthClick(object sender, EventArgs e)
        {
            Response.Redirect("NorSN.aspx");
        }
    }

}