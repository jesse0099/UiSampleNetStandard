using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiSampleMigrat.Api_Models;

namespace UiSampleMigrat.Models
{
    public class InventarioSucursal:NotificationObject
    {


        public ApiSucursal _sucursal;
        public ApiSucursal Sucursal
        {
            get { return _sucursal; }
            set { _sucursal = value;
                onPropertyChanged();
            }
        }

        private ObservableCollection<Producto> _productos;

        public ObservableCollection<Producto> Productos
        {
            get { return _productos; }
            set { _productos = value;
                onPropertyChanged();
            }
        }


        public InventarioSucursal()
        {
            this.Productos = new ObservableCollection<Producto>();
        }
    }
}
