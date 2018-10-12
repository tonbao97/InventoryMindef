using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inventory.View.AddNew
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddNewPage : ContentPage
	{
		public AddNewPage ()
		{
			InitializeComponent();

        }

        private async void OnTapped (object sender, EventArgs e)
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
                DefaultCamera = CameraDevice.Front
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
    }
}