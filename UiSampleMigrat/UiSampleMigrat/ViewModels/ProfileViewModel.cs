using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiSampleMigrat.Models;
using Xamarin.Forms;

namespace UiSampleMigrat.ViewModels
{
    public class ProfileViewModel : NotificationObject
    {
        #region Propiedades
        private ClientProfile _myClient;
        public ClientProfile MyClient
        {
            get { return _myClient; }
            set
            {
                _myClient = value;
                onPropertyChanged();
            }
        }
        private String _nombreApellido;
        public String NombreApellido
        {
            get { return _nombreApellido; }
            set
            {
                _nombreApellido = value;
                onPropertyChanged();
            }
        }

        #endregion

        #region Constructores
        public ProfileViewModel()
        {


            //Obtencion de datos del perfil 
            ApiClientProfile profileInf = LoginViewModel.GetInstance().ClientProfile;
            if (profileInf != null)
            {
                var profileImageBytes = Convert.FromBase64String(Convert.ToString(profileInf.PP));
                ImageSource profileImage;
                if (profileImageBytes.Length != 0)
                    profileImage = ImageSource.FromStream(() => new MemoryStream(profileImageBytes));
                else
                    profileImage = ImageSource.FromFile("userF.png");


                MyClient = new ClientProfile()
                {
                    ProfileImage = profileImage,
                    Apellido = profileInf.Apellido,
                    PrimerNombre = profileInf.PrimerNombre,
                    SegundoNombre = profileInf.SegundoNombre,
                    SegundoApellido = profileInf.SegundoApellido,
                    Email = profileInf.Email,
                    Afiliado = profileInf.Afiliado
                };

                this.NombreApellido = $"{MyClient.PrimerNombre} {MyClient.Apellido}";

                _instance = this;
            }
            else
            {
                //Datos locales
            }
        }
        #endregion

        #region Singleton
        private static ProfileViewModel _instance;
        public static ProfileViewModel GetInstance()
        {
            if (_instance == null)
                return new ProfileViewModel();
            else
                return _instance;
        }
        #endregion


    }
}