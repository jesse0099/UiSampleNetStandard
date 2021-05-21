using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiSampleMigrat.Models;
using UiSampleMigrat.Api_Models;
using Xamarin.Forms;
using UiSampleMigrat.Helpers;

namespace UiSampleMigrat.ViewModels
{
    using Realms;
    using System.Windows.Input;
    using UiSampleMigrat.Views.Logins;

    public class ProfileViewModel : NotificationObject
    {
        #region Propiedades
        private ICommand _logOutCommand;

        public ICommand LogOutCommand
        {
            get { return _logOutCommand; }
            set
            {
                _logOutCommand = value;
                onPropertyChanged();
            }
        }

        public byte[]  LocalCharge{ get; set; }

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

            try
            {
                MyClient = new ClientProfile();
                //Obtencion de datos del perfil 
                ClientProfile profileInf = LoginViewModel.GetInstance().ClientProfile;
                if (profileInf != null)
                {


                    MyClient = profileInf;

                    this.NombreApellido = $"{MyClient.PrimerNombre} {MyClient.Apellido}";

                    this.LogOutCommand = new Command(LogOutCommandExecute);

                    _instance = this;
                }
                else
                {
                    //Datos locales
                    //Consulta a Realm
                    var r = Realm.GetInstance();
                    var realmQuery = r.All<RmbClientProfile>().First<RmbClientProfile>();
                    LocalCharge = realmQuery.ProfilePhoto;
                    ImageSource profileImageSource = FromBytesToImageSource(LocalCharge);

                    //Seteando datos del perfil
                    this.MyClient = new ClientProfile()
                    {
                        ProfileImage = profileImageSource,
                        Afiliado = realmQuery.Afiliado.Date,
                        Apellido = realmQuery.Apellido,
                        SegundoApellido = realmQuery.SegundoApellido,
                        Email = realmQuery.Email,
                        PrimerNombre = realmQuery.PrimerNombre,
                        SegundoNombre = realmQuery.SegundoNombre,
                    };

                    this.NombreApellido = $"{MyClient.PrimerNombre} {MyClient.Apellido}";

                    this.LogOutCommand = new Command(LogOutCommandExecute);

                    _instance = this;
                }
                //Cargando pantalla de actualizacion
                UpdateProfileViewModel.GetInstance().Nombres = $"{MyClient.PrimerNombre} {MyClient.SegundoNombre}";
                UpdateProfileViewModel.GetInstance().Apellidos = $"{MyClient.Apellido} {MyClient.SegundoApellido}";
                UpdateProfileViewModel.GetInstance().Profile.Email = $"{MyClient.Email}";
                UpdateProfileViewModel.GetInstance().Profile.ProfileImage = MyClient.ProfileImage;

            }
            catch (InvalidOperationException) {
                //Inicializacion con DB realm vacia 
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

        #region Metodos
        public static void DeleteAllClientsRealm(List<RmbClientProfile> objects)
        {
            var realm = Realm.GetInstance();
            using (var transaction = realm.BeginWrite())
            {
                foreach (var item in objects)
                    realm.Remove(item);
                transaction.Commit();
            }
        }
        public  void LogOutCommandExecute()
        {
            //Limpiar Settings
            Settings.AppSettingsClear();
            var r = Realm.GetInstance();
            //Realm List o f Objects
            List<RmbClientProfile> _realms = r.All<RmbClientProfile>().ToList();
            DeleteAllClientsRealm(_realms);
            Application.Current.MainPage = new NavigationPage(new PaginaGeraldLogin());
        }
        private ImageSource FromBytesToImageSource(byte[] rawBytes) {
            if(rawBytes.Length!=0)
                return ImageSource.FromStream(()=> new MemoryStream(rawBytes));
            else
                return ImageSource.FromFile("userF.png");
        }


        #endregion

    }
}