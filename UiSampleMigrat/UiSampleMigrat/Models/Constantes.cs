using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiSampleMigrat.Models
{
    public class Constantes
    {
        #region Acceso a la Api
        //public  static  string BASEURL = "https://eecommerapi.conveyor.cloud/";
        //public const string BASEURL = "http://192.168.100.33:45455/";
        public const string BASEURL = "http://192.168.100.207:45455/";
        public const string COMMEPREFIX = "/api/comme";
        public const string COMMEGETBYCAT = "/getByCat?category=";
        public const string COMMEGETSUCBYCOMME = "/getSucByCommer?idCommer=";

        public const string PRODUCTSPREFIX = "/api/product";
        public const string PRODUCTGETBYCOMME = "/getByCommer?idCommer=";

        public const string LOGINPREFIX = "api/login";
        public const string LOGINAUTH = "/loginClient?";
        public const string LOGINAUTHUSERPAR = "user";
        public const string LOGINAUTHPASSPAR = "password";

        public const string CLIENTPREFIX = "api/clients";
        public const string CLIENTPROFILE = "/profileInfoByAuth?";
        public const string CLIENTUPDATEPROFILE = "/UpdateClientProfile";
        public const string CLIENTUPDATECREDENTIALS = "/UpdateClientCredentials";

        #endregion


    }
}