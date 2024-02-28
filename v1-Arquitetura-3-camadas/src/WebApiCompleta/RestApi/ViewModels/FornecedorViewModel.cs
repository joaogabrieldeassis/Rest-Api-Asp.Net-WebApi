using MinhaAp.Busines.Models.Enuns;
using MinhaAp.Busines.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Dev.AppMvc.ViewModels
{
    public class FornecedorViewModel
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

        public EnderecoViewModel? Endereco { get; set; }

        public IEnumerable<ProdutoViewModel>? Produtos { get; set; }
    }
}
