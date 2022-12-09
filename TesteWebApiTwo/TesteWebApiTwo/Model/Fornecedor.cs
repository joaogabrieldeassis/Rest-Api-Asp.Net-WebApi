using System.ComponentModel.DataAnnotations;

namespace TesteWebApiTwo.Model
{
    public class Fornecedor
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(100,ErrorMessage = "O campo {0} precisa ter entre 2 e 1 caracterers",MinimumLength = 2)]
        public string Nome { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre 2 e 1 caracterers", MinimumLength = 2)]
        public string Documento { get; set; }
        public int TipoFornecedor { get; set; }
        public bool Ativo { get; set; }
    }
}
