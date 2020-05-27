﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UiSampleMigrat.Helpers;
using UiSampleMigrat.Interfaces;
using UiSampleMigrat.Models;
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

        private ApiClientProfile _clientProfile;

        public ApiClientProfile ClientProfile
        {
            get { return _clientProfile; }
            set { _clientProfile = value; }
        }


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
            _instance = this;
        }
        #endregion

        #region Metodos  y eventos
        public async void LoginCommandExecute()
        {
            await Device.InvokeOnMainThreadAsync(() => {
                this.IsBusy = true;
                this.IsEnabled = false;
            });
            /*Logueandome y obteniendo un token*/
            if (userLogin.password != null && userLogin.userName != null)
            {
                proc = new RestServiceConsumer();
                var controllerString = $"{Constantes.LOGINAUTH}{Constantes.LOGINAUTHUSERPAR}={userLogin.userName}&{Constantes.LOGINAUTHPASSPAR}={userLogin.password}";
                var response = await proc.Get<string>(Constantes.BASEURL, Constantes.LOGINPREFIX, controllerString);

                if (!response.IsSuccesFull)
                {
                    //Errores en la respuesta
                    if (response.Result == null)
                    {
                        await Device.InvokeOnMainThreadAsync(() => {
                            this.IsBusy = false;
                            this.IsEnabled = true;
                        });
                        await Application.Current.MainPage.DisplayAlert("Error!", "Credenciales incorrectas", "OK");
                        return;
                    }
                    //Manejo de otros errores
                    await Device.InvokeOnMainThreadAsync(() => {
                        this.IsBusy = false;
                        this.IsEnabled = true;
                    });
                    await Application.Current.MainPage.DisplayAlert("Error!", response.Message, "OK");
                    return;
                }
                //Settings
                Settings.SerializedToken = Convert.ToString(response.Result);
                Settings.IsRemembered = RememberMe;

                //Informacion de perdil
                var profileControllerString = $"{Constantes.CLIENTPROFILE}{Constantes.LOGINAUTHUSERPAR}={userLogin.userName}&{Constantes.LOGINAUTHPASSPAR}={userLogin.password}";
                var profileResponse = await proc.Get<ApiClientProfile>(Constantes.BASEURL, Constantes.CLIENTPREFIX, profileControllerString, Settings.SerializedToken);
                if (!profileResponse.IsSuccesFull)
                {
                    await Device.InvokeOnMainThreadAsync(() => {
                        this.IsBusy = false;
                        this.IsEnabled = true;
                    });
                    await Application.Current.MainPage.DisplayAlert("Error!", response.Message, "OK");
                    return;
                }
                ApiClientProfile profileInfo = (ApiClientProfile)profileResponse.Result;
                this.ClientProfile = profileInfo;
                
                #region Carga de datos a otros ViewModels
                UpdateProfileViewModel.GetInstance().Nombres = $"{profileInfo.PrimerNombre} {profileInfo.SegundoNombre}";
                UpdateProfileViewModel.GetInstance().Apellidos = $"{profileInfo.Apellido} {profileInfo.SegundoApellido}";
                UpdateProfileViewModel.GetInstance().Profile.Email = $"{profileInfo.Email}";

                var profileImageBytes = Convert.FromBase64String(Convert.ToString(profileInfo.PP));
                ImageSource profileImage;
                if (profileImageBytes.Length != 0)
                    profileImage = ImageSource.FromStream(() => new MemoryStream(profileImageBytes));
                else
                    profileImage = ImageSource.FromFile("userF.png");
                UpdateProfileViewModel.GetInstance().Profile.ProfileImage = profileImage;
                #endregion

                //Settings
                Settings.FullName = $"{profileInfo.PrimerNombre} {profileInfo.SegundoNombre} {profileInfo.Apellido} {profileInfo.SegundoApellido}";
                Settings.ClientUID = profileInfo.ID;
                Settings.SuccesfullPassword = userLogin.password;

                await Device.InvokeOnMainThreadAsync(() => {
                    this.IsBusy = false;
                    this.IsEnabled = true;
                });
                //Navegacion a la pagina Root bloqueando el regreso al Login
                Application.Current.MainPage = new RootHomePage();

            }
            else
            {
                await Device.InvokeOnMainThreadAsync(() => {
                    this.IsBusy = false;
                    this.IsEnabled = true;
                });
                await Application.Current.MainPage.DisplayAlert("Error!", "Todos los datos son obligatorios", "OK");
            }
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