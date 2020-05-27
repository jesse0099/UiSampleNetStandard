using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiSampleMigrat.Models
{
    public class Item 
    {

        private int _idCarrito;

        [PrimaryKey, AutoIncrement]
        public int IdCarrito
        {
            get { return _idCarrito; }
            set { _idCarrito = value; }
        }

        private int cantidad;

        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value;
            }
        }

        private string nombreItem;

        public string NombreItem
        {
            get { return nombreItem; }
            set { nombreItem = value;
            }
        }

        private Producto producto;

        public Producto Producto
        {
            get { return producto; }
            set { producto = value;
            }
        }



    }
}
