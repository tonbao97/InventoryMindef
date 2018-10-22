using Inventory.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inventory.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        private const string Url = "http://192.168.1.103:12345/Token";     
        private HttpClient client = new HttpClient();

        public Login()
        {
            InitializeComponent();
            Background.Source = ImageSource.FromResource("Inventory.Image.background.jpg");
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {

            //if (Username.Text.Equals("") || Password.Text.Equals(""))
            //{
            //    await DisplayAlert("Notice", "Please enter username and password", "Okay");
            //    Error.Text = "Username and password can't be empty";
            //}
            //else
            //{
            //    var dict = new Dictionary<string, string>();
            //    dict.Add("username", Username.Text);
            //    dict.Add("password", Password.Text);
            //    dict.Add("grant_type", "password");
            //    var client = new HttpClient();
            //    var response = await client.PostAsync(Url, new FormUrlEncodedContent(dict));

            //    if (response.IsSuccessStatusCode)
            //    {
            //        var text = response.Content.ReadAsStringAsync();

            //        var Token = JsonConvert.DeserializeObject<Token>(text.Result);

            //        Application.Current.Properties["Token"] = Token.access_token;
            //        Application.Current.Properties["Name"] = Token.userName;
            //        Application.Current.Properties["Type"] = Token.token_type;

            //        await Navigation.PushAsync(new MainMenu());
            //    }
            //    else
            //    {
            //        await DisplayAlert("Error", "Wrong username or password", "Ok");
            //        Error.Text = "Wrong username or password";
            //    }
            //}
            await Navigation.PushAsync(new MainMenu());
            }

        //protected override void OnAppearing()
        //{
        //    if (Application.Current.Properties.ContainsKey("Token"))
        //    {
        //        if(Application.Current.Properties["Token"] != null)
        //        {
        //            Navigation.PushAsync(new MainMenu());
        //        }
        //    }
        //    base.OnAppearing();
        //}
    }
}

