using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UiSampleMigrat.Models;

namespace UiSampleMigrat.Interfaces
{
    public interface ILocalDao<T>
    {
        Task<T> RealmGet(T obj);
        Task<List<T>> RealmGetList();
        Task<Response> RealmUpdate(T obj);
        Task<Response> RealmSave(T obj);
        Task<Response> RealmDelete(T obj);
    }
}
