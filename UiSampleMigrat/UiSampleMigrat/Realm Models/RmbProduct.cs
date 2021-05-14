using System;
using System.Collections.Generic;
using System.Text;

namespace UiSampleMigrat.Realm_Models
{
    using Realms;
    public class RmbProduct : RealmObject
    {
        [PrimaryKey]
        public int ID { get; set; }
        public int IDSucursal { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float UnitPrice { get; set; }
        public DateTimeOffset Expire { get; set; }
    }
}
