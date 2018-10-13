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
        private const string Url = "http://192.168.1.10:12345/Token";

        private HttpClient client = new HttpClient();
        public Login ()
		{
			InitializeComponent ();
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
            //    //var req = new HttpRequestMessage(HttpMethod.Post, Url) { Content = new FormUrlEncodedContent(dict) };
            //    var response = await client.PostAsync(Url, new FormUrlEncodedContent(dict));
            //    var text = response.Content.ReadAsStringAsync();
            //    await DisplayAlert("Test", text.Result,"Ok");
            //    //var listfruits = JsonConvert.DeserializeObject<Token>();


            //}
            await Navigation.PushAsync(new MainMenu());
        }
    }
}