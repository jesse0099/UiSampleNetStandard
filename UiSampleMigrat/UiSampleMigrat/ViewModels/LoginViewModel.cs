using Android.Widget;
using System;
using System.Windows.Input;
using UiSampleMigrat.Helpers;
using UiSampleMigrat.Models;
using UiSampleMigrat.Services;
using UiSampleMigrat.Views.Home;
using Xamarin.Forms;
using UiSampleMigrat.MyExceptions;

namespace UiSampleMigrat.ViewModels
{
    public class LoginViewModel : NotificationObject
    {

        #region Propiedades
        public LoginDao Dao { get; set; }

        public ClientProfileDao CProfileDao { get; set; }

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

        public ClientProfile ClientProfile { get; set; }

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
        #endregion


        #region Constructores
        public LoginViewModel()
        {
            this.IsEnabled = true;
            this.IsBusy = false;
            this.Dao = new LoginDao();
            this.CProfileDao = new ClientProfileDao();
            this.UserLoguin = new Login();
            LoginCommand = new Command(LoginCommandExecute);
            _instance = this;
        }
        #endregion

        #region Metodos  y eventos
        public async void LoginCommandExecute()
        {
            //Notificacion de ocupado (Bussy indicator activo)0
            try
            {
                EnablingVarsOnMainThread(false, true);
                Settings.IsRemembered = this.UserLoguin.RememberMe;

                var authresponse = await Dao.Auth(this.UserLoguin);
                var tempToken = Convert.ToString(authresponse.Result);
                Settings.SerializedToken = tempToken;

                CProfileDao.Login = this.UserLoguin;
                ClientProfile = await CProfileDao.Get(new ClientProfile());

                
                if (Settings.IsRemembered)
                        CProfileDao.RealmSave(ClientProfile);

                Settings.FullName = $"{ClientProfile.PrimerNombre} {ClientProfile.SegundoNombre} {ClientProfile.Apellido} {ClientProfile.SegundoApellido}";
                Settings.ClientUID = 1;
                Settings.SuccesfullPassword = userLogin.password;

                EnablingVarsOnMainThread(true, false);

                Application.Current.MainPage = new RootHomePage();
            }
            catch (LoginException lEx)
            {

                Commons.CustomizedToast(Android.Graphics.Color.White, Android.Graphics.Color.Black,
                    lEx.Message, ToastLength.Long, iconResource: "error64", textSize: 16);
            }
            catch (ClientProfileException cpEx) {
                Commons.CustomizedToast(Android.Graphics.Color.White, Android.Graphics.Color.Black,
                    cpEx.Message, ToastLength.Long, iconResource: "error64", textSize: 16);
            }
            catch (ConnectionException cEx) {
                Commons.CustomizedToast(Android.Graphics.Color.White, Android.Graphics.Color.Black,
                     cEx.Message, ToastLength.Long, iconResource: "error64", textSize: 16);
            }
            finally
            {
                EnablingVarsOnMainThread(true, false);
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