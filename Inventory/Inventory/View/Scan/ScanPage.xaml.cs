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
        public ScanPage()
        {
            InitializeComponent();
        }

        async void ScanButton_Clicked(object sender, EventArgs e)
        {
            var scan = new ZXingScannerPage();
            await Navigation.PushAsync(scan);
            scan.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                    ScannedCode.Text = result.Text;
                });
            };
        }

        async void ViewItemDetailsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ItemDetailsPage());
        }




        //private  async void Button_Clicked(object sender, EventArgs e)
        //{
        //    var scan = new ZXingScannerPage();
        //    await Navigation.PushAsync(scan);
        //    scan.OnScanResult += (result) =>
        //    {
        //            Device.BeginInvokeOnMainThread(async () =>
        //            {
        //                await Navigation.PopAsync();
        //                scannedCode.Text = result.Text;
        //            });
        //    };

        //}
    }
}