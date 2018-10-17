using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
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
	public partial class PopupSupplier : PopupPage
    {
		public PopupSupplier ()
		{
			InitializeComponent ();
		}

        private void Add_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true);
        }
    }
}