using System;
using Realms;

namespace UiSampleMigrat.Models { 
	
	public class RmbClientProfile : RealmObject {
        [PrimaryKey]
        public int ID{ get; set; }
        public byte[] ProfilePhoto { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string Apellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Email { get; set; }
        public DateTimeOffset Afiliado { get; set; }
    }

}