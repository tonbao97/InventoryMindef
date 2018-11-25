using Inventory.Models.UserSearch;
using Inventory.Services;
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
    public partial class UserSearch : ContentPage
    {
        List<Unit> UnitList = new List<Unit>();
        private const string Url = "https://ubd-fpt-inventory.azurewebsites.net";
        private const string UrlUnits = Url + "/api/GetUnits";
        private const string UrlSearch = Url + "/api/searchbyuser/";
        private HttpClient client = new HttpClient();
        public UserSearch()
        {
            InitializeComponent();
            string token = Application.Current.Properties["Token"].ToString();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            Id.Text = "";
            Name.Text = "";
        }

        public async void loadInfo() {
            try
            {
                var Units = await client.GetStringAsync(UrlUnits);
                var listUnit = JsonConvert.DeserializeObject<List<Unit>>(Units);
                UnitList = new List<Unit>(listUnit);
                UnitPicker.ItemsSource = UnitList;
                UnitPicker.ItemDisplayBinding = new Binding("Name");
                UnitPicker.SelectedIndex = 0;
            }
             catch (Exception Err)
            {
                //Loading.IsVisible = false;
                await DisplayAlert("Error", "No connection to server", "Noticed");
                await Navigation.PopAsync();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            loadInfo();
        }

        private async void Search(object sender, EventArgs e)
        {
                if (Name.Text.Equals("") && Id.Text.Equals(""))
                {
                   await DisplayAlert("Notice","Please input Staff Name or Staff Id","Ok");
                }
                else if (!Name.Text.Equals("") && Id.Text.Equals(""))
                {
                    SearchbyName();
                }
                else
                {
                    SearchById();
                }
        }


        public async void SearchbyName() {
            var res = await client.GetAsync(UrlSearch + "?keyword=" + Name.Text);
            if (res.IsSuccessStatusCode)
            {
                var text = res.Content.ReadAsStringAsync();
                var UserFound = JsonConvert.DeserializeObject<List<User>>(text.Result);
                foreach (var item in UserFound.ToList())
                {
                    if (!item.IssuedItems.Any())
                    {
                        UserFound.Remove(item);
                    }
                    if (!Id.Text.Equals(""))
                    {
                        if (!item.Unit.Equals(UnitPicker.Items[UnitPicker.SelectedIndex]) || !item.IdentityNo.Equals(Id.Text))
                        {
                            UserFound.Remove(item);
                        }
                    }
                    else
                    {
                        if (!item.Unit.Equals(UnitPicker.Items[UnitPicker.SelectedIndex]))
                        {
                            UserFound.Remove(item);
                        }
                    }
                }
                if (UserFound.Count > 0)
                {
                    UserList.ItemsSource = UserFound;
                }
                else
                {
                    await DisplayAlert("Notice", "No record found!", "Ok");
                    UserList.ItemsSource = UserFound;
                }
            }
            else
            {
                await DisplayAlert("Notice", "ERROR", "Ok");
            }
        }
    

        public async void SearchById() {
            var res = await client.GetAsync(UrlSearch);
            if (res.IsSuccessStatusCode)
            {
                var text = res.Content.ReadAsStringAsync();
                var UserFound = JsonConvert.DeserializeObject<List<User>>(text.Result);
                foreach (var item in UserFound.ToList())
                {
                    if (!item.IssuedItems.Any())
                    {
                        UserFound.Remove(item);
                    }
                    if (!item.IdentityNo.Equals(Id.Text))
                        {
                            UserFound.Remove(item);
                        }      
                }
                if (UserFound.Count > 0)
                {
                    UserList.ItemsSource = UserFound;
                }
                else
                {
                    await DisplayAlert("Notice", "No record found!", "Ok");
                    UserList.ItemsSource = UserFound;
                }
            }
            else
            {
                await DisplayAlert("Notice", "ERROR", "Ok");
            }
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var itemList = (Models.UserSearch.User)e.Item;
            var ItemsList = new SearchItem.UserSearchPopup.PopupUserIssuedList(itemList.IssuedItems);
            PopupNavigation.Instance.PushAsync(ItemsList);
        }
    }
}