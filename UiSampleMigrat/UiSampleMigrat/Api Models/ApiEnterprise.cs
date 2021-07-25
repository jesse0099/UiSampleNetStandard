using System;
using UiSampleMigrat.Models;

namespace UiSampleMigrat.Api_Models
{
    public class ApiEnterprise
    {
        public int IdComercio { get; set; }
        public string NombreComercio { get; set; }
        public object Logo { get; set; }
        public string Categoria { get; set; }
        public double Estrellas { get; set; }
        public DateTime FechaAfiliacion { get; set; }
        public string Descripcion { get; set; }
    }
}
