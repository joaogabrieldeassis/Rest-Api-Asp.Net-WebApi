using RestApiV1.Domain.Interfaces.Repository;
using RestApiV1.Domain.Interfaces.Services;
using RestApiV1.Domain.Models;
using RestApiV1.Domain.Validations;

namespace RestApiV1.Domain.Service
{
    public class FornecedorService : BaseService, IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;

        public FornecedorService(IFornecedorRepository fornecedorRepository)
        {
            _fornecedorRepository = fornecedorRepository;
        }

        public async Task Adicionar(Fornecedor entity)
        {
            if (ExecutarValidacao(new FornecedorValidation(), entity)
                || ExecutarValidacao(new EnderecoValidation(), entity.Endereco))
                return;

            if (_fornecedorRepository.BuscarAsync(f => f.Documento == entity.Documento).Result.Any())
            {
                Notificar("Já existe um fornecedor com esse documento");
                return;
            }


            await _fornecedorRepository.AdicionarAsync(entity);
        }

        public async Task Atualizar(Fornecedor entity)
        {
            if (ExecutarValidacao(new FornecedorValidation(), entity))
                return;

            if (_fornecedorRepository.BuscarAsync(f => f.Documento == entity.Documento && f.Id != entity.Id).Result.Any())
            {
                Notificar("Já existe um fornecedor com esse documento");
                return;
            }

            await _fornecedorRepository.AdicionarAsync(entity);
        }

        public async Task Remover(Guid id)
        {
            var fornecedor = await _fornecedorRepository.ObterFornecedorProdutosEndereco(id);

           if(!OhFornecedorEstaValidoParaAhRemocao(fornecedor))
                return;

            var endereco = await _fornecedorRepository.ObterEnderecoPorFornecedor(id);

            if(endereco != null)
            {
                await _fornecedorRepository.RemoverEnderecoDoFornecedor(endereco);
            }

            await _fornecedorRepository.DeletarAsync(id);
        }

        public void Dispose()
        {
            _fornecedorRepository?.Dispose();
        }

        public bool OhFornecedorEstaValidoParaAhRemocao(Fornecedor fornecedor)
        {
            if (fornecedor == null)
            {
                Notificar("Esse fornecedor não existe na base de dados");
                return false;
            }
            if (fornecedor.Produtos.Any())
            {

                Notificar("Esse fornecedor possui produtos cadastrados");
                return false;
            }
            return true;
        }
    }
}
