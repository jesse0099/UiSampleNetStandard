using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UiSampleMigrat.Models;
using UiSampleMigrat.Services;
using Xamarin.Forms;
using UiSampleMigrat.Helpers;
using UiSampleMigrat.Api_Models;
using Android.Widget;
using Android.Graphics;
using Android;
using UiSampleMigrat.ViewModels;

namespace UiSampleMigrat.ViewModels
{
    using Realms;
    public class UpdateProfileViewModel : NotificationObject 
    {

        #region Propiedades
        public byte[] ProfileImageBytes { get; set; }

        private string _repeatPassword;

        public string RepeatPassword
        {
            get { return _repeatPassword; }
            set { _repeatPassword = value;
                onPropertyChanged();
            }
        }


        private ICommand _updateCommand;

        public ICommand UpdateCommand
        {
            get { return _updateCommand; }
            set { _updateCommand = value;
                onPropertyChanged();
            }
        }


        private string _oldPassword;

        public string OldPassword
        {
            get { return _oldPassword; }
            set { _oldPassword = value;
                onPropertyChanged();
            }
        }

        private string _newPassword;

        public string NewPassword
        {
            get { return _newPassword; }
            set { _newPassword = value;
                onPropertyChanged();
            }
        }



        private string _nombres;

        public string Nombres
        {
            get { return _nombres; }
            set { _nombres = value;
                onPropertyChanged();
            }
        }

        private string  _apellidos;

        public string  Apellidos
        {
            get { return _apellidos; }
            set { _apellidos = value;
                onPropertyChanged();
            }
        }


        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value;
                onPropertyChanged();
            }
        }

        private bool _isVisible;

        public bool IsVisible
        {
            get { return _isVisible; }
            set { _isVisible = value;
                onPropertyChanged();
            }
        }


        private ClientProfile _profile;

        public ClientProfile Profile
        {
            get { return _profile; }
            set { _profile = value;
                onPropertyChanged();
            }
        }


        #endregion

        #region Constructores
        public UpdateProfileViewModel() {
            this.UpdateCommand = new Command(UpdateCommandExecute);
            this.Profile = new ClientProfile();
            EmptyStringInitializer();
            _instance = this;
        }
        #endregion

        #region Metodos
        public async void UpdateCommandExecute() {
            bool updateCredentials = false;
            RestServiceConsumer service;
            SetActivity(true);
            //Conexion
            service  = new RestServiceConsumer();
            var response = service.CheckConnection();
            if (!response.IsSuccesFull)
            {
                SetActivity(false);
                //No hay conexion
                CustomizedToast(Android.Graphics.Color.White, Android.Graphics.Color.Black,
                response.Message, iconResource: "error64", textSize: 16);
                return;
            }
            try
            {
                //Chequeo de vacios 
                if (!AllDataChecker()) {
                    //Faltan datos
                    SetActivity(false);
                    CustomizedToast(Android.Graphics.Color.White, Android.Graphics.Color.Black,
                    Languages.AllDataNeeded, iconResource: "error64", textSize: 16);
                    return;
                }

                if (!NameChecker(this.Nombres.TrimEnd(' ')) || !NameChecker(this.Apellidos.TrimEnd(' ')))
                {
                    SetActivity(false);
                    //Los nombres o apellidos no han sido bien escritos}
                    if(!NameChecker(this.Nombres.TrimEnd(' ')))
                        CustomizedToast(Android.Graphics.Color.White, Android.Graphics.Color.Black,
                        Languages.WronWGNames, iconResource: "error64", textSize: 16);
                    else
                        CustomizedToast(Android.Graphics.Color.White, Android.Graphics.Color.Black,
                        Languages.WronWGivenNames, iconResource: "error64", textSize: 16);
                    return;
                }

                //Chequeo de password correcto
                if (Settings.SuccesfullPassword != this.OldPassword)
                {
                    SetActivity(false);
                    //No coinciden las contraseñas
                    CustomizedToast(Android.Graphics.Color.White, Android.Graphics.Color.Black,
                    Languages.PasswordsDontMatch, iconResource: "error64", textSize: 16);
                    return;
                }

                //Se quiere actualizar algo de la seccion de credenciales?
                if (!this.NewPassword.Equals(string.Empty) || !this.RepeatPassword.Equals(string.Empty))
                {
                    //Todos los datos fueron proporcionados?
                    if (NewPassword.Equals(string.Empty) || RepeatPassword.Equals(string.Empty))
                    {
                        SetActivity(false);
                        CustomizedToast(Android.Graphics.Color.White, Android.Graphics.Color.Black,
                        Languages.AllDataNeeded, iconResource: "error64", textSize: 16);
                        return;
                    }
                    //Chequeo de repeticion de nueva contraseña
                    if (this.NewPassword != this.RepeatPassword)
                    {
                        SetActivity(false);
                        //No coinciden las contraseñas
                        CustomizedToast(Android.Graphics.Color.White, Android.Graphics.Color.Black,
                        Languages.PasswordsShouldMatch, iconResource: "error64", textSize: 16);
                        return;
                    }
                    else
                        updateCredentials = true;
                }


                //Creacion del modelo a enviar
                Stream streamedImage = GetImageSourceStream(this.Profile.ProfileImage);
                this.ProfileImageBytes = StreamToByteArray(streamedImage);
                

                var token = Settings.SerializedToken;

                var posted = new ApiPlainClientProfile()
                {
                    ID = Settings.ClientUID,
                    PrimerNombre = this.Nombres.Split(' ')[0],
                    SegundoNombre = this.Nombres.Split(' ')[1],
                    Apellido = this.Apellidos.Split(' ')[0],
                    SegundoApellido = this.Apellidos.Split(' ')[1],
                    Email = this.Profile.Email,
                    PP = this.ProfileImageBytes,
                    Afiliado = DateTime.Now
                };

                var posted2 = new ApiClientCredentials()
                {
                    IdClient=-1,
                    IdPersona = posted.ID,
                    Password = this.NewPassword,
                    UserName = "NONE"
                };

                Response result2=null;
                var result = await service.Put<ApiPlainClientProfile>(Constantes.BASEURL, Constantes.CLIENTPREFIX, Constantes.CLIENTUPDATEPROFILE, posted, token);
                if(updateCredentials)
                    result2 = await service.Put<ApiClientCredentials>(Constantes.BASEURL,Constantes.CLIENTPREFIX,Constantes.CLIENTUPDATECREDENTIALS,posted2,token) ;

                if (updateCredentials)
                {
                    if (!result.IsSuccesFull || !result2.IsSuccesFull)
                    {
                        SetActivity(false);
                        //Error en la peticion
                        if (!result.IsSuccesFull)
                        {
                            CustomizedToast(Android.Graphics.Color.White, Android.Graphics.Color.Black,
                            result.Message, iconResource: "error64", textSize: 16);
                            SetActivity(false);
                            return;
                        }
                        else
                            CustomizedToast(Android.Graphics.Color.White, Android.Graphics.Color.Black,
                            result2.Message, iconResource: "error64", textSize: 16);
                            SetActivity(false);
                        return;
                    }
                    Settings.SuccesfullPassword = this.NewPassword;
                }
                else {
                    if (!result.IsSuccesFull)
                    {
                        CustomizedToast(Android.Graphics.Color.White, Android.Graphics.Color.Black,
                        result.Message, iconResource: "error64", textSize: 16);
                        SetActivity(false);
                        return;
                    }
                }

                SetActivity(false);
                CustomizedToast(Android.Graphics.Color.White, Android.Graphics.Color.Black,
                    Languages.UpdatedProfile,iconResource:"ok96",textSize:16);
                UpdateLocalProfileInfo(posted);

            }
            catch (Exception ex)
            {
                CustomizedToast(Android.Graphics.Color.White, Android.Graphics.Color.Black,
                ex.Message, iconResource: "error64", textSize: 16);
                SetActivity(false);
                throw ex;
            }
            
        }

        private void UpdateLocalProfileInfo(ApiPlainClientProfile newValue) {
        
            var oldDate = ProfileViewModel.GetInstance().MyClient.Afiliado;

         
            ImageSource profileImage = null;
            if (this.ProfileImageBytes.Length != 0)
                profileImage = ImageSource.FromStream(() => new MemoryStream(this.ProfileImageBytes));

            ProfileViewModel.GetInstance().NombreApellido = $"{newValue.PrimerNombre} {newValue.Apellido}";

            ProfileViewModel.GetInstance().MyClient = new ClientProfile() {
                PrimerNombre = newValue.PrimerNombre,
                SegundoNombre = newValue.SegundoNombre,
                Apellido = newValue.Apellido,
                SegundoApellido = newValue.SegundoApellido,
                Email = newValue.Email,
                ProfileImage = profileImage,
                Afiliado = oldDate.Date
            };
            
            var tmpC = ProfileViewModel.GetInstance().MyClient;

            try
            {
                var r = Realm.GetInstance();
                r.Write(() => {
                    r.Add<RmbClientProfile>(new RmbClientProfile()
                    {
                        ID = Settings.ClientUID,
                        PrimerNombre = tmpC.PrimerNombre,
                        ProfilePhoto = this.ProfileImageBytes,
                        SegundoNombre= tmpC.SegundoNombre,
                        Apellido = tmpC.Apellido,
                        SegundoApellido= tmpC.SegundoApellido,
                        Email = tmpC.Email,
                        Afiliado = tmpC.Afiliado.Date
                    }, update: true);
                });
            }
            catch (Exception ex)
            {
                UpdateProfileViewModel.CustomizedToast(Android.Graphics.Color.White, Android.Graphics.Color.Black,
                 ex.Message, ToastLength.Long, iconResource: "error64", textSize: 16);
            }
        }


        public static void CustomizedToast(Android.Graphics.Color textColor, Android.Graphics.Color backgroundColor, string message,ToastLength length=ToastLength.Long,
             string iconResource="Elec",float textSize=16,string resourceFolder="drawable") {

            int resourceId = Android.App.Application.Context.Resources.GetIdentifier(iconResource, resourceFolder, Android.App.Application.Context.PackageName);
            var toast = Toast.MakeText(Android.App.Application.Context, message, length);
            var v = (Android.Views.ViewGroup)toast.View;
            if (v.ChildCount > 0 && v.GetChildAt(0) is TextView)
            {
                TextView tv = (TextView)v.GetChildAt(0);
                tv.SetTextColor(textColor);
                tv.SetCompoundDrawablesRelativeWithIntrinsicBounds(resourceId, 0, 0, 0);
                tv.SetTextSize(Android.Util.ComplexUnitType.Sp,textSize);
            }
            Android.Graphics.Color c = backgroundColor;
            ColorMatrixColorFilter CM = new ColorMatrixColorFilter(new float[]
                {
                        0,0,0,0,c.R,
                        0,0,0,0,c.G,
                        0,0,0,0,c.B,
                        0,0,0,1,0
                });
            toast.View.Background.SetColorFilter(CM);
            toast.Show();
        }

        private void SetActivity(bool input) {

            Device.BeginInvokeOnMainThread(()=> {
                this.IsVisible = input;
                this.IsBusy = input;
            });
        }

        private Boolean NameChecker(String text) {
            String[] inputArray = text.Split(' ');
            if (inputArray.Length != 2)
                return false;
            try {
                if ((inputArray[0].Equals(string.Empty) || inputArray[0].Equals(null))
                        || (inputArray[1].Equals(string.Empty) || inputArray[1].Equals(null)))
                    return false;
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        private bool AllDataChecker() {
            if (this.OldPassword.Equals(string.Empty)
                || this.Profile.Email.Equals(string.Empty) || this.Nombres.Equals(string.Empty) || this.Apellidos.Equals(string.Empty))
                return false;
            else
                return true;
        }

        public static byte[] StreamToByteArray(Stream input) {
            byte[] buffer = new byte[16*1024];
            using (MemoryStream ms = new MemoryStream()) {
                int read;
                while ((read = input.Read(buffer,0,buffer.Length))>0) {
                    ms.Write(buffer,0,read);
                }
                return ms.ToArray();
            }
        }

        public static  Stream GetImageSourceStream(ImageSource imgSource) {
            if (imgSource is StreamImageSource) {
                try {
                    StreamImageSource strImgSource = (StreamImageSource)imgSource;
                    System.Threading.CancellationToken cToken = System.Threading.CancellationToken.None;
                    Task <Stream> returned =  strImgSource.Stream(cToken);
                    return returned.Result;
                } catch (Exception ex) {
                    Debug.WriteLine(ex.Message);
                    return null;
                }
            }
            return null;
        }

        private void EmptyStringInitializer() {
            this.OldPassword = string.Empty;
            this.NewPassword = string.Empty;
            this.RepeatPassword = string.Empty;
        }
        #endregion

        #region Singleton
        public static UpdateProfileViewModel _instance { get; set; }
        public static UpdateProfileViewModel GetInstance() {
            if (_instance == null)
                return new UpdateProfileViewModel();
            else
                return _instance;
        }
        #endregion

    }
}
