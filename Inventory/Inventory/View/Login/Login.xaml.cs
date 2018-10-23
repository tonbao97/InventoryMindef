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
        private const string Url = "http://202.160.1.102:8083/Token";     
        private HttpClient client = new HttpClient();

        public Login()
        {
            InitializeComponent();
            Background.Source = ImageSource.FromResource("Inventory.Image.background.jpg");
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {

            if (Username.Text.Equals("") || Password.Text.Equals(""))
            {
                await DisplayAlert("Notice", "Please enter username and password", "Okay");
                Error.Text = "Username and password can't be empty";
            }
            else
            {
                var dict = new Dictionary<string, string>();
                dict.Add("username", Username.Text);
                dict.Add("password", Password.Text);
                dict.Add("grant_type", "password");
                var client = new HttpClient();

                try
                {
                    var response = await client.PostAsync(Url, new FormUrlEncodedContent(dict));
                    if (response.IsSuccessStatusCode)
                    {
                        var text = response.Content.ReadAsStringAsync();

                        var Token = JsonConvert.DeserializeObject<Token>(text.Result);

                        Application.Current.Properties["Token"] = Token.access_token;
                        Application.Current.Properties["Name"] = Token.userName;
                        Application.Current.Properties["Type"] = Token.token_type;

                        await Navigation.PushAsync(new MainMenu());
                    }
                    else
                    {

                        Error.Text = "Wrong username or password";
                    }
                }
                catch (System.Net.WebException Err)
                {
                   await DisplayAlert("Error", "No connection to server", "Noticed");
                }



            }
        }

        protected override void OnAppearing()
        {
            if (Application.Current.Properties.ContainsKey("Token"))
            {
                if (Application.Current.Properties["Token"] != null)
                {
                    Navigation.PushAsync(new MainMenu());
                }
            }
            base.OnAppearing();
        }


        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (height<400)
            {
                Logo.IsVisible = false;
            }
            else
            {
                Logo.IsVisible = true;
            }
        }
    }
}

