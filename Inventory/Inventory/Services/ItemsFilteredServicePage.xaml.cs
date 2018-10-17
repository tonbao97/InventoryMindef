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
		public ItemsFilteredServicePage()
		{
			InitializeComponent();

            filteredListView.ItemsSource = new List<Equipment>
            {
                new Equipment { EquipmentId="001", SerialNo="11-111-1111", ItemCategory="Accesories", ItemType="CMOS Battery", Location="All", DeliveryOrderNo="1111", DeliveryDate="01 JAN 2018", SupplierName="Aidee", CategoryID=01, BrandName="Acer", Model="11", Quantity=1, Picture="picture1"},
                new Equipment { EquipmentId="002", SerialNo="12-112-1112", ItemCategory="Accesories", ItemType="FDFSD", Location="Technical", DeliveryOrderNo="2222", DeliveryDate="02 FEB 2018", SupplierName="Tin", CategoryID=02, BrandName="Dell", Model="22", Quantity=2, Picture="picture2"},
                new Equipment { EquipmentId="003", SerialNo="13-113-1113", ItemCategory="Accesories", ItemType="Hard Case Kit", Location="Application", DeliveryOrderNo="3333", DeliveryDate="03 FEB 2018", SupplierName="Masdey", CategoryID=03, BrandName="Apple", Model="33", Quantity=3, Picture="picture3"}
            };
        }

        private void sortFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            // no handler made to apply filter yet
        }
    }
}