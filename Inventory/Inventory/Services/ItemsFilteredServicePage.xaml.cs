using Inventory.Models;
using Inventory.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inventory.Services
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemsFilteredServicePage : ContentPage
	{
		public ItemsFilteredServicePage ()
		{
			InitializeComponent ();

            filteredListView.ItemsSource = new List<Equipment>
            {
                new Equipment { EquipmentId="001", SerialNo="11-111-1111", ItemCategory="Accesories", ItemType="Connectors", Location="All"},
                new Equipment { EquipmentId="002", SerialNo="12-112-1112", ItemCategory="Accesories", ItemType="Projectors", Location="Technical"},
                new Equipment { EquipmentId="003", SerialNo="13-113-1113", ItemCategory="Accesories", ItemType="Acc. Etc.", Location="Application"}
            };

        }

        private void sortFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            // no handler made to apply filter yet
        }
    }
}