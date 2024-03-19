using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows;
using wyer.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace wyer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserDataPage : ContentPage
    {
        public UserDataPage()
        {
            InitializeComponent();
            Title = "Member Infomation";

            ReadData();
        }

        private async void ReadData()
        {
            var myid = Preferences.Get("UserID", "");
            if (myid.Length == 0)
                myid = TempData.T_id;

            var client = new HttpClient();
            try
            {
                var content = new StringContent("json", Encoding.UTF8, "application/json");
                client.Timeout = TimeSpan.FromSeconds(5);
                var uri = "http://118.163.50.93/member/?id=" + myid;
                var result = await client.GetAsync(uri);

                // on error throw a exception  
                //result.EnsureSuccessStatusCode();

                // handling the answer  
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    var resultString = await result.Content.ReadAsStringAsync();
                    if (resultString.IndexOf("<") == -1 && resultString.Length > 0)    //若有錯誤就不做//如逾時或錯誤卜息
                    {
                        var json = JsonConvert.DeserializeObject<UserData>(resultString);

                        x0.Text = json.Id;
                        User_Name.Text = json.usname;
                        Passwrod.Text = json.pswd;
                        Nick_Name.Text = json.nkname;
                        Telephone.Text = json.tel;
                        Mobile.Text = json.mobile;
                        E_mail.Text = json.e_mail;
                    }
                    //若錯誤就不顯示基本資料
                }
            }
            catch(Exception ex)
            {
                Debug.Write(ex.Message);
            }
        }

        private async void Change_Clicked(object sender, EventArgs e)
        {

            //check不允空白的欄位 (member,username, password, mobile, email)
            var member = x0.Text;
            var username = User_Name.Text;
            var password = Passwrod.Text;
            var NKname= Nick_Name.Text;
            var tel = Telephone.Text;
            var mobile = Mobile.Text;
            var email = E_mail.Text;

            if (username.Length == 0 || password.Length == 0 || email.Length == 0)
                await DisplayAlert("Info", "User Name or Password or E-mail Blanks are not allowed!","OK");
            else
            {
                //儲存資料
                var client = new HttpClient();
                try
                {
                    var content = new StringContent("json", Encoding.UTF8, "application/json");
                    client.Timeout = TimeSpan.FromSeconds(5);
                    var uri = "http://118.163.50.93/savemember/?id="+member+"&Uname=" + username + "&Pswd=" + password + "&nkname="+NKname+"&tel="+tel+"&mobile="+mobile+"&email="+email;
                    var result = await client.GetAsync(uri);

                    // on error throw a exception  
                    //result.EnsureSuccessStatusCode();
                    if (result.StatusCode == HttpStatusCode.OK)
                    {
                        // handling the answer  
                        var resultString = await result.Content.ReadAsStringAsync();
                        if (resultString.IndexOf("<") == -1 && resultString.Length > 0)    //若有錯誤就不做//如逾時或錯誤卜息
                        {
                            var json = JsonConvert.DeserializeObject<string>(resultString);
                            if (json != "OK")
                                await DisplayAlert("Info", json, "OK");
                            else
                            {
                                await DisplayAlert("Message", "Save Completed!", "OK");
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                finally
                {
                    client.Dispose();
                }
            }
        }
    }
}