using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inventory.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
		public Login ()
		{
			InitializeComponent ();
            Background.Source = ImageSource.FromResource("Inventory.Image.background.jpg");
            
        }

        private void Login_Clicked(object sender, EventArgs e)
        {
            if (Username.Text.Equals("") || Password.Text.Equals(""))
            {
                DisplayAlert("Notice", "Please enter username and password","Okay");
                Error.Text = "Username and password can't be empty";
            }
            else
            {
                Navigation.PushAsync(new MainMenu());
            }
          
        }
    }
}