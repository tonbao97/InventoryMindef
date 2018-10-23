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
	public partial class SearchItemOptionPage : TabbedPage
	{
		public SearchItemOptionPage()
		{
			InitializeComponent();

            #region // HardCoded for example only
            locationListView.ItemsSource = new List<CategoryGroup> // hardcoded this list as proof of concept, not binded dropdown to list displayed yet. Observable list should be in ViewModel
            {
                new CategoryGroup("All", "A")
                {
                    new Equipment { EquipmentId="001", SerialNo="11-111-1111", ItemCategory="Accesories", ItemType="CMOS Battery", Location="All", UserId="Aidee", DeliveryOrderNo="1111", DeliveryDate="01 JAN 2018", SupplierName="Aidee", CategoryID=01, BrandName="Acer", Model="11", Quantity=1, Picture="picture1"},
                    new Equipment { EquipmentId="002", SerialNo="12-112-1112", ItemCategory="Accesories", ItemType="FDFSD", Location="Technical", UserId="Bao",  DeliveryOrderNo="2222", DeliveryDate="02 FEB 2018", SupplierName="Tin", CategoryID=02, BrandName="Dell", Model="22", Quantity=2, Picture="picture2"},
                    new Equipment { EquipmentId="003", SerialNo="13-113-1113", ItemCategory="Accesories", ItemType="Hard Case Kit", Location="Application", UserId="Masdee",  DeliveryOrderNo="3333", DeliveryDate="03 FEB 2018", SupplierName="Masdey", CategoryID=03, BrandName="Apple", Model="33", Quantity=3, Picture="picture3"}
                },

                new CategoryGroup("Technical", "C")
                {
                    new Equipment { EquipmentId="004", SerialNo="14-114-1114", ItemCategory="Computer", ItemType="Desktop", Location="All", UserId="Aidee",  DeliveryOrderNo="4444", DeliveryDate="04 APR 2018", SupplierName="Aidee", CategoryID=04, BrandName="Mac", Model="44", Quantity=4, Picture="picture4"},
                    new Equipment { EquipmentId="005", SerialNo="15-115-1115", ItemCategory="Computer", ItemType="Notebook", Location="Technical", UserId="Bao",  DeliveryOrderNo="5555", DeliveryDate="05 MAY 2018", SupplierName="Tin", CategoryID=05, BrandName="Lenovo", Model="55", Quantity=5, Picture="picture5"},
                    new Equipment { EquipmentId="006", SerialNo="16-116-1116", ItemCategory="Computer", ItemType="Tablet", Location="Application", UserId="Masdee",  DeliveryOrderNo="6666", DeliveryDate="06 JUNE 2018", SupplierName="Masdey", CategoryID=06, BrandName="Samsung", Model="66", Quantity=6, Picture="picture6"}
                },

                new CategoryGroup("Application", "O")
                {
                    new Equipment { EquipmentId="007", SerialNo="17-117-1117", ItemCategory="Other Devices", ItemType="Printer", Location="All", UserId="Aidee",  DeliveryOrderNo="7777", DeliveryDate="07 JUL 2018", SupplierName="Aidee", CategoryID=07, BrandName="Lexmark", Model="77", Quantity=7, Picture="picture7"},
                    new Equipment { EquipmentId="008", SerialNo="18-118-1118", ItemCategory="Other Devices", ItemType="Scanner", Location="Technical", UserId="Bao",  DeliveryOrderNo="8888", DeliveryDate="08 AUG 2018", SupplierName="Tin", CategoryID=08, BrandName="Toshiba", Model="88", Quantity=8, Picture="picture8"},
                    new Equipment { EquipmentId="009", SerialNo="19-119-1119", ItemCategory="Other Devices", ItemType="Projector", Location="Application", UserId="Masdee",  DeliveryOrderNo="9999", DeliveryDate="09 SEP 2018", SupplierName="Masdey", CategoryID=09, BrandName="Kyocera", Model="99", Quantity=9, Picture="picture9"}
                }
            };

            //userListView.ItemsSource = new List<CategoryGroup> // hardcoded this list as proof of concept, not binded dropdown to list displayed yet. Observable list should be in ViewModel
            //{
            //    new CategoryGroup("Aidee", "A")
            //    {
            //        new Equipment { EquipmentId="001", SerialNo="11-111-1111", ItemCategory="Accesories", ItemType="CMOS Battery", Location="All", UserId="Aidee", DeliveryOrderNo="1111", DeliveryDate="01 JAN 2018", SupplierName="Aidee", CategoryID=01, BrandName="Acer", Model="11", Quantity=1, Picture="picture1"},
            //        new Equipment { EquipmentId="002", SerialNo="12-112-1112", ItemCategory="Accesories", ItemType="FDFSD", Location="Technical", UserId="Bao",  DeliveryOrderNo="2222", DeliveryDate="02 FEB 2018", SupplierName="Tin", CategoryID=02, BrandName="Dell", Model="22", Quantity=2, Picture="picture2"},
            //        new Equipment { EquipmentId="003", SerialNo="13-113-1113", ItemCategory="Accesories", ItemType="Hard Case Kit", Location="Application", UserId="Masdee",  DeliveryOrderNo="3333", DeliveryDate="03 FEB 2018", SupplierName="Masdey", CategoryID=03, BrandName="Apple", Model="33", Quantity=3, Picture="picture3"}
            //    },

            //    new CategoryGroup("Bao", "B")
            //    {
            //        new Equipment { EquipmentId="004", SerialNo="14-114-1114", ItemCategory="Computer", ItemType="Desktop", Location="All", UserId="Aidee",  DeliveryOrderNo="4444", DeliveryDate="04 APR 2018", SupplierName="Aidee", CategoryID=04, BrandName="Mac", Model="44", Quantity=4, Picture="picture4"},
            //        new Equipment { EquipmentId="005", SerialNo="15-115-1115", ItemCategory="Computer", ItemType="Notebook", Location="Technical", UserId="Bao",  DeliveryOrderNo="5555", DeliveryDate="05 MAY 2018", SupplierName="Tin", CategoryID=05, BrandName="Lenovo", Model="55", Quantity=5, Picture="picture5"},
            //        new Equipment { EquipmentId="006", SerialNo="16-116-1116", ItemCategory="Computer", ItemType="Tablet", Location="Application", UserId="Masdee",  DeliveryOrderNo="6666", DeliveryDate="06 JUNE 2018", SupplierName="Masdey", CategoryID=06, BrandName="Samsung", Model="66", Quantity=6, Picture="picture6"}
            //    },

            //    new CategoryGroup("Masdee", "M")
            //    {
            //        new Equipment { EquipmentId="007", SerialNo="17-117-1117", ItemCategory="Other Devices", ItemType="Printer", Location="All", UserId="Aidee",  DeliveryOrderNo="7777", DeliveryDate="07 JUL 2018", SupplierName="Aidee", CategoryID=07, BrandName="Lexmark", Model="77", Quantity=7, Picture="picture7"},
            //        new Equipment { EquipmentId="008", SerialNo="18-118-1118", ItemCategory="Other Devices", ItemType="Scanner", Location="Technical", UserId="Bao",  DeliveryOrderNo="8888", DeliveryDate="08 AUG 2018", SupplierName="Tin", CategoryID=08, BrandName="Toshiba", Model="88", Quantity=8, Picture="picture8"},
            //        new Equipment { EquipmentId="009", SerialNo="19-119-1119", ItemCategory="Other Devices", ItemType="Projector", Location="Application", UserId="Masdee",  DeliveryOrderNo="9999", DeliveryDate="09 SEP 2018", SupplierName="Masdey", CategoryID=09, BrandName="Kyocera", Model="99", Quantity=9, Picture="picture9"}
            //    }
            //};

            itemListView.ItemsSource = new List<CategoryGroup> // hardcoded this list as proof of concept, not binded dropdown to list displayed yet. Observable list should be in ViewModel
            {
                new CategoryGroup("Accessories", "A")
                {
                    new Equipment { EquipmentId="001", SerialNo="11-111-1111", ItemCategory="Accesories", ItemType="CMOS Battery", Location="All", UserId="Aidee", DeliveryOrderNo="1111", DeliveryDate="01 JAN 2018", SupplierName="Aidee", CategoryID=01, BrandName="Acer", Model="11", Quantity=1, Picture="picture1"},
                    new Equipment { EquipmentId="002", SerialNo="12-112-1112", ItemCategory="Accesories", ItemType="FDFSD", Location="Technical", UserId="Bao",  DeliveryOrderNo="2222", DeliveryDate="02 FEB 2018", SupplierName="Tin", CategoryID=02, BrandName="Dell", Model="22", Quantity=2, Picture="picture2"},
                    new Equipment { EquipmentId="003", SerialNo="13-113-1113", ItemCategory="Accesories", ItemType="Hard Case Kit", Location="Application", UserId="Masdee",  DeliveryOrderNo="3333", DeliveryDate="03 FEB 2018", SupplierName="Masdey", CategoryID=03, BrandName="Apple", Model="33", Quantity=3, Picture="picture3"}
                },

                new CategoryGroup("Computer", "C")
                {
                    new Equipment { EquipmentId="004", SerialNo="14-114-1114", ItemCategory="Computer", ItemType="Desktop", Location="All", UserId="Aidee",  DeliveryOrderNo="4444", DeliveryDate="04 APR 2018", SupplierName="Aidee", CategoryID=04, BrandName="Mac", Model="44", Quantity=4, Picture="picture4"},
                    new Equipment { EquipmentId="005", SerialNo="15-115-1115", ItemCategory="Computer", ItemType="Notebook", Location="Technical", UserId="Bao",  DeliveryOrderNo="5555", DeliveryDate="05 MAY 2018", SupplierName="Tin", CategoryID=05, BrandName="Lenovo", Model="55", Quantity=5, Picture="picture5"},
                    new Equipment { EquipmentId="006", SerialNo="16-116-1116", ItemCategory="Computer", ItemType="Tablet", Location="Application", UserId="Masdee",  DeliveryOrderNo="6666", DeliveryDate="06 JUNE 2018", SupplierName="Masdey", CategoryID=06, BrandName="Samsung", Model="66", Quantity=6, Picture="picture6"}
                },

                new CategoryGroup("Other Devices", "O")
                {
                    new Equipment { EquipmentId="007", SerialNo="17-117-1117", ItemCategory="Other Devices", ItemType="Printer", Location="All", UserId="Aidee",  DeliveryOrderNo="7777", DeliveryDate="07 JUL 2018", SupplierName="Aidee", CategoryID=07, BrandName="Lexmark", Model="77", Quantity=7, Picture="picture7"},
                    new Equipment { EquipmentId="008", SerialNo="18-118-1118", ItemCategory="Other Devices", ItemType="Scanner", Location="Technical", UserId="Bao",  DeliveryOrderNo="8888", DeliveryDate="08 AUG 2018", SupplierName="Tin", CategoryID=08, BrandName="Toshiba", Model="88", Quantity=8, Picture="picture8"},
                    new Equipment { EquipmentId="009", SerialNo="19-119-1119", ItemCategory="Other Devices", ItemType="Projector", Location="Application", UserId="Masdee",  DeliveryOrderNo="9999", DeliveryDate="09 SEP 2018", SupplierName="Masdey", CategoryID=09, BrandName="Kyocera", Model="99", Quantity=9, Picture="picture9"}
                }
            };
            #endregion 

            //This is to add count of itemsitemtype
            //int ItemTypeCount = 0;
            //foreach (Equipment ItemType in listView.ItemsSource)
            //{
            //    ItemTypeCount++;
            //}
        }

        async void ShowButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ItemsFilteredServicePage());
        }

        private void locationFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        async void itemListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new ItemsFilteredServicePage());
            // the idea here is that if they select a specific item type then the item filter page applies that filter to output that specific outcome
        }
    }
}