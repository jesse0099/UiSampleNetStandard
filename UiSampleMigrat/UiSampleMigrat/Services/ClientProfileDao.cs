using System;
using UiSampleMigrat.Helpers;
using System.Threading.Tasks;
using UiSampleMigrat.Api_Models;
using UiSampleMigrat.Interfaces;
using UiSampleMigrat.Models;
using UiSampleMigrat.MyExceptions;
using System.Collections.Generic;

namespace UiSampleMigrat.Services
{
    public class ClientProfileDao : BaseDao, IDao<ClientProfile>
    {
        public Login Login { get; set; }
        public ClientProfileDao()
        {

        }
        public Task<Response> Delete(ClientProfile obj)
        {
            throw new NotImplementedException();
        }

        public Task<Response> Push(ClientProfile obj)
        {
            throw new NotImplementedException();
        }

        public Task<Response> Put(ClientProfile obj)
        {
            throw new NotImplementedException();
        }

        public async Task<ClientProfile> Get()
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

                ClientProfile returned = new ClientProfile() { PrimerNombre = result.PrimerNombre,
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
                throw new ClientProfileException(ex.Message,ex);
            }
        }

        public Task<List<ClientProfile>> GetList()
        {
            throw new NotImplementedException();
        }
    }
}
