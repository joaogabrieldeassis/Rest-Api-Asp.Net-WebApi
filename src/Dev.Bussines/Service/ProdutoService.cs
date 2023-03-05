using Dev.Bussines.Interfaces;
using Dev.Bussines.Model.Validation;
using MinhaAp.Busines.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Bussines.Service
{
    public class ProdutoService : BaseService, IProdutoService
    {
        #region Private Fields

        private readonly IProdutoRepository _produtoRepository;
        //private readonly IUSer _user;

        #endregion Private Fields

        #region Public Constructors

        public ProdutoService(IProdutoRepository produtoRepository, INotificador notificador) : base(notificador)
        {
            _produtoRepository = produtoRepository;            
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task Adicionar(Produto produto)
        {
            if (!ExecutarValidacao(validacao: new ProdutoValidation(),
                                   entidade: produto))
                return;

            //var user = _user.GetUserId();

            await _produtoRepository.Adicionar(produto);
        }

        public async Task Atualizar(Produto produto)
        {
            if (!ExecutarValidacao(validacao: new ProdutoValidation(),
                                   entidade: produto))
                return;

            await _produtoRepository.Atualizar(produto);
        }

        public async Task Remover(Guid id)
        {
            await _produtoRepository.Deletar(id);
        }

        public void Dispose()
        {
            _produtoRepository?.Dispose();
        }

        #endregion Public Methods
    }
}
