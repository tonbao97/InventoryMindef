using Inventory.Models;
using Inventory.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inventory.View.SearchItem
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchItemPage : ContentPage
	{
        public SearchItemPage()
        {
            InitializeComponent();

            listView.ItemsSource = new List<CategoryGroup> // hardcoded this list as proof of concept, not binded dropdown to list displayed yet. Observable list should be in ViewModel
            {
                new CategoryGroup("Accessories", "A")
                {
                    new Equipment { EquipmentId="001", SerialNo="11-111-1111", ItemCategory="Accesories", ItemType="Connectors", Location="All"},
                    new Equipment { EquipmentId="002", SerialNo="12-112-1112", ItemCategory="Accesories", ItemType="Projectors", Location="Technical"},
                    new Equipment { EquipmentId="003", SerialNo="13-113-1113", ItemCategory="Accesories", ItemType="Acc. Etc.", Location="Application"}
                },

                new CategoryGroup("Computer", "C")
                {
                    new Equipment { EquipmentId="004", SerialNo="14-114-1114", ItemCategory="Computer", ItemType="Desktop", Location="All"},
                    new Equipment { EquipmentId="005", SerialNo="15-115-1115", ItemCategory="Computer", ItemType="Laptop", Location="Technical"},
                    new Equipment { EquipmentId="006", SerialNo="16-116-1116", ItemCategory="Computer", ItemType="Comp. Etc.", Location="Application"}
                },

                new CategoryGroup("Other Devices", "O")
                {
                    new Equipment { EquipmentId="007", SerialNo="17-117-1117", ItemCategory="Other Devices", ItemType="Hard drives", Location="All"},
                    new Equipment { EquipmentId="008", SerialNo="18-118-1118", ItemCategory="Other Devices", ItemType="Printer", Location="Technical"},
                    new Equipment { EquipmentId="009", SerialNo="19-119-1119", ItemCategory="Other Devices", ItemType="Others Etc.", Location="Application"}
                }
            };
        }

        async void ShowButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ItemsFilteredPage());
        }

        private void locationFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}