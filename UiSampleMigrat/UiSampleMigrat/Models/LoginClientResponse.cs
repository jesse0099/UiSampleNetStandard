using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiSampleMigrat.Models
{
    public class LoginClientResponse
    {
        public string unique_name { get; set; }
        public int nbf { get; set; }
        public int exp { get; set; }
        public int iat { get; set; }
        public string iss { get; set; }
        public string aud { get; set; }
        public string jti { get; set; }
    }
}
