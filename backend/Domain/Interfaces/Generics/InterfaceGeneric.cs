using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio.Interfaces.Generics
{
    public interface InterfaceGeneric<T> where T : class
    {
        Task<T> Add(T Objeto);
        Task<T> Update(T Objeto);
        Task Delete(T Objeto);
        Task<T> GetEntityByID(int id);
        Task<List<T>> List();
    }
}
