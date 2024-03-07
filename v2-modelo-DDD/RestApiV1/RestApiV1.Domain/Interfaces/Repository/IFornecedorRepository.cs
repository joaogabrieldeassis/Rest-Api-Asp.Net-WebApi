using RestApiV1.Domain.Models;

namespace RestApiV1.Domain.Interfaces.Repository
{
    public interface IFornecedorRepository : IRepositoryBase<Fornecedor>
    {
        public Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id);
        public Task RemoverEnderecoDoFornecedor(Endereco endereco);
        public Task<Endereco> ObterEnderecoPorFornecedor(Guid id);
        public Task<Fornecedor> ObterFornecedorEndereco(Guid id);
    }
}
