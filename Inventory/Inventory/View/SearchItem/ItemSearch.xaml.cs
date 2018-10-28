using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inventory.View.SearchItem
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemSearch : ContentPage
	{
		public ItemSearch ()
		{
			InitializeComponent ();
		}

        private void SearchButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}