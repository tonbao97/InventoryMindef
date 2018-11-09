using Inventory.Models.LocationSearch;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inventory.View.SearchItem
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchLocation : ContentPage
	{
        private HttpClient client = new HttpClient();
        private const string Url = "https://ubd-fpt-inventory.azurewebsites.net";
        private const string UrlUnits = Url + "/api/GetUnits";
        private const string UrlMainUnits = Url + "/api/GetMainUnits";
        private const string UrlSubUnit = Url + "/api/GetsubUnits";
        private const string UrlDepartment = Url + "/api/GetDepartments";
        List<MainUnit> MainUnitList = new List<MainUnit>();
        List<Unit> UnitList = new List<Unit>();
        List<SubUnit> SubUnitList = new List<SubUnit>();
        List<Department> DepartmentList = new List<Department>();
        List<Models.LocationSearch.Type> TypeList = new List<Models.LocationSearch.Type>();
        public SearchLocation ()
		{
			InitializeComponent ();
            string token = Application.Current.Properties["Token"].ToString();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        }
        protected override void OnAppearing()
        {
           
                base.OnAppearing();
                loadInfo();
        }

        public async void loadInfo()
        {
            try
            {
                Loading.IsVisible = true;
                var MainUnits = await client.GetStringAsync(UrlMainUnits);
                var listMainUnit = JsonConvert.DeserializeObject<List<MainUnit>>(MainUnits);
                MainUnitList = new List<MainUnit>(listMainUnit);
                MainUnitPicker.ItemsSource = MainUnitList;
                MainUnitPicker.ItemDisplayBinding = new Binding("Name");
                MainUnitPicker.SelectedIndex = 0;

                var Units = await client.GetStringAsync(UrlUnits);
                var listUnit = JsonConvert.DeserializeObject<List<Unit>>(Units);
                UnitList = new List<Unit>(listUnit);
                UnitPicker.ItemsSource = UnitList;
                UnitPicker.ItemDisplayBinding = new Binding("Name");
                UnitPicker.SelectedIndex = 0;

                var SubUnits = await client.GetStringAsync(UrlSubUnit);
                var listSubUnit = JsonConvert.DeserializeObject<List<SubUnit>>(SubUnits);
                SubUnitList = new List<SubUnit>(listSubUnit);
                SubUnitPicker.ItemsSource = SubUnitList;
                SubUnitPicker.ItemDisplayBinding = new Binding("Name");
                SubUnitPicker.SelectedIndex = 0;

                var Department = await client.GetStringAsync(UrlDepartment);
                var listDepartment = JsonConvert.DeserializeObject<List<Department>>(Department);
                DepartmentList = new List<Department>(listDepartment);
                DeparmentPicker.ItemsSource = DepartmentList;
                DeparmentPicker.ItemDisplayBinding = new Binding("Name");
                DeparmentPicker.SelectedIndex = 0;
                Loading.IsVisible = false;
            }
            catch (System.Net.WebException Err)
            {
                Loading.IsVisible = false;
               await DisplayAlert("Error", "No connection to server", "Noticed");
               await Navigation.PopAsync();
            }
            

        }

        private async void Search_Clicked(object sender, EventArgs e)
        {
            string UrlGet = Url + "/api/searchbylocation?MainUnitId=" + (MainUnitPicker.SelectedIndex + 1) +
                "&UnitId=" + (UnitPicker.SelectedIndex + 1) + "&SubUnitId=" + (SubUnitPicker.SelectedIndex + 1) +
                "&DepartmentId=" + (DeparmentPicker.SelectedIndex + 1);
            var Items = await client.GetStringAsync(UrlGet);
            var ItemTypeList = JsonConvert.DeserializeObject<List<Models.LocationSearch.Type>>(Items);
            TypeList = new List<Models.LocationSearch.Type>(ItemTypeList);
            if (TypeList.Count>0)
            {
                TypeListView.ItemsSource = TypeList;
            }
            else
            {
                TypeListView.ItemsSource = TypeList;
                await DisplayAlert("Notice","No Result Found","Ok");
            }
        }


        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var itemList = (Models.LocationSearch.Item)e.Item;
            var ItemsList = new SearchItem.LocationSearch.PopupListitem(itemList.ListItem);
            PopupNavigation.Instance.PushAsync(ItemsList);
            
        }
    }
}