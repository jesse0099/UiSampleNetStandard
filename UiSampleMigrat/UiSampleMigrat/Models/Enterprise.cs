using System;
using UiSampleMigrat.Api_Models;
using UiSampleMigrat.Helpers;
using Xamarin.Forms;

namespace UiSampleMigrat.Models
{
    public class Enterprise
    {
        public int Id { get; set; }
        public string Nombre{ get; set; }
        public ImageSource Logo { get; set; }
        public Categoria Categoria{ get; set; }
        public double Estrellas { get; set; }
        public DateTime FechaAfiliacion { get; set; }
        public string Descripcion { get; set; }
        public Enterprise()
        {

        }

        public Enterprise(ApiEnterprise result)
        {
            try
            {
                Id = result.IdComercio;
                Nombre = result.NombreComercio;
                Descripcion = result.Descripcion.Trim('\r','\n');
                Logo = Commons.ObjectToImageSource(result.Logo);
                Categoria = new Categoria(){ Nombre = result. Categoria};
                Estrellas = result.Estrellas;
                FechaAfiliacion = result.FechaAfiliacion;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
