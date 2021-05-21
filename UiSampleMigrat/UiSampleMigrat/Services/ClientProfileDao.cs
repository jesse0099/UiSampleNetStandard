using System;
using UiSampleMigrat.Helpers;
using System.Threading.Tasks;
using UiSampleMigrat.Api_Models;
using UiSampleMigrat.Interfaces;
using UiSampleMigrat.Models;
using UiSampleMigrat.MyExceptions;
using System.Collections.Generic;
using Realms;
using Android.Widget;

namespace UiSampleMigrat.Services
{
    public class ClientProfileDao : BaseDao, IDao<ClientProfile>,ILocalDao<ClientProfile>
    {
        public Login Login { get; set; }
        public ClientProfileDao()
        {

        }
        #region Metodos Locales
        #endregion

        #region IDao<ClientProfileDao>
        public Task<Response> Delete(ClientProfile obj)
        {
            throw new NotImplementedException();
        }

        public Task<Response> Put(ClientProfile obj)
        {
            throw new NotImplementedException();
        }

        public Task<List<ClientProfile>> GetList()
        {
            throw new NotImplementedException();
        }
   
        public async Task<ClientProfile> Get(ClientProfile obj)
        {
            ApiPlainClientProfile result;
            if (!proc.CheckConnection().IsSuccesFull)
                throw new ConnectionException("Sin conexion a internet.");
            try
            {
                var profileControllerString = $"{Constantes.CLIENTPROFILE}{Constantes.LOGINAUTHUSERPAR}={Login.userName}&{Constantes.LOGINAUTHPASSPAR}={Login.password}";
                var response = await proc.Get<ApiPlainClientProfile>(Constantes.BASEURL, Constantes.CLIENTPREFIX, profileControllerString, Settings.SerializedToken);

                if (!response.IsSuccesFull)
                    throw new ClientProfileException("Problemas Obteniendo la informacion.");
                result = (ApiPlainClientProfile)response.Result;

                ClientProfile returned = new ClientProfile()
                {
                    PrimerNombre = result.PrimerNombre,
                    SegundoNombre = result.SegundoNombre,
                    Apellido = result.Apellido,
                    SegundoApellido = result.SegundoApellido,
                    Email = result.Email,
                    ProfileImage = Commons.ObjectToImageSource(result.PP),
                    Afiliado = result.Afiliado
                };

                return returned;
            }
            catch (Exception ex)
            {
                throw new ClientProfileException(ex.Message, ex);
            }
        }

        public Task<Response> Post(ClientProfile obj)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region ILocalDao<ClientProfileDao>
        public Task<ClientProfile> RealmGet(ClientProfile obj)
        {
            throw new NotImplementedException();
        }

        public Task<List<ClientProfile>> RealmGetList()
        {
            throw new NotImplementedException();
        }

        public Task<Response> RealmUpdate(ClientProfile obj)
        {
            throw new NotImplementedException();
        }

        public async Task<Response> RealmSave(ClientProfile obj)
        {
            Response response = new Response()
            {
                IsSuccesFull = false
            };
            try
            {
                var r = Realm.GetInstance();
                var ppBytes = Commons.StreamToByteArray(Commons.GetImageSourceStream(obj.ProfileImage));
                await r.WriteAsync((x) =>
                {
                    //throw new ClientProfileException("Excepcion de prueba");
                    x.Add(new RmbClientProfile()
                    {
                        ID = obj.ID,
                        ProfilePhoto = ppBytes,
                        Afiliado = obj.Afiliado,
                        Apellido = obj.Apellido,
                        SegundoApellido = obj.SegundoApellido,
                        Email = obj.Email,
                        PrimerNombre = obj.PrimerNombre,
                        SegundoNombre = obj.SegundoNombre,
                    });
                });
                response.IsSuccesFull = true;
                return response;
            }
            catch (Exception ex)
            {

                Commons.CustomizedToast(Android.Graphics.Color.White, Android.Graphics.Color.Black,
                    ex.Message, ToastLength.Long, iconResource: "error64", textSize: 16);
                return response;
            }
        }

        public Task<Response> RealmDelete(ClientProfile obj)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
