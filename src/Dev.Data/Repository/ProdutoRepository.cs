using MinhaAp.Busines.Models;
using Dev.Bussines.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Dev.Data.Context;

namespace Dev.Data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(MeuDbContext context) : base(context)
        {

        }
        public async Task<Produto> ObterProdutoFornecedor(Guid id) => await _context.Produtos.AsNoTracking().Include(x => x.Fornecedor).FirstOrDefaultAsync(x => x.Id == id);
        

        public async Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId)=> await Buscar(x => x.FornecedorId == fornecedorId);

        public async Task<IEnumerable<Produto>> BuscarPeloNomeDoFornecedor(Guid nome) => (IEnumerable<Produto>)_context.Produtos.FirstOrDefault(x => x.FornecedorId == nome);

        public async Task<IEnumerable<Produto>> ObterProdutosFornecedores()=> await _context.Produtos.AsNoTracking().Include(x => x.Fornecedor).OrderBy(x => x.Nome).ToListAsync();
        
    }
}
