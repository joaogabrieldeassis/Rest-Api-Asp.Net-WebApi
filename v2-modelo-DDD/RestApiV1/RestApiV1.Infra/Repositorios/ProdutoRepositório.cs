using Microsoft.EntityFrameworkCore;
using RestApiV1.Domain.Interfaces.Repository;
using RestApiV1.Domain.Models;
using RestApiV1.Infra.Context;

namespace RestApiV1.Infra.Repositorios
{
    public class ProdutoRepository : RepositoryBase<Produto>, IProdutoRepository
    {
        public ProdutoRepository(MeuDbContext context) : base(context)
        {

        }
        public async Task<Produto> ObterProdutoFornecedor(Guid id) => await _context.Produtos.AsNoTracking().Include(x => x.Fornecedor).FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId) => await BuscarAsync(x => x.FornecedorId == fornecedorId);

        public async Task<IEnumerable<Produto>> ObterProdutosFornecedores() => await _context.Produtos.AsNoTracking().Include(x => x.Fornecedor).OrderBy(x => x.Nome).ToListAsync();

    }
}
