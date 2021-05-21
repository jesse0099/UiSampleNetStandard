using Android.Widget;
using Realms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UiSampleMigrat.Helpers;
using UiSampleMigrat.Interfaces;
using UiSampleMigrat.Models;
using UiSampleMigrat.Api_Models;
using UiSampleMigrat.Services;
using UiSampleMigrat.Views.Home;
using Xamarin.Forms;

namespace UiSampleMigrat.ViewModels
{
    public class LoginViewModel : NotificationObject
    {

        #region Propiedades
        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                onPropertyChanged();
            }
        }

        private bool _isEnabled;

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                onPropertyChanged();
            }
        }

        public ApiPlainClientProfile ClientProfile { get; set; }

        private RestServiceConsumer proc;

        private Login userLogin;

        public Login UserLoguin
        {
            get { return userLogin; }
            set
            {
                userLogin = value;
                onPropertyChanged();
            }
        }

        private ICommand _loginCommand;

        public ICommand LoginCommand
        {
            get { return _loginCommand; }
            set
            {
                _loginCommand = value;
                onPropertyChanged();
            }
        }

        private Boolean _rememberMe;

        public Boolean RememberMe
        {
            get { return _rememberMe; }
            set
            {
                _rememberMe = value;
                onPropertyChanged();
            }
        }
        #endregion


        #region Constructores
        public LoginViewModel()
        {
            this.IsEnabled = true;
            this.IsBusy = false;
            this.UserLoguin = new Login();
            LoginCommand = new Command(LoginCommandExecute);
            proc = new RestServiceConsumer();
            _instance = this;
        }
        #endregion

        #region Metodos  y eventos
        public async void LoginCommandExecute()
        {
            //Notificacion de ocupado (Bussy indicator activo)
            EnablingVarsOnMainThread(false,true);

            proc = new RestServiceConsumer();
            //Revisar conexion a internet
            if (proc.CheckConnection().IsSuccesFull) {
                try
                {
                    //Intento de login
                    if (userLogin.password != null && userLogin.userName != null)
                    {
                        var controllerString = $"{Constantes.LOGINAUTH}{Constantes.LOGINAUTHUSERPAR}={userLogin.userName}&{Constantes.LOGINAUTHPASSPAR}={userLogin.password}";
                        var response = await proc.Get<string>(Constantes.BASEURL, Constantes.LOGINPREFIX, controllerString);

                        if (!response.IsSuccesFull)
                        {
                            if (response.Result == null)
                            {
                                //Credenciales incorrectas - Fallo en el Login
                                EnablingVarsOnMainThread(true, false);
                                await Application.Current.MainPage.DisplayAlert("Error!", "Credenciales Incorrectas", "OK");
                                return;
                            }
                        }

                        //Token y valor de Recuerdo
                        Settings.SerializedToken = Convert.ToString(response.Result);
                        Settings.IsRemembered = RememberMe;

                        //Informacion de perfil
                        var profileResponse = await ProfileInfo();
                        if (!profileResponse.IsSuccesFull)
                        {
                            EnablingVarsOnMainThread(true,false);
                            await Application.Current.MainPage.DisplayAlert("Error!", profileResponse.Message, "OK");
                            return;
                        }

                        ApiPlainClientProfile profileInfo = (ApiPlainClientProfile)profileResponse.Result;
                        this.ClientProfile = profileInfo;



                        #region Carga de datos a otros ViewModels
                        var profileImageBytes = Convert.FromBase64String(profileInfo.PP.ToString());
                        ImageSource profileImage;
                        if (profileImageBytes.Length != 0)
                            profileImage = ImageSource.FromStream(() => new MemoryStream(profileImageBytes));
                        else
                            profileImage = ImageSource.FromFile("userF.png");
                        #endregion

                        //Control de recuerdos
                        if (Settings.IsRemembered)
                        {
                            //Cargar perfil a BD local
                            var r = Realm.GetInstance();
                            try
                            {
                                r.Write(() => {
                                    r.Add(new RmbClientProfile()
                                    {
                                        ID = profileInfo.ID,
                                        ProfilePhoto = profileImageBytes,
                                        Afiliado = profileInfo.Afiliado,
                                        Apellido = profileInfo.Apellido,
                                        SegundoApellido = profileInfo.SegundoApellido,
                                        Email = profileInfo.Email,
                                        PrimerNombre = profileInfo.PrimerNombre,
                                        SegundoNombre = profileInfo.SegundoNombre,
                                    });
                                });
                            }
                            catch (Exception ex)
                            {
                                UpdateProfileViewModel.CustomizedToast(Android.Graphics.Color.White, Android.Graphics.Color.Black,
                                    ex.Message, ToastLength.Long, iconResource: "error64", textSize: 16);
                            }
                        }

                        //Settings
                        Settings.FullName = $"{profileInfo.PrimerNombre} {profileInfo.SegundoNombre} {profileInfo.Apellido} {profileInfo.SegundoApellido}";
                        Settings.ClientUID = profileInfo.ID;
                        Settings.SuccesfullPassword = userLogin.password;

                        EnablingVarsOnMainThread(true,false);
                        
                        Application.Current.MainPage = new RootHomePage();

                    }
                    else {
                        EnablingVarsOnMainThread(true, false);
                        await Application.Current.MainPage.DisplayAlert("Error!", "Todos los datos son obligatorios", "OK");
                    }
                }
                catch (Exception ex) { 
                    //Error de servicio no disponible  y otros 
                     EnablingVarsOnMainThread(true, false);
                     await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
                }
            }
            else {
                //Error de conexion
                EnablingVarsOnMainThread(true, false);
                await Application.Current.MainPage.DisplayAlert("Error!", "Conexion A Internet No Disponible", "OK");
            }
        }

        public async Task<Response> ProfileInfo() {
            Response returned = new Response();
            returned.IsSuccesFull = false;
            returned.Message = "Conexion No disponible";
            if (!proc.CheckConnection().IsSuccesFull)
                return returned;
            try
            {
                //Informacion de perfil
                var profileControllerString = $"{Constantes.CLIENTPROFILE}{Constantes.LOGINAUTHUSERPAR}={userLogin.userName}&{Constantes.LOGINAUTHPASSPAR}={userLogin.password}";
                returned = await proc.Get<ApiPlainClientProfile>(Constantes.BASEURL, Constantes.CLIENTPREFIX, profileControllerString, Settings.SerializedToken);
                return returned;
            }
            catch (Exception ex)
            {
                //Problemas de acceso al servicio
                returned.Message = "Problemas De Acceso al Servicio";
                return returned;
            }
        }

        private async void EnablingVarsOnMainThread(bool isenabled, bool isbusy) {
            await Device.InvokeOnMainThreadAsync(() => {
                IsBusy = isbusy;
                IsEnabled = isenabled;
            });
        }
        #endregion

        #region Singleton
        private static LoginViewModel _instance;
        public static LoginViewModel GetInstance()
        {
            if (_instance == null)
                return new LoginViewModel();
            else
                return _instance;

        }
        #endregion

    }
}