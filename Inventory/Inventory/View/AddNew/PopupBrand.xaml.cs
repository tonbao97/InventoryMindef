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
	public partial class PopupBrand : PopupPage
    {
		public PopupBrand ()
		{
			InitializeComponent ();
		}

        private void Add_Clicked(object sender, EventArgs e)
        {
            if (!Brand.Text.Equals(""))
            {
                var Brands = (ObservableCollection<Brand>)BindingContext;
                Brand NewBrand = new Brand(Brand.Text, null, null, null, null, true, Brands.Count + 1);
                Brands.Add(NewBrand);
                PopupNavigation.Instance.PopAsync(true);
            }
            else
            {
                DisplayAlert("Notice", "Please input brand", "ok");
            }
        }
    }
}