using System;


namespace UiSampleMigrat.Models
{
    using Xamarin.Forms;

    public class ClientProfile
    {
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string Apellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Email { get; set; }
        public ImageSource ProfileImage { get; set; }
        public DateTime Afiliado { get; set; }
    }
}