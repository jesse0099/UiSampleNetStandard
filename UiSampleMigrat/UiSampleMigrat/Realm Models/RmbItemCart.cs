using System;
using System.Collections.Generic;
using System.Text;

namespace UiSampleMigrat.Realm_Models
{
    using Realms;
    public class RmbItemCart : RealmObject
    {
        [PrimaryKey]
        public int ID { get; set; }
        public int Quantity { get; set; }
        public RmbProduct Product { get; set; }
        public RmbCart Cart { get; set; }
    }
}
