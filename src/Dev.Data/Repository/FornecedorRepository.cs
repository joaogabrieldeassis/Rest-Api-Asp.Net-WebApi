using Dev.Bussines.Interfaces;
using Dev.Data.Context;
using Microsoft.EntityFrameworkCore;
using MinhaAp.Busines.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Data.Repository
{
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository(MeuDbContext context) : base(context)
        {

        }
        public async Task<Fornecedor> ObterFornecedorEndereco(Guid id) => await _context.Fornecedores.AsNoTracking().Include(x => x.Endereco).FirstOrDefaultAsync(c => c.Id == id);

        public async Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id) => await _context.Fornecedores.AsNoTracking().Include(x => x.Produtos).Include(x => x.Endereco).FirstOrDefaultAsync(x => x.Id == id);
    }
}
