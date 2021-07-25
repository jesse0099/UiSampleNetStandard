using System;


namespace UiSampleMigrat.Models
{
    using UiSampleMigrat.Api_Models;
    using UiSampleMigrat.Helpers;
    using Xamarin.Forms;

    public class ClientProfile
    {
        public int ID { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string Apellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Email { get; set; }
        public ImageSource ProfileImage { get; set; }
        public DateTime Afiliado { get; set; }

        public ClientProfile()
        {

        }
        public ClientProfile(ApiPlainClientProfile result)
        {
            try
            {
                ID = result.ID;
                PrimerNombre = result.PrimerNombre;
                SegundoNombre = result.SegundoNombre;
                Apellido = result.Apellido;
                SegundoApellido = result.SegundoApellido;
                Email = result.Email;
                ProfileImage = Commons.ObjectToImageSource(result.PP);
                Afiliado = result.Afiliado;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}