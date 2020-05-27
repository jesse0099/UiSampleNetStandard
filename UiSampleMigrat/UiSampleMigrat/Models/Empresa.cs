using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace UiSampleMigrat.Models
{
    public class Empresa
    {

        public int idEmpresa { get; set; }

        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set
            {
                nombre = value;

            }
        }

        private string ilustracion;

        public string Ilustracion
        {
            get { return ilustracion; }
            set
            {
                ilustracion = value;

            }
        }

        private string descripcion;

        public string Descripcion
        {
            get { return descripcion; }
            set
            {
                descripcion = value;

            }
        }

        private DateTime fechaAfilacion;

        public DateTime FechaAfiliacion
        {
            get { return fechaAfilacion; }
            set
            {
                fechaAfilacion = value;

            }
        }

        private double estrellas;

        public double Estrellas
        {
            get { return estrellas; }
            set
            {
                estrellas = value;

            }
        }

        private string _categoria;

        public string Categoria
        {
            get { return _categoria; }
            set { _categoria = value; }
        }

        private ImageSource _logo;

        public ImageSource Logo
        {
            get { return _logo; }
            set { _logo = value; }
        }




    }
}