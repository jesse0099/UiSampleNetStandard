using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using UiSampleMigrat.Models;

namespace UiSampleMigrat.ViewModels
{
    public class RootExplorePageViewModel : NotificationObject
    {
        private ObservableCollection<Categoria> categoriaslist;

        public ObservableCollection<Categoria> CategoriasList
        {
            get { return categoriaslist; }
            set
            {
                categoriaslist = value;
               // onPropertyChanged();
            }

        }
        private Categoria categoriaSeleccionada;

        public Categoria CategoriaSeleccionada
        {
            get { return categoriaSeleccionada; }
            set { categoriaSeleccionada = value;
                onPropertyChanged();
            }
        }


        public RootExplorePageViewModel()
        {

            //Reconociendo seleccion
            PropertyChanged += RootExplorePageViewModel_PropertyChanged;
            //Valores de testeo
            CategoriasList = new ObservableCollection<Categoria>();
            CategoriasList.Add(new Categoria(){ NombreCategoria="Farmacias",Vendedores=89,Portada="Drugs.png"});
            CategoriasList.Add(new Categoria(){ NombreCategoria="Restaurantes",Vendedores=79,Portada="restaurant.png"});
            CategoriasList.Add(new Categoria(){ NombreCategoria="Electronicos",Vendedores=29,Portada="Elec.png"});

        }

        private void RootExplorePageViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
           if(e.PropertyName == nameof(CategoriaSeleccionada))
            {
                if (CategoriaSeleccionada != null)
                {
                    MessagingCenter.Send(this, "Goto");
                    CategoriaSeleccionada = null;
                }
            }
        }
    }
}
