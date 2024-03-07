using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestApiV1.Application.Dtos;
using RestApiV1.Application.Interfaces;
using RestApiV1.Domain.Interfaces.Repository;
using RestApiV1.Domain.Interfaces.Services;

namespace RestApiV1.Api.Controllers
{
    public class ProdutosController
    {
        private readonly IProdutoApplication _produtoApplication;

        public ProdutosController(IProdutoApplication produtoApplication)
        {
            _produtoApplication = produtoApplication;
        }

        [HttpGet]
        public async Task<IEnumerable<ProdutoDto>> ObterTodosOsProdutos()
        {
            return await _produtoApplication.ObterTodos();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProdutoDto>> ObterProdutoId(Guid id)
        {
            var produtoDto = await _produtoApplication.ObterPorId(id);
            //if(produto == null)

            return produtoDto;

        }

        [HttpPost]
        public async Task<ActionResult<ProdutoDto>> CriarUmProduto(ProdutoDto produtoViewModel)
        {

        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ProdutoDto>> Atualizar(Guid id, ProdutoDto produtoViewModel)
        {

        }


        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ProdutoDto>> DeletarProduto(Guid id)
        {


        }
       
    }
}
