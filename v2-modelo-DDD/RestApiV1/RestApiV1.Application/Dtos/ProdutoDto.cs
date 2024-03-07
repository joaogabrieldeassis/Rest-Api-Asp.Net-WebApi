
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RestApiV1.Application.Dtos
{
    public class ProdutoDto
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracters", MinimumLength = 2)]
        public string Nome { get; set; }

        public Guid? FornecedorId { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracters", MinimumLength = 2)]
        public string Descricao { get; set; }

        [DisplayName("Imagem do Produto")]
        public string? ImagemUplade { get; set; }

        public string? Imagem { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]

        public decimal Valor { get; set; }
        [ScaffoldColumn(false)]

        public DateTime DataDeCadastro { get; set; }

        [DisplayName("Ativo?")]
        public bool Ativo { get; set; }
        public string? NomeFornecedor { get; set; }
    }
}
