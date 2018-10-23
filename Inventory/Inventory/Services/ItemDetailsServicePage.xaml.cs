using Inventory.Models.ItemDetail;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inventory.Services
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemDetailsServicePage : ContentPage
	{
        private string Url = "http://202.160.1.102:8083/api/getiteminfo/";
        public ItemDetailsServicePage(string id)
		{
			InitializeComponent();
            getInfo(id);
        }

        private async void getInfo(string id) {
            try
            {
                Url = Url + id;
                HttpClient client = new HttpClient();
                string token = Application.Current.Properties["Token"].ToString();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                var GetItem = await client.GetStringAsync(Url);
                var Item = JsonConvert.DeserializeObject<ItemDetail>(GetItem);
                ItemPic.Source = ImageSource.FromUri(new Uri(Item.Picture));
                SerialNum.Text = Item.SerialNo;
                CateGory.Text = Item.Category;
                EType.Text = Item.EquipmentType;
                ItemBrand.Text = Item.Brand;
                ItemModel.Text = Item.Model;
                ItemStatusSection.Title = "Item Status: " + Item.Status;
                issuedUser.Text = Item.StaffName;
                issuedUserUnit.Text = Item.Unit;
                issuedUserContactNo.Text = Item.ContactNo;
                Location.Text = Item.MainUnit + "-" + Item.Unit + "-" + Item.SubUnit + "-" + Item.Department;
            }
            catch (System.Net.WebException Err) {
                await DisplayAlert("Error","No connection to Server","Noticed");
                await Navigation.PopAsync();
            }
               
        }
    }
}
