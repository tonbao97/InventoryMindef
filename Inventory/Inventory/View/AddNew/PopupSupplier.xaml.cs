using Inventory.Models.AddPageModel;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            if (!Supplier.Text.Equals(""))
            {
                var SupplierList = (ObservableCollection<Supplier>)BindingContext;
                Supplier newSupplier = new Supplier(Supplier.Text, null, null, null, null, null, null, null, null, null, null, true, SupplierList.Count + 1);
                SupplierList.Add(newSupplier);
                PopupNavigation.Instance.PopAsync(true);
            }
            else
            {
                DisplayAlert("Notice","Please input supplier","ok");
            }
        }
    }
}