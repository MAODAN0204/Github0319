using System;
using System.Text;
using System.Threading.Tasks;
using wyer.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace wyer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SystemPage : ContentPage
    {
        public SystemPage()
        {
            InitializeComponent();

            Title = "System";

            var havename = Preferences.Get("UserName", "");
            var haveNname = Preferences.Get("NickName", "");
            if (havename.Length != 0)
            {
                if (haveNname.Length != 0)
                    nickname.Text = haveNname;
                else
                    nickname.Text = havename;
            }
            else
            {
                if (TempData.T_id.Length == 0)
                    nickname.Text = "User";
                else
                    nickname.Text = TempData.T_name;
            }
        }

        private async void UserData_Clicked(object sender, EventArgs e)
        {
            var UserDataPage = new Views.UserDataPage();
            await Navigation.PushAsync(UserDataPage);
        }

        void OnToggled(object sender, ToggledEventArgs e)
        {
            // Perform an action after examining e.Value
        }

        private async void ContactPage_Clicked(object sender, EventArgs e)
        {
            var weburl = new Views.ControlPage("http://www.wyer.com.tw/en/contact.html");
            await Navigation.PushAsync(weburl);
            
        }

        private async void AboutPage_Clicked(object sender, EventArgs e)
        {
            var weburl = new Views.ControlPage("http://www.wyer.com.tw/en/about.html");
            await Navigation.PushAsync(weburl);
        }

        private async void FQAPage_Clicked(object sender, EventArgs e)
        {
            var weburl = new Views.ControlPage("http://www.wyer.com.tw/en/FQA.html");
            await Navigation.PushAsync(weburl);
        }

        private async void ManualPage_Clicked(object sender, EventArgs e)
        {
            //var weburl = new Views.ControlPage("http://www.wyer.com.tw/en/usermanual/");
            //await Navigation.PushAsync(weburl);
            var webView = new Views.webPage("http://www.wyer.com.tw/en/usermanual/");
            await Navigation.PushAsync(webView);
        }

        private async void Logout_Clicked(object sender, EventArgs e)
        {
            //Clear all data
            Preferences.Remove("UserID");
            Preferences.Remove("DeviceMac", "");
            Preferences.Remove("DeviceName", "");
            Preferences.Remove("batteryname", "");
            //await 
            var Logout = new LoginPage();
            await Navigation.PushAsync(Logout);
        }
    }
}