using System;
using System.Collections.Generic;
using System.Text;

namespace UiSampleMigrat.Services
{
    public class BaseDao
    {
        protected RestServiceConsumer proc { get; set; }
        public BaseDao()
        {
            this.proc = new RestServiceConsumer();
        }
    }
}
