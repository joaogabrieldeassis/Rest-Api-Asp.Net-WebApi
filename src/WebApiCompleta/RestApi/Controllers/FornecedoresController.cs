using AutoMapper;
using Dev.AppMvc.ViewModels;
using Dev.Bussines.Interfaces;
using Dev.Bussines.Notificacoes;
using DevIO.Business.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinhaAp.Busines.Models;
using RestApi.Extensions;

namespace RestApi.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/fornecedores")]
    public class FornecedoresController : MainController
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IFornecedorService _fornecedorService;
        private readonly IMapper _mapper;        

        public FornecedoresController(IFornecedorRepository fornecedorRepository,
            IMapper mapper,
            IFornecedorService fornecedorService,
            INotificador notificador,
            IEnderecoRepository enderecoRepository,
            IUser appUser
            ) :base(appUser,notificador)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;   
            _fornecedorService = fornecedorService;
            _enderecoRepository = enderecoRepository;
            
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<FornecedorViewModel>> ObterTodosOsFornecedores()=> _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());

        
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>> ObterFornecedorPorId(Guid id)
        {
            var receiveId = await ObterFornecedorProdutosEnderecos(id);
            if (receiveId != null) return Ok(receiveId);
            return StatusCode(404);
           
        }
        [ClaimsAuthorize("Fornecedor","Adicionar")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<FornecedorViewModel>> CriarFornecedor(FornecedorViewModel fornecedor)
        {
            if (UsuarioAutenticado)
            {
                var userName = UsuarioId;
            }
            if (!ModelState.IsValid) return CustomReponse(ModelState); 
            
             await _fornecedorService.Adicionar(_mapper.Map<Fornecedor>(fornecedor));
                     
            return CustomReponse(fornecedor);
            
        }
        [ClaimsAuthorize("Fornecedor", "Atualizar")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>> Update(Guid id, FornecedorViewModel fornecedor)
        {
            
            if (id != fornecedor.Id) return BadRequest();
                        
            if (!ModelState.IsValid) return CustomReponse(ModelState);

            
             await _fornecedorService.Atualizar(_mapper.Map<Fornecedor>(fornecedor));
            return CustomReponse(fornecedor);
        }
        [ClaimsAuthorize("Fornecedor", "Remover")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>> Excluir(Guid id)
        {
            var buscarFornecedor = _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorEndereco(id));
            if (buscarFornecedor == null) return NotFound();
                        
            await _fornecedorService.Remover(id);
                                   
            return CustomReponse();
        }
        [AllowAnonymous]
        [HttpGet("obter-endereco/{id:guid}")]
        public async Task<EnderecoViewModel> ObterEnderecoPorId(Guid id)
        {

            return _mapper.Map<EnderecoViewModel>(await _enderecoRepository.ObterPorId(id));
        }
        [ClaimsAuthorize("Fornecedor", "Atualizar")]
        [HttpPut("atualizar-endereco/{id:guid}")]
        private async Task<ActionResult> AtualizarEndereco(Guid id, EnderecoViewModel endereco)
        {

            if (id != endereco.Id) return BadRequest();

            if (!ModelState.IsValid) return CustomReponse(ModelState);


            await _enderecoRepository.Atualizar(_mapper.Map<Endereco>(endereco));
            return CustomReponse(endereco);
        }
        [NonAction]
        private async Task<FornecedorViewModel> ObterFornecedorProdutosEnderecos(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorProdutosEndereco(id));
        }
    }
}
