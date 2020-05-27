using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiSampleMigrat.Models
{
    public class Orden : NotificationObject
    {
        private string  vendedor;

        public string  Vendedor
        {
            get { return vendedor; }
            set { vendedor = value;
                onPropertyChanged();
            }
        }

        private DateTime ordenada;

        public DateTime Ordenada
        {
            get { return ordenada; }
            set { ordenada = value;
                onPropertyChanged();
            }
        }

        private int id;

        public int ID
        {
            get { return id; }
            set
            {
                id = value;
                onPropertyChanged();
            }
        }

        private double subTotal;

        public double SubTotal
        {
            get { return subTotal; }
            set
            {
                subTotal = value;
                onPropertyChanged();
            }
        }

        private double costoEnvio;

        public double CostoEnvio
        {
            get { return costoEnvio; }
            set
            {
                costoEnvio = value;
                onPropertyChanged();
            }
        }


        private double total;

        public double Total
        {
            get { return total; }
            set { total = value;
                onPropertyChanged();
            }
        }


        private ObservableCollection<Item> items;

        public ObservableCollection<Item> Items
        {
            get { return items; }
            set { items = value;
                onPropertyChanged();
            }
        }




        public Orden()
        {
            //Leer el id del cliente desde la coleccion de propiedades Application
            //Y hacer una query bien vergas 
            Items = new ObservableCollection<Item>();
            Items.Add(new Item() { NombreItem = "Coca - cola", Cantidad = 1,Producto = new Producto() { Descripcion="Coca-cola 355ml",NombreProducto="Coca-cola"} });
            Items.Add(new Item() { NombreItem = "Nachos", Cantidad = 1, Producto = new Producto() { Descripcion = "Nachos",NombreProducto ="Nachos" } });

            this.SubTotal = Total - (Total * 0.15);
            this.CostoEnvio = 0.0;
        }



    }
}
