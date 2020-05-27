using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiSampleMigrat.Models
{
    public class Categoria : NotificationObject
    {
        private string nombreCategoria;

        public string NombreCategoria
        {
            get { return nombreCategoria; }
            set {
                nombreCategoria = value;
                onPropertyChanged();
            }
        }

        private int vendedores;

        public int Vendedores
        {
            get { return vendedores; }
            set {
                vendedores = value;
                onPropertyChanged();
            }
        }

        private string portada;

        public string Portada
        {
            get { return portada; }
            set { portada = value;
                onPropertyChanged();
            }
        }




    }
}
