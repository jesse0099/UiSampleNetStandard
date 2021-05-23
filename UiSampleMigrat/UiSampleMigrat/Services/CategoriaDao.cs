using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UiSampleMigrat.Interfaces;
using UiSampleMigrat.Models;
using UiSampleMigrat.MyExceptions;

namespace UiSampleMigrat.Services
{
    public class CategoriaDao : BaseDao, IDao<Categoria>
    {
        public Task<Response> Delete(Categoria obj)
        {
            throw new System.NotImplementedException();
        }

        public Task<Categoria> Get(Categoria obj)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Categoria>> GetList()
        {
            if (!proc.CheckConnection().IsSuccesFull)
                throw new ConnectionException("Sin Acceso a Internet");

            try
            {
                var response = await proc.Get<List<Categoria>>(Constantes.BASEURL, Constantes.EXPLOREPREFIX, Constantes.CATEGORIES);

                if (!response.IsSuccesFull)
                    throw new CategoryException(response.Message);

                return (List<Categoria>)response.Result;
            }
            catch (Exception ex)
            {
                throw new CategoryException(ex.Message, ex);
            }
        }

        public Task<Response> Post(Categoria obj)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response> Put(Categoria obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
