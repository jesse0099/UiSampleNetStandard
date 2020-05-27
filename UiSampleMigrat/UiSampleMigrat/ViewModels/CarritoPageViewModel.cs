using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiSampleMigrat.Models
{
    public class CarritoPageViewModel : NotificationObject
    {

        private static ObservableCollection<Item> items;

        public ObservableCollection<Item> Items
        {
            get { return items; }
            set { items = value;
                onPropertyChanged();
            }
        }

        public CarritoPageViewModel()
        {


            Items = new ObservableCollection<Item>();
            Items.Add(new Item() { Cantidad=1,NombreItem ="NAN" ,Producto = new Producto() { NombreProducto = "RTX 2080 WATERCOOLING",Precio =9000,Descripcion= "GPU dedicada NVIDIA" } });
            Items.Add(new Item() { Cantidad=1,NombreItem ="NAN" ,Producto = new Producto() { NombreProducto = "RTX 2080 TI",Precio =5000 ,Descripcion= "GPU dedicada NVIDIA" } });
            Items.Add(new Item() { Cantidad=1,NombreItem ="NAN" ,Producto = new Producto() { NombreProducto = "RTX 2080",Precio =3000,Descripcion="GPU dedicada NVIDIA" } });
            Items.Add(new Item() { Cantidad=1,NombreItem ="NAN" ,Producto = new Producto() { NombreProducto = "TITAN XP",Precio =2000 ,Descripcion= "GPU dedicada NVIDIA" } });
            Items.Add(new Item() { Cantidad=1,NombreItem ="NAN" ,Producto = new Producto() { NombreProducto = "GTX 1080",Precio =1000 ,Descripcion="GPU dedicada NVIDIA"} });
           // App.Database.SaveItemAsync(new TodoItem() {Name="C",Notes="Nota"});
           //var x = App.Database.GetItemsAsync();


        }
        

    }
}
