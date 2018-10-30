using Inventory.Models.LocationSearch;
using Inventory.Services;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inventory.View.SearchItem.LocationSearch
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PopupListitem : PopupPage
    {
		public PopupListitem (List<IssuedItem> list)
		{
            InitializeComponent();
            IssuedItemList.ItemsSource = list;
        }

        private void IssuedItemList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true);
            var item = (IssuedItem)e.SelectedItem;
            Navigation.PushAsync(new ItemDetailsServicePage(item.Id.ToString()));
        }
    }
}