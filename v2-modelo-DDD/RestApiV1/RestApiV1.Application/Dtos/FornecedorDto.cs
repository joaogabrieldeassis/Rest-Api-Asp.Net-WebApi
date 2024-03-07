
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RestApiV1.Application.Dtos
{
    public class FornecedorDto
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracters", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracters", MinimumLength = 2)]
        public string Documento { get; set; }

        [DisplayName("Tipo")]
        public int TipoDoFornecedor { get; set; }

        [DisplayName("Ativo?")]
        public bool Ativo { get; set; }

        public EnderecoDto? Endereco { get; set; }

        public IEnumerable<ProdutoDto>? Produtos { get; set; }
    }
}
