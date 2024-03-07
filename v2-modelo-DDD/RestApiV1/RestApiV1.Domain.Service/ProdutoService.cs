using RestApiV1.Domain.Interfaces.Notification;
using RestApiV1.Domain.Interfaces.Repository;
using RestApiV1.Domain.Interfaces.Services;
using RestApiV1.Domain.Models;
using RestApiV1.Domain.Validations;

namespace RestApiV1.Domain.Service
{
    public class ProdutoService : BaseService, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository, INotificador notificador)
            : base(notificador)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task Adicionar(Produto entity)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), entity))
                return;

            await _produtoRepository.AdicionarAsync(entity);
        }

        public async Task Atualizar(Produto entity)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), entity))
                return;

            await _produtoRepository.AdicionarAsync(entity);
        }

        public async Task Remover(Guid id)
        {
            var produto = await _produtoRepository.ObterPorIdAsync(id);

            if (produto == null)
                Notificar("Produto não encontrado");

            await _produtoRepository.DeletarAsync(produto);
        }

        public void Dispose()
        {
            _produtoRepository?.Dispose();
        }
    }
}
