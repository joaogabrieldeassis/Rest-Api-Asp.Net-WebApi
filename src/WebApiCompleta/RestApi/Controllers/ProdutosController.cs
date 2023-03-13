using AutoMapper;
using Dev.AppMvc.ViewModels;
using Dev.Bussines.Interfaces;
using DevIO.Business.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MinhaAp.Busines.Models;
using RestApi.Extensions;

namespace RestApi.Controllers
{
   [Authorize]
   [Route("api/produtos")]
    public class ProdutosController : MainController
    {
       
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;       

        public ProdutosController(IProdutoRepository produtoRepository, 
            IProdutoService produtoService,
            IMapper mapper,
            INotificador notificador, IUser user):base(user, notificador )
        {
            _produtoRepository = produtoRepository;
            _produtoService = produtoService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<ProdutoViewModel>> ObterTodosOsProdutos()
        {
            List<ProdutoViewModel> produtos;
            return    _mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterProdutosFornecedores()); 
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProdutoViewModel>> ObterProdutoPorId(Guid id)
        {
            
            var buscarProduto = await ObterProdutoFornecedor(id);
            if (buscarProduto.Value.Id == null)
            {
                NotificarErro("O id informados não são iguais");
                return CustomReponse();
            }
            return CustomReponse(buscarProduto);                        
        }
        [ClaimsAuthorize("Produto","Adicionar")]
        [HttpPost]
        public async Task<ActionResult<ProdutoViewModel>> CriarUmProduto(ProdutoViewModel produtoViewModel)
        {
            if (!ModelState.IsValid) return CustomReponse(ModelState);            

            var imagemNome = Guid.NewGuid() + "_" + produtoViewModel.Imagem;
            if (!UploadDeArquivo(produtoViewModel.ImagemUplade, imagemNome))
            {
                return CustomReponse();
            }
            produtoViewModel.Imagem = imagemNome;
            await _produtoService.Adicionar(_mapper.Map<Produto>(produtoViewModel));
            return CustomReponse(produtoViewModel);
        }
        [ClaimsAuthorize("Produto", "Atualizar")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ProdutoViewModel>> Atualizar(Guid id, ProdutoViewModel produtoViewModel)
        {
            var buscarProduto = await ObterProdutoFornecedor(id);
            
            if (buscarProduto.Value.Id == null) return NotFound();

            produtoViewModel.Imagem = buscarProduto.Value.Imagem;

            if (!ModelState.IsValid) return CustomReponse();

            if (produtoViewModel.ImagemUplade != null)
            {
                var imagemNome = Guid.NewGuid() + "_" + produtoViewModel.Imagem;
                if (!UploadDeArquivo(produtoViewModel.Imagem,imagemNome))
                {
                    return CustomReponse(ModelState);
                }
                buscarProduto.Value.Imagem = imagemNome;
            }

            buscarProduto.Value.Nome = produtoViewModel.Nome;
            buscarProduto.Value.Descricao = produtoViewModel.Descricao;
            buscarProduto.Value.Valor = produtoViewModel.Valor;
            buscarProduto.Value.Ativo = produtoViewModel.Ativo;
            await _produtoService.Atualizar(_mapper.Map<Produto>(produtoViewModel));

            return CustomReponse(produtoViewModel);
        }

        [ClaimsAuthorize("Produto","Excluir")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ProdutoViewModel>> DeletarProduto(Guid id)
        {
            var buscarProduto = await ObterProdutoFornecedor(id);
            if (buscarProduto == null) NotFound();

            await _produtoService.Remover(id);
            return CustomReponse(buscarProduto);
            
        }

        [NonAction]
        public async Task<ActionResult<ProdutoViewModel>> ObterProdutoFornecedor(Guid id) => _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterProdutoFornecedor(id));
        
        [NonAction]
        public bool UploadDeArquivo(string arquivo,string nomeDaImagem)
        {            
            if (string.IsNullOrEmpty(arquivo))
            {
               NotificarErro("Forneça uma imagens para esse produto");
                return false;
            }
            var imagemDataByteArray = Convert.FromBase64String(arquivo);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", nomeDaImagem);
            if (System.IO.File.Exists(filePath))
            {
                NotificarErro("Já existe um arquivo com este nome ");
                return false;
            }
            System.IO.File.WriteAllBytes(filePath, imagemDataByteArray);
            return true;

        }
    }
}
