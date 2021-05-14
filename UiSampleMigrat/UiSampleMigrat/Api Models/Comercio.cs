using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiSampleMigrat.Api_Models
{
    public class Comercio
    {
        public int idComercio { get; set; }
        public string nombreComercio { get; set; }
        public object logo { get; set; }
        public string categoria { get; set; }
        public double estrellas { get; set; }
        public DateTime fechaAfiliacion { get; set; }
        public string descripcion { get; set; }
    }
}
