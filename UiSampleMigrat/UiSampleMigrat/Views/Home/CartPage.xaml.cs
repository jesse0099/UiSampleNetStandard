using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UiSampleMigrat.Views.Home
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CartPage : ContentPage
	{
		public CartPage ()
		{
			InitializeComponent ();

            lstCarrito.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                if (e.Item == null) return;

                Task.Delay(500);

                // Deselect the item.
                if (sender is Xamarin.Forms.ListView lv) lv.SelectedItem = null;
            };
        }
	}
}