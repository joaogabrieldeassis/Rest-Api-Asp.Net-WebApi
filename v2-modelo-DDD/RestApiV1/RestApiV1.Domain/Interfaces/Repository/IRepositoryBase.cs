using RestApiV1.Domain.Models;
using System.Linq.Expressions;

namespace RestApiV1.Domain.Interfaces.Repository
{
    public interface IRepositoryBase<T> : IDisposable where T : class
    {
        Task AdicionarAsync(T entity);
        Task<T> ObterPorIdAsync(Guid id);
        Task AtualizarAsync(T entity);
        Task<List<T>> ObterTodosAsync();
        Task DeletarAsync(Guid id);
        Task<IEnumerable<T>> BuscarAsync(Expression<Func<T, bool>> predicate);
        Task<int> SaveChangesAsync();
    }
}
