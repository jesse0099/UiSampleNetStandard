using System;
using System.Collections.Generic;
using System.Text;

namespace UiSampleMigrat.Realm_Models
{
    using Realms;
    public class RmbCart : RealmObject
    {
        [PrimaryKey]
        public int ID { get; set; }
        public IList<RmbItemCart> Items { get;}

    }
}
