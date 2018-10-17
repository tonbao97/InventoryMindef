using Rg.Plugins.Popup.Pages;
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
	public partial class PopupBrand : PopupPage
    {
		public PopupBrand ()
		{
			InitializeComponent ();
		}

        private void Add_Clicked(object sender, EventArgs e)
        {

        }
    }
}