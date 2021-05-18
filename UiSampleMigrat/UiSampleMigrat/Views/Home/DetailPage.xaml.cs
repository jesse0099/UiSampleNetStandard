using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Enums;
using Rg.Plugins.Popup.Services;
using System;
using UiSampleMigrat.Views.PopUps;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using UiSampleMigrat.Models;
using System.Threading.Tasks;

namespace UiSampleMigrat.Views.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : ContentPage
    {

        private Orden selectedOrder;

        public Orden SelectedOrder
        {
            get { return selectedOrder;}
            set { selectedOrder = value; }
        }

        public DetailPage()
        {
            InitializeComponent();

            lstOrders.ItemTapped += (object sender, ItemTappedEventArgs e) => {
                // don't do anything if we just de-selected the row.
                if (e.Item == null) return;

                // Optionally pause a bit to allow the preselect hint.
                Task.Delay(500);

                selectedOrder = ((Orden)lstOrders.SelectedItem);
                // Deselect the item.
                if (sender is Xamarin.Forms.ListView lv) lv.SelectedItem = null;

            };
        }


        private async void BtnSeeDetail_Clicked(object sender, EventArgs e)
        {
            if (SelectedOrder != null)
            {
                var popupProperties = new PopUpOrderDetail(SelectedOrder.Items, SelectedOrder);
                var scaleAnimation = new ScaleAnimation
                {
                    PositionIn = MoveAnimationOptions.Right,
                    PositionOut = MoveAnimationOptions.Left
                };
                await PopupNavigation.Instance.PushAsync(popupProperties);
            }
        }
    }
}
