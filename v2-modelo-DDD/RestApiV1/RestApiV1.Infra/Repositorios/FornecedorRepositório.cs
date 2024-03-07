using Microsoft.EntityFrameworkCore;
using RestApiV1.Domain.Interfaces.Repository;
using RestApiV1.Domain.Models;
using RestApiV1.Infra.Context;

namespace RestApiV1.Infra.Repositorios
{
    public class FornecedorRepositório : RepositoryBase<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepositório(MeuDbContext context) : base(context)
        {

        }        

        public async Task<Fornecedor> ObterFornecedorEndereco(Guid id)
        {
            return await _context.Fornecedores.AsNoTracking().Include(e => e.Endereco).FirstOrDefaultAsync(c=>c.Id == id);
        }

        public async Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id) => await _context.Fornecedores.AsNoTracking().Include(x => x.Produtos).Include(x => x.Endereco).FirstOrDefaultAsync(x => x.Id == id);

        
        public async Task<Endereco> ObterEnderecoPorFornecedor(Guid id)
        {
            return await _context.Enderecos.AsNoTracking().FirstOrDefaultAsync(c => c.FornecedorId == id);
        }

        public async Task RemoverEnderecoDoFornecedor(Endereco endereco)
        {
             _context.Enderecos.Remove(endereco);
            await SaveChangesAsync();
        }
    }
}
