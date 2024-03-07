using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApiV1.Application.Interfaces
{
    public interface IApplication<T> where T : class
    {
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(Guid id);
        Task<IEnumerable<T>> ObterTodos();
        Task<T> ObterPorId(Guid id);
    }
}
