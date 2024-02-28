using RestApiV1.Domain.Models.Enuns;

namespace RestApiV1.Domain.Models
{
    public class Fornecedor : Entity
    {
        public string? Nome { get; set; }
        public string? Documento { get; set; }
        public TipoFornecedor TipoDoFornecedor { get; set; }
        public bool Ativo { get; set; }
        public Endereco Endereco { get; set; }
        public IEnumerable<Produto> Produtos { get; set; }
    }
}
