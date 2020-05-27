using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiSampleMigrat.Api_Models;

namespace UiSampleMigrat.Models
{
    public class GroupedProducts: ObservableCollection<Producto>
    {
        public ApiSucursal NombreSucursal { get; set; }

        public GroupedProducts(ApiSucursal nombreSucursal)
            : base()
        {
            NombreSucursal = nombreSucursal;
        }

        public GroupedProducts(ApiSucursal nombreSucursal, IEnumerable<Producto> source)
            : base(source)
        {
            NombreSucursal = nombreSucursal;
        }

    }
}
