using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiSampleMigrat.Api_Models
{
    public class ApiClientCredentials
    {
        public int IdPersona { get; set; }
        public int IdClient { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}
