using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiSampleMigrat.Models;
using UiSampleMigrat.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UiSampleMigrat.Views.Enterprises
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EnterprisePage : ContentPage , INotifyPropertyChanged
    {

        private Empresa _selectedEnterprise;

        public Empresa SelectedEnterprise
        {
            get { return _selectedEnterprise; }
            set { _selectedEnterprise = value; }
        }


        private EnterprisePageViewModel context;

        public EnterprisePageViewModel Context
        {
            get { return context; }
            set { context = value;
                OnPropertyChanged();
            }
        }


        public EnterprisePage(string category)
        {
            //Crear recurso en base a la categoria enviada

            Context = new EnterprisePageViewModel(category);

           
            InitializeComponent();

            this.BindingContext = Context;


            //Navegacion al inventario de la empresa seleccionada
            MessagingCenter.Subscribe<EnterprisePageViewModel>(this, "GotoInventario", (x) => {
                //Respuesta
                if (lstEnterprises.SelectedItem != null)
                    gotoInventario(((Empresa)lstEnterprises.SelectedItem).idEmpresa);
            });

        }

        private void SldStars_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            //var newStep = Math.Round(e.NewValue / 1.1);

            //sldStars.Value = newStep * 1.1;
        }

        private async void gotoInventario(int comercio)
        {
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushModalAsync(new UiSampleMigrat.Views.Enterprises.InventarioPage(comercio));
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            Context.filter(((SearchBar)sender).Text);
        }
    }
}