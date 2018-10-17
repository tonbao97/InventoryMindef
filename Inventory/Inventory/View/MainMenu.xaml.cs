using Inventory.View.AddNew;
using Inventory.View.SearchItem;
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
	public partial class MainMenu : ContentPage
	{
		public MainMenu ()
		{
			InitializeComponent ();
            Background.Source = ImageSource.FromResource("Inventory.Image.background.jpg");
        }

        async void AddButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddNewPage());
        }

        async void SearchButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchItemPage());
        }

        private void ScanButton_Clicked(object sender, EventArgs e)
        {

        }

        private void Logout_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties["Token"] = null;
            Application.Current.Properties["Name"] = null;
            Application.Current.Properties["Type"] = null;
            Navigation.PopAsync();
        }
    }
}