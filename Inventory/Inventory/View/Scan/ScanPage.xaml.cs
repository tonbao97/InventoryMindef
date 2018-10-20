using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Common;
using ZXing.Net.Mobile.Forms;
using ZXing.QrCode;
using ZXing.Mobile;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Inventory.Services;

namespace Inventory.View.Scan
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanPage : ContentPage
    {
        bool Scanned = false;
        public string ScanMessage { get; set; }

        public ScanPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // ScanQr();
            PictureTaken.Source = ImageSource.FromUri(new Uri("https://firebasestorage.googleapis.com/v0/b/fir-7f783.appspot.com/o/Pic%2Fimage.jpg?alt=media&token=6ecba52d-f686-49d7-a288-7145b7baa5e3"));
        }

        // The ZXing UI doesnt handle back navigation well if no barcode or qr code is scanned
        //private void ScanQr() {
        //    if (!Scanned)
        //    {
        //        var scan = new ZXingScannerPage();
        //        Navigation.PushAsync(scan);
        //        scan.OnScanResult += (result) =>
        //        {
        //            Device.BeginInvokeOnMainThread(async () =>
        //            {
        //                ScannedCode.Text = result.Text;
        //                Scanned = true;
        //                await Navigation.PopAsync();
        //            });
        //        };
        //    }
        //}

        async void ViewItemDetailsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ItemDetailsServicePage());
        }


        private async void ScanButton_Clicked(object sender, EventArgs e)
        {
            var scan = new ZXingScannerPage();
            await Navigation.PushAsync(scan);
            scan.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                    ScannedCode.Text = result.Text;

                    if (ScannedCode.Text == null)
                    {
                        ItemDetailsButton.IsEnabled = false;
                        ItemDetailsButton.IsVisible = false;
                        // should also show the default text "Please scan the item barcode..." on the UI
                        
                    }
                    else
                    {
                        ItemDetailsButton.IsEnabled = true;
                        ItemDetailsButton.IsVisible = true;

                        // This bindable context is only a proof of concept
                        ScanMessage = "EquipmentId, SerialNo, ItemCategory, ItemType, Location, IssuedUser";
                        BindingContext = this;

                        // at this point the ScanMessage should show the item and the message on the UI should show;
                        // EquipmentId
                        // SerialNo
                        // ItemCategory
                        // ItemType
                        // Location
                        // IssuedUser <--not yet has a data property
                    }
                });
            };
        
        }
    }
}