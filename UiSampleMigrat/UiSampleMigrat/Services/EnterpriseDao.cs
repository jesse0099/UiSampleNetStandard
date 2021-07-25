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
    public class EnterpriseDao : BaseDao, IDao<Enterprise>
    {
        public Task<Response> Delete(Enterprise obj)
        {
            throw new System.NotImplementedException();
        }

        public Task<Enterprise> Get(Enterprise obj)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Enterprise>> GetList()
        {
            throw new System.NotImplementedException();
        }

        //Lista de comercios dada una lista de categorias
        public async Task<List<Enterprise>> GetListByCats(List<Categoria> cats) {
            if (!proc.CheckConnection().IsSuccesFull)
                throw new ConnectionException();
            try
            {
                if (cats.Count < 1)
                    throw new ComerException("Ninguna Categoria Seleccionada");

                var ids = cats.ConvertAll((delegate (Categoria cat) { return cat.Id; } ));

                var objName = "vals";
                var response  = await proc.Get<List<ApiEnterprise>>(Constantes.BASEURL, Constantes.COMMEPREFIX, $"{Constantes.COMMEGETBYCATS}{Commons.IdsWrapper(ids,objName)}");

                if (!response.IsSuccesFull)
                    throw new ComerException(response.Message);

                var returned = ((List<ApiEnterprise>)response.Result).ConvertAll(delegate (ApiEnterprise result) {
                    return new Enterprise(result);
                });

                return returned;
            }
            catch (Exception ex)
            {

                throw new ComerException(ex.Message,ex);
            }
        }

        public Task<Response> Post(Enterprise obj)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response> Put(Enterprise obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
