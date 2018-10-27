using Firebase.Storage;
using Inventory.Models;
using Inventory.Models.AddPageModel;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inventory.View.AddNew
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddNewPage : ContentPage
	{


         ObservableCollection<Supplier> SupplierList { get; set; } = new ObservableCollection<Supplier>();
         ObservableCollection<Brand> Brands { get; set; } = new ObservableCollection<Brand>();
        ObservableCollection<EquipmentTypes> Equipmenttypes { get; set; } = new ObservableCollection<EquipmentTypes>();
        ObservableCollection<Category> Category { get; set; } = new ObservableCollection<Category>();
        private MediaFile file;

        private const string Url = "http://202.160.1.102:8083";
        private const string UrlSuppliers = Url + "/api/GetSuppliers";
        private const string UrlAdd = Url + "/api/Equipments/AddEquipment";
        private const string UrlBrands = Url + "/api/Equipments/GetBrands";
        private const string UrlEquipmenttypes = Url + "/api/GetEquipmenttypes";
        private const string UrlCategories = Url + "/api/Equipments/GetCategories";
        int SupIndex;
        int EquipmentTypeIndex;
        int CategoryIndex;
        int BrandIndex;


        private HttpClient client = new HttpClient();

		public AddNewPage()
		{
			InitializeComponent();
            string token = Application.Current.Properties["Token"].ToString();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                OnTapped();
            };

            TakenPicture.GestureRecognizers.Add(tapGestureRecognizer);
          
        }

        private async void OnTapped()
        {
            SupIndex = SupplierPicker.SelectedIndex;
            EquipmentTypeIndex = EquipmentTypePicker.SelectedIndex;
            CategoryIndex = CategoryPicker.SelectedIndex;
            BrandIndex = itemBrand.SelectedIndex;

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Test",
                SaveToAlbum = true,
                CompressionQuality = 75,
                CustomPhotoSize = 50,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 2000,
                DefaultCamera = CameraDevice.Rear
            });

            if (file == null)
                return;
            else
            {

                TakenPicture.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
            }
            SupplierPicker.SelectedIndex = SupIndex;
            EquipmentTypePicker.SelectedIndex = EquipmentTypeIndex;
            CategoryPicker.SelectedIndex = CategoryIndex;
            itemBrand.SelectedIndex = BrandIndex;
        }

        private void AddSupplier_Clicked(object sender, EventArgs e)
        {
            var SupplierAddPage = new PopupSupplier();
            SupplierAddPage.BindingContext = SupplierList;
            PopupNavigation.Instance.PushAsync(SupplierAddPage);
        }

        private void AddBrand_Clicked(object sender, EventArgs e)
        {

            var BrandAddPage = new PopupBrand();
            BrandAddPage.BindingContext = Brands;
            PopupNavigation.Instance.PushAsync(BrandAddPage);
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            Validate();
        }


        private void Validate() {

            if (DeliveryOrderNo == null || Model.Text == null || Quantity.Text == null)
            {
                DisplayAlert("Noticed","Please input all the needed info","Ok");
            }
            else if (file == null)
            {
                DisplayAlert("Noticed", "Please take picture of item", "Ok");
            }
            else
            {
                SendInfoToDatabse();
            }
        }

        private async void SendInfoToDatabse() {

            Loading.IsVisible = true;
            DateTime day = Date.Date;
            var DayChoose = String.Format("{0:yyyy/MM/dd}", day);


            var stroageImage = await new FirebaseStorage("fir-7f783.appspot.com")
                .Child("Pic")
                .Child(Model.Text)
                .PutAsync(file.GetStream());
            string imgurl = stroageImage;

            Item newItem = new Item(DeliveryOrderNo.Text, DayChoose, SupplierPicker.Items[SupplierPicker.SelectedIndex].ToString(), CategoryPicker.SelectedIndex, itemBrand.Items[itemBrand.SelectedIndex].ToString(), Model.Text, int.Parse(Quantity.Text), imgurl);


            var content = JsonConvert.SerializeObject(newItem);
            var res = client.PostAsync(UrlAdd, new StringContent(content, Encoding.UTF8, "application/json"));
            Loading.IsVisible = false;
            if (res.IsCompleted)
            {
                await DisplayAlert("Check", "Sending process is successful", "OK");
            }
            else
            {
                await DisplayAlert("Check", "Sending process is Failed", "OK");
            }
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                Loading.IsVisible = true;
                var Supplier = await client.GetStringAsync(UrlSuppliers);
                var listSupplier = JsonConvert.DeserializeObject<List<Supplier>>(Supplier);
                SupplierList = new ObservableCollection<Supplier>(listSupplier);

                SupplierPicker.ItemsSource = SupplierList;
                SupplierPicker.ItemDisplayBinding = new Binding("Name");


                var Brand = await client.GetStringAsync(UrlBrands);
                var listBrand = JsonConvert.DeserializeObject<List<Brand>>(Brand);
                Brands = new ObservableCollection<Brand>(listBrand);


                itemBrand.ItemsSource = Brands;
                itemBrand.ItemDisplayBinding = new Binding("Name");

                var Equipmenttype = await client.GetStringAsync(UrlEquipmenttypes);
                var listEquipmenttypes = JsonConvert.DeserializeObject<List<EquipmentTypes>>(Equipmenttype);
                Equipmenttypes = new ObservableCollection<EquipmentTypes>(listEquipmenttypes);


                EquipmentTypePicker.ItemsSource = Equipmenttypes;
                EquipmentTypePicker.ItemDisplayBinding = new Binding("Name");


                var Categories = await client.GetStringAsync(UrlCategories);
                var listCategory = JsonConvert.DeserializeObject<ObservableCollection<Category>>(Categories);
                Category = new ObservableCollection<Category>(listCategory);


                CategoryPicker.ItemsSource = Category;
                CategoryPicker.ItemDisplayBinding = new Binding("Name");


                SupplierPicker.SelectedIndex = 0;
                itemBrand.SelectedIndex = 0;
                CategoryPicker.SelectedIndex = 0;
                EquipmentTypePicker.SelectedIndex = 0;
                Loading.IsVisible = false;
            }
            catch (System.Net.WebException Err)
            {
                Loading.IsVisible = false;
                await DisplayAlert("Error", "No connection to server", "Noticed");
                await Navigation.PopAsync();
            }
        }

    }
}