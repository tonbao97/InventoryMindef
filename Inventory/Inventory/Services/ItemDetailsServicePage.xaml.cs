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
        private string Url = "https://ubd-fpt-inventory.azurewebsites.net/api/getiteminfo/";
        public ItemDetailsServicePage(string id)
		{
			InitializeComponent();
            getInfo(id);
            this.Title = "Item Id " + id + "'s info:";
        }

        private async void getInfo(string id) {
            try
            {
                Url = Url + id;
                Loading.IsVisible = true;
                HttpClient client = new HttpClient();
                string token = Application.Current.Properties["Token"].ToString();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                var GetItem = await client.GetStringAsync(Url);
                var Item = JsonConvert.DeserializeObject<ItemDetail>(GetItem);

                ItemPic.Source = ImageSource.FromUri(new Uri(Item.Picture));
                EquipID.Text = id;
                SerialNum.Text = Item.SerialNo;
                CateGory.Text = Item.Category;
                EType.Text = Item.EquipmentType;
                ItemBrand.Text = Item.Brand;
                ItemModel.Text = Item.Model;
                ItemProcessor.Text = Item.Processor;
                ItemRam.Text = Item.RAM;
                ItemHDD.Text = Item.HDD;
                ItemVGA.Text = Item.VGA;
                ItemStatusSection.Title = "Item Status: " + Item.Status;
                issuedUser.Text = Item.StaffName;
                issuedUserUnit.Text = Item.Unit;
                issuedUserContactNo.Text = Item.ContactNo;
                Location.Text = Item.MainUnit + "-" + Item.Unit + "-" + Item.SubUnit + "-" + Item.Department;
                Loading.IsVisible = false;
            }
            catch (Exception Err)
            {
                if (Err.ToString().Contains("404"))
                {
                    await DisplayAlert("Error", "Item Not Found", "Noticed");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "No connection to server", "Noticed");
                    await Navigation.PopAsync();
                }
                
            }
               
        }
    }
}
