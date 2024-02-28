using RestApiV1.Domain.Models;

namespace RestApiV1.Domain.Interfaces.Services
{
    public interface IFornecedorService : IServiceBase<Fornecedor>
    {
        public bool OhFornecedorEstaValidoParaAhRemocao(Fornecedor fornecedor);
    }
}
