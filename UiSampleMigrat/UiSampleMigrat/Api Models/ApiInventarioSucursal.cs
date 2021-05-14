using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiSampleMigrat.Api_Models;

namespace UiSampleMigrat.Api_Models
{
    public class ApiInventarioSucursal
    {
        public ApiSucursal  Sucursal { get; set; }
        public List<ApiProducto> Productos { get; set; }
        public ApiInventarioSucursal()
        {
            this.Productos = new List<ApiProducto>();
        }
    }
}
