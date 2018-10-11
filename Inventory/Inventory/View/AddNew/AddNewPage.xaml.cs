using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inventory.View.AddNew
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddNewPage : ContentPage
	{
		public AddNewPage ()
		{
			InitializeComponent ();
		}

        private void CameraButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("to bind to camera function", "New Item", "OK");
        }

    }
}