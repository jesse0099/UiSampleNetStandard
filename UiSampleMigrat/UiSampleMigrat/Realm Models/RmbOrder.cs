using System;
using System.Collections.Generic;
using System.Text;

namespace UiSampleMigrat.Realm_Models
{
    using Realms;
    public class RmbOrder : RealmObject
    {
        [PrimaryKey]
        public int ID { get; set; }
        public int State { get; set; }
        public float ShippingPrice { get; set; }
        public float GrandTotal { get; set; }
        public RmbCart Cart { get; set; }

    }
}
