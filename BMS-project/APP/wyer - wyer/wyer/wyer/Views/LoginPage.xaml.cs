using System;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using wyer.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Newtonsoft.Json;
using System.Diagnostics;

namespace wyer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            //this.BindingContext = new LoginViewModel();

            CheckInfo();
        }

        private async void CheckInfo()
        {
            var Account = Preferences.Get("UserID", "");

            if (Account != "")
                await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if ((username.Text != null) && (password.Text != null))
            {
                //if user remeber username and pawword, if not do nothing

                var myusername = username.Text;
                var mypassword = password.Text;
                var client = new HttpClient();
                try
                {
                    var content = new StringContent("json", Encoding.UTF8, "application/json");
                    client.Timeout = TimeSpan.FromSeconds(5);
                    var uri = "http://118.163.50.93/checkmember/?Uname=" + myusername + "&Pswd=" + mypassword;
                    var result = await client.GetAsync(uri);

                    // on error throw a exception  
                    //result.EnsureSuccessStatusCode();

                    // handling the answer  
                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var resultString = await result.Content.ReadAsStringAsync();
                        if (resultString == "[]")  //找不到資料
                        {
                            await DisplayAlert("Info", "UserName or Password Error! \n \nPls Check!", "OK");
                        }
                        else
                        {
                            if (resultString.IndexOf("<") == -1 && resultString.Length > 0)    //若有錯誤就不做//如逾時或錯誤卜息
                            {
                                var json = JsonConvert.DeserializeObject<UserInfos>(resultString);

                                if (RemeberInfo.IsChecked == true)
                                {
                                    Preferences.Set("UserID", json.Id);
                                    Preferences.Set("UserName", json.usname);
                                    Preferences.Set("NickName", json.nkname);
                                    Preferences.Set("AlertCount", 0); //alert message 設成0
                                }

                                TempData.T_id = json.Id;
                                TempData.T_name = json.usname;
                                TempData.T_nick = json.nkname;

                                await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
                                //await PopupNavigation.Instance.PushAsync(new HomePage);
                            }
                            else
                            {
                                await DisplayAlert("Message", "Connect wyer.com.tw", "OK");
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    //Debug.WriteLine(ex.Message);
                    await DisplayAlert("Message", "Contact System Administrator", "OK");
                }
                finally
                {
                    client.Dispose();
                }
            }
            else
                await DisplayAlert("Info", "Input Data Error!", "OK");
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var weburl = new Views.ControlPage("http://118.163.50.93/register");
            await Navigation.PushAsync(weburl);
        }

        private async void TapGestureForgetPS_Tapped(object sender, EventArgs e)
        {
            var weburl = new Views.ControlPage("http://118.163.50.93/forget");
            await Navigation.PushAsync(weburl);
        }

        private async Task<String> InputUserInfo()
        {
            var myusername = username.Text;
            var mypassword = password.Text;

            // WebRequest request = WebRequest.Create("https://192.168.1.146/member");
            // request.Method = "POST";
            var client = new HttpClient();

            try
            {

                var content = new StringContent("json", Encoding.UTF8, "application/json");
                client.Timeout = TimeSpan.FromSeconds(5);
                var uri = "http://118.163.50.93/checkmember/?Uname=" + myusername + "&Pswd=" + mypassword;
                var result = await client.PostAsync(uri, content);

                // on error throw a exception  
                //result.EnsureSuccessStatusCode();
                String resultString = "";
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                { 
                    // handling the answer  
                    resultString = await result.Content.ReadAsStringAsync();
                }
                return resultString;
                
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return (ex.Message);
            }
            finally
            {
                client.Dispose();
            }
        }
    }
}