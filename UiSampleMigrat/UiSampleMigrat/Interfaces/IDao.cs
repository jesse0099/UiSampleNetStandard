using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using UiSampleMigrat.Models;

namespace UiSampleMigrat.Interfaces
{
    public interface IDao<T>
    {
      Task<T> Get();
      Task<List<T>> GetList();
      Task<Response> Put(T obj);
      Task<Response> Push(T obj);
      Task<Response> Delete(T obj);
      
    }
}
