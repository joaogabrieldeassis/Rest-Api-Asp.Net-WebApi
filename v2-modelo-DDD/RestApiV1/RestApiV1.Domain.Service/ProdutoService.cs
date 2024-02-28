using RestApiV1.Domain.Interfaces.Repository;
using RestApiV1.Domain.Interfaces.Services;
using RestApiV1.Domain.Models;
using RestApiV1.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApiV1.Domain.Service
{
    public class ProdutoService : BaseService, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
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
            await _produtoRepository.DeletarAsync(id);
        }

        public void Dispose()
        {
            _produtoRepository?.Dispose();
        }
    }
}
