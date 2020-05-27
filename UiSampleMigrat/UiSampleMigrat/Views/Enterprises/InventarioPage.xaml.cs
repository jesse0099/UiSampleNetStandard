using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Enums;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using UiSampleMigrat.ViewModels;
using UiSampleMigrat.Views.PopUps;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UiSampleMigrat.Views.Enterprises
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InventarioPage : ContentPage
    {
        private InventarioPageViewModel context;

        public InventarioPageViewModel Context
        {
            get { return context; }
            set { context = value;

            }
        }

        public InventarioPage(int empresa)
        {
            //Context 
            Context = new InventarioPageViewModel(empresa);

            InitializeComponent();

            this.BindingContext = Context;

            lstEnterprises.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                if (e.Item == null) return;

                Task.Delay(500);

                // Deselect the item.
                if (sender is Xamarin.Forms.ListView lv) lv.SelectedItem = null;

            };

            //this.popupLoadingView.IsVisible = false;

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            
            var popupProperties = new TestPop();
            var scaleAnimation = new ScaleAnimation
            {
                PositionIn = MoveAnimationOptions.Right,
                PositionOut = MoveAnimationOptions.Left
            };
            await PopupNavigation.Instance.PushAsync(popupProperties);
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Filtro de inventario
            this.Context.filter(((SearchBar)sender).Text.ToLower());
        }
    }
}