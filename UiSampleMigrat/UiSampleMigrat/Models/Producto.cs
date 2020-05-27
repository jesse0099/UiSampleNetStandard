using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiSampleMigrat.Api_Models;
using Xamarin.Forms;

namespace UiSampleMigrat.Models
{
    public class Producto 
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value;
            }
        }

        private int idSucursal;

        public int IdSucursal
        {
            get { return idSucursal; }
            set { idSucursal = value;

            }
        }

        private string _nombreSucursal;

        private ApiSucursal _sucursal;

        public ApiSucursal Sucursal
        {
            get { return _sucursal; }
            set { _sucursal = value; }
        }


        public string NombreSucursal
        {
            get { return _nombreSucursal; }
            set { _nombreSucursal = value;
            }
        }


        private string nombreProducto;

        public string NombreProducto
        {
            get { return nombreProducto; }
            set { nombreProducto = value;
            }
        }

        private string empresa;

        public string Empresa
        {
            get { return empresa; }
            set { empresa = value;
            }
        }

        private double precio;

        public double Precio
        {
            get { return precio; }
            set { precio = value;
            }
        }


        private int existencias;

        public int Existencias
        {
            get { return existencias; }
            set { existencias = value;
            }
        }

        private string descripcion;

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value;
            }
        }

        private DateTime fechVencimiento;

        public DateTime FechaVencimiento
        {
            get { return fechVencimiento; }
            set { fechVencimiento = value;
            }
        }

        private ImageSource ilustracion;

        public ImageSource Ilustracion
        {
            get { return ilustracion; }
            set { ilustracion = value;
            }
        }


    }
}
