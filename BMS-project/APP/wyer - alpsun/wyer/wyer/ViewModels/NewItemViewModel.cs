using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using wyer.Models;
using wyer.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
namespace wyer.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private string text;
        private string description;
        private string remark;
        private readonly Services.AlertService _messageService;

        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this._messageService = DependencyService.Get<Services.AlertService>();

            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

         

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(Text)
                && !String.IsNullOrWhiteSpace(description);
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string Remark
        {
            get => remark;
            set => SetProperty(ref remark, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }


        public string EntryChangedInputText { get; set; }
        public int EntryChangedInputTextLength { get; set; }

        public void OnEntryChangedInputTextChanged()
        {
            if (string.IsNullOrEmpty(EntryChangedInputText))
            {
                EntryChangedInputTextLength = 0;
            }
            else
            {
                EntryChangedInputTextLength = EntryChangedInputText.Length;
            }
        }
        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {

            Item newItem = new Item()
            {
                Id = Guid.NewGuid().ToString(),
                Text = Text.ToUpper(),
                Description = Description,
                Remark = Remark
            };

            //await DataStore.AddItemAsync(newItem);
            var client = new HttpClient();
            try
            {
                var content = new StringContent("json", Encoding.UTF8, "application/json");
                client.Timeout = TimeSpan.FromSeconds(5);
                var uri = "http://118.163.50.93/checkdev/?dev=" + Text ;
                var result = await client.GetAsync(uri);
                //檢查dev_mac是否己登記
                //檢查dev_mca是否合法與是否被註冊 10代表合法並沒被註冊
                // on error throw a exception  
                //sult.EnsureSuccessStatusCode();
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {   //handling the answer
                    String resultString = await result.Content.ReadAsStringAsync();
                    String resp=JsonConvert.DeserializeObject<String>(resultString);

                    if (resp == "10" && resultString.Length > 0)
                    {   //資料正確存檔
                        var myid = Preferences.Get("UserID", "");
                        if (Preferences.Get("UserID", "") == "")
                            myid = TempData.T_id;

                        //uri = "http://118.163.50.93/checkdev/?member="+myid+"&dev_mac="+ Text +"&dev_name=" + Description+"&remark="+Remark;
                        uri = "http://118.163.50.93/additem/?member=" + myid + "&dev_mac=" + Text + "&dev_name=" + Description + "&remark=" + Remark;
                        result = await client.GetAsync(uri);
                        //result.EnsureSuccessStatusCode();
                        if (result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var finalString = await result.Content.ReadAsStringAsync();
                            var resultString2 = JsonConvert.DeserializeObject<String>(finalString);
                            if (resultString2 == "OK" && resultString2.IndexOf("<") == -1 && resultString2.Length > 0)
                            {
                                await _messageService.ShowAsync("Save completed !");
                                await Shell.Current.GoToAsync("..");
                            }
                            else
                            {
                                await _messageService.ShowAsync(finalString);
                            }
                        }
                    }
                    else
                    {
                        //ID重覆
                        if(resp.Length == 1 && resp == "0")
                            await _messageService.ShowAsync("No this device MAC! \n Please Confirm !");
                        else if(resp.Length == 2 && resp.Substring(1,1) != "0" )
                            await _messageService.ShowAsync("This Device has been configured! \n Please Confirm !");
                    }
                }
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally{
                client.Dispose();
            }

            // This will pop the current page off the navigation stack
            
        }
    }
}
