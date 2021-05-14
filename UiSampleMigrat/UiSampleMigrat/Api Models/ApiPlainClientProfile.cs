using System;
using System.Collections.Generic;
using System.Text;

namespace UiSampleMigrat.Api_Models
{
    public class ApiPlainClientProfile
    {
        public int ID { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string Apellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Email { get; set; }
        public object PP { get; set; }
        public DateTime Afiliado { get; set; }
    }
}
