using Microsoft.EntityFrameworkCore;
using RestApiV1.Domain.Interfaces.Repository;
using RestApiV1.Infra.Context;
using System.Linq.Expressions;

namespace RestApiV1.Infra.Repositorios
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly MeuDbContext _context;
        protected readonly DbSet<T> _entities;

        public RepositoryBase(MeuDbContext meuDbContext)
        {
                _context = meuDbContext;
            _entities = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> ObterTodosAsync()
        {
           return await _entities.AsNoTracking().ToListAsync();
        }

        public async Task<T> ObterPorIdAsync(Guid id)
        {
            return await _entities.FindAsync(id);  
        }

        public async Task<IEnumerable<T>> BuscarAsync(Expression<Func<T, bool>> predicate)
        {
            return await _entities.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task AdicionarAsync(T entity)
        {
            await _entities.AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task AtualizarAsync(T entity)
        {
            _entities.Update(entity);
            await SaveChangesAsync();
        }        

        public async Task DeletarAsync(T entity)
        {
            _entities.Remove(entity);
            await SaveChangesAsync();
        }                

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
