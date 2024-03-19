using System;
using System.Text;
using System.Threading.Tasks;
using wyer.Models;
using alpsun.Views;
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
            var weburl = new Views.ControlPage("https://www.alpsun.com.sg/");
            await Navigation.PushAsync(weburl);
        }

        private async void AboutPage_Clicked(object sender, EventArgs e)
        {
            var weburl = new Views.ControlPage("https://www.alpsun.com.sg/");
            await Navigation.PushAsync(weburl);
        }

        private async void FQAPage_Clicked(object sender, EventArgs e)
        {
            var weburl = new Views.ControlPage("http://www.wyer.com.tw/en/FQA-alpsun.html");
            await Navigation.PushAsync(weburl);
        }

        private async void ManualPage_Clicked(object sender, EventArgs e)
        {
            //var weburl = new Views.ControlPage("http://118.163.50.93/usermanual");
            //await Navigation.PushAsync(weburl);
            var webpage = new alpsun.Views.webpage("http://118.163.50.93/usermanual");
            await Navigation.PushAsync(webpage);
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