using System;
using UiSampleMigrat.Helpers;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using UiSampleMigrat.Interfaces;
using UiSampleMigrat.Models;
using UiSampleMigrat.MyExceptions;
using System.Collections.Generic;

namespace UiSampleMigrat.Services
{
    public class LoginDao : BaseDao,IDao<Login>
    {

        public LoginDao()
        {
            
        }

        public async Task<Response> Auth(Login obj) {
            if (!proc.CheckConnection().IsSuccesFull)
                throw new ConnectionException("Sin Acceso a Internet");
                
            try {
                if (obj.password == null || obj.userName == null)
                    throw new LoginException("Todos los datos son necesarios");

                var controllerString = $"{Constantes.LOGINAUTH}{Constantes.LOGINAUTHUSERPAR}={obj.userName}&{Constantes.LOGINAUTHPASSPAR}={obj.password}";
                var response = await proc.Get<string>(Constantes.BASEURL, Constantes.LOGINPREFIX, controllerString);

                if (!response.IsSuccesFull)
                    throw new LoginException("Credenciales Incorrectas");

                var tempToken = Convert.ToString(response.Result);
                Settings.SerializedToken = tempToken;
                Settings.IsRemembered = obj.RememberMe;
                return response;
            } catch (Exception ex) {
                throw new LoginException(ex.Message,ex);
            }

        }

        public Task<Response> Delete(Login obj)
        {
            throw new NotImplementedException();
        }
        Task<Login> IDao<Login>.Get()
        {
            throw new NotImplementedException();
        }
        public Task<List<Login>> GetList()
        {
            throw new NotImplementedException();
        }

        public Task<Response> Push(Login obj)
        {
            throw new NotImplementedException();
        }

        public Task<Response> Put(Login obj)
        {
            throw new NotImplementedException();
        }

    }
}
