using AutoMapper;
using RestApiV1.Application.Dtos;
using RestApiV1.Application.Interfaces;
using RestApiV1.Domain.Interfaces.Repository;
using RestApiV1.Domain.Interfaces.Services;
using RestApiV1.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApiV1.Application
{
    public class ProdutoApplication : IProdutoApplication
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;

        public ProdutoApplication(IProdutoRepository produtoRepository, IProdutoService produtoService, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _produtoService = produtoService;
            _mapper = mapper;
        }

        public async Task Add(ProdutoDto produtoDto)
        {
            var produto = _mapper.Map<Produto>(produtoDto);
           await _produtoService.Adicionar(produto);
        }

        public async Task Delete(Guid id)
        {
           await _produtoService.Remover(id);
        }

        public async Task<ProdutoDto> ObterPorId(Guid id)
        {
            return _mapper.Map<ProdutoDto>(await _produtoRepository.ObterProdutoFornecedor(id));
        }

        public async Task<IEnumerable<ProdutoDto>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<ProdutoDto>>(await _produtoRepository.ObterProdutosFornecedores());
        }

        public async Task Update(ProdutoDto produtoDto)
        {
            var produto = _mapper.Map<Produto>(produtoDto);
            await _produtoService.Atualizar(produto);
        }
    }
}
