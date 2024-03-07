using Microsoft.AspNetCore.Mvc;
using RestApiV1.Application.Dtos;

namespace RestApiV1.Api.Controllers
{
    public class FornecedorController
    {


        [HttpGet]
        public async Task<IEnumerable<FornecedorDto>> ObterTodosOsFornecedores()
        {

        }



        [HttpGet("{id:guid}")]
        public async Task<ActionResult<FornecedorDto>> ObterFornecedorPorId(Guid id)
        {
           

        }

        [HttpPost]
        public async Task<ActionResult<FornecedorDto>> CriarFornecedor(FornecedorDto fornecedor)
        {
            

        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<FornecedorDto>> Update(Guid id, FornecedorDto fornecedor)
        {

            
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<FornecedorDto>> Excluir(Guid id)
        {
            
        }

        [HttpGet("obter-endereco/{id:guid}")]
        public async Task<FornecedorDto> ObterEnderecoPorId(Guid id)
        {

           
        }

        [HttpPut("atualizar-endereco/{id:guid}")]
        private async Task<ActionResult> AtualizarEndereco(Guid id, EnderecoDto endereco)
        {
           
        }

    }
}
