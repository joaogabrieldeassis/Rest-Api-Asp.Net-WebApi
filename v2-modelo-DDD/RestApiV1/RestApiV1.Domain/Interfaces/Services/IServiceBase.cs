
using RestApiV1.Domain.Models;

namespace RestApiV1.Domain.Interfaces.Services
{
    public interface IServiceBase<T> : IDisposable  where T : class
    {
        Task Adicionar(T entity);
        Task Atualizar(T entity);
        Task Remover(Guid id);
    }
}
