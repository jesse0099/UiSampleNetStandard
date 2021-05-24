using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UiSampleMigrat.Api_Models;
using UiSampleMigrat.Helpers;
using UiSampleMigrat.Interfaces;
using UiSampleMigrat.Models;
using UiSampleMigrat.MyExceptions;

namespace UiSampleMigrat.Services
{
    public class ComercioDao : BaseDao, IDao<Comercio>
    {
        public Task<Response> Delete(Comercio obj)
        {
            throw new System.NotImplementedException();
        }

        public Task<Comercio> Get(Comercio obj)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Comercio>> GetList()
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Comercio>> GetListByCats(List<Categoria> cats) {
            if (!proc.CheckConnection().IsSuccesFull)
                throw new ConnectionException();
            try
            {
                if (cats.Count < 1)
                    throw new ComerException("Ninguna Categoria Seleccionada");

                var ids = cats.ConvertAll<int>((delegate (Categoria cat) { return cat.Id; } ));

                var objName = "vals";
                var response  = await proc.Get<List<Comercio>>(Constantes.BASEURL, Constantes.COMMEPREFIX, $"{Constantes.COMMEGETBYCATS}{Commons.IdsWrapper(ids,objName)}");
                var returned = (List<Comercio>)response.Result;
                return returned;
            }
            catch (Exception ex)
            {

                throw new ComerException(ex.Message,ex);
            }
        }

        public Task<Response> Post(Comercio obj)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response> Put(Comercio obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
