using Inventory.Models.AddPageModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
	public partial class ItemSearch : ContentPage
	{
        private const string Url = "http://202.160.1.102:8083";
        private const string UrlType = Url + "/api/Equipments/GetCategories";
        private HttpClient client = new HttpClient();
        ObservableCollection<Category> Category { get; set; } = new ObservableCollection<Category>();
        public ItemSearch ()
		{
			InitializeComponent ();
            string token = Application.Current.Properties["Token"].ToString();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var Categories = await client.GetStringAsync(UrlType);
            var listCategory = JsonConvert.DeserializeObject<ObservableCollection<Category>>(Categories);
            Category = new ObservableCollection<Category>(listCategory);


            TypePicker.ItemsSource = Category;
            TypePicker.ItemDisplayBinding = new Binding("Name");
            TypePicker.SelectedIndex = 0;
        }

        private void SearchButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}