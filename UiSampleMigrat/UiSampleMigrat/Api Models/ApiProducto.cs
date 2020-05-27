using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiSampleMigrat.Api_Models
{
    public class ApiProducto
    {
        public int idProducto { get; set; }
        public int sucursal { get; set; }
        public string nombreProducto { get; set; }
        public int existencias { get; set; }
        public string descripcion { get; set; }
        public DateTime fechaVencimiento { get; set; }
        public object ilustracion { get; set; }
        public Decimal precio { get; set; }
        public bool isPromocion { get; set; }
    }
}
