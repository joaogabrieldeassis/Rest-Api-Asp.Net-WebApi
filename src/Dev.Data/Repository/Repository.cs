using Dev.Bussines.Interfaces;
using Dev.Data.Context;
using Microsoft.EntityFrameworkCore;
using MinhaAp.Busines.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly MeuDbContext _context;
        protected readonly DbSet<TEntity> _entities;

        protected Repository(MeuDbContext context) 
        
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.AsNoTracking().Where(predicate).ToListAsync();
        }
        public virtual async Task<TEntity> ObterPorId(Guid id)
        {
            return await _entities.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> ObterTodos()
        {
            return await _entities.ToListAsync();
        }
        public virtual async Task Adicionar(TEntity entity)
        {
            _entities.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Atualizar(TEntity entity)
        {
            _entities.Update(entity);
            await SaveChanges();
        }       

        public virtual async Task Deletar(Guid id)
        {
            var entity = _entities.AsNoTracking().FirstOrDefaultAsync(x=>x.Id == id);
            _entities.Remove(await entity);
            await SaveChanges();
        }

        public async Task<int> SaveChanges() =>  _context.SaveChanges();

        public void Dispose() => _context?.Dispose();
    }
}
