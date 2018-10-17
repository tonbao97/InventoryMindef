using Inventory.Models;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
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

namespace Inventory.View.AddNew
{   
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddNewPage : ContentPage
	{


        private const string Url = "http://192.168.137.232:12345/api/Equipments/AddEquipment";
        private HttpClient client = new HttpClient();
        public AddNewPage ()
		{
			InitializeComponent();
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                OnTapped();
            };
            TakenPicture.GestureRecognizers.Add(tapGestureRecognizer);
        }

    


        private async void OnTapped ()
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
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

                //var stroageImage = await new FirebaseStorage("fir-7f783.appspot.com")
                //    .Child("Pic")
                //    .Child("image.jpg")
                //    .PutAsync(file.GetStream());
                //string imgurl = stroageImage;
                //Url.Text = imgurl;
            }
            await DisplayAlert("File Location", file.Path, "OK");
        }

        private void AddSupplier_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new PopupSupplier());
        }

        private void AddBrand_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new PopupBrand());
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            string token = Application.Current.Properties["Token"].ToString();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            DateTime day = Date.Date;
            var DayChoose = String.Format("{0:yyyy/MM/dd}", day);

            Item newItem = new Item(DeliveryOrderNo.Text, DayChoose, SupplierPicker.SelectedItem.ToString(), CategoryPicker.SelectedIndex, itemBrand.SelectedItem.ToString(), Model.Text, int.Parse(Quantity.Text), "");
            var content = JsonConvert.SerializeObject(newItem);
            var res = client.PostAsync(Url, new StringContent(content, Encoding.UTF8, "application/json"));
            DisplayAlert("Check", res.Result.ToString(), "OK");

        }
    }
}