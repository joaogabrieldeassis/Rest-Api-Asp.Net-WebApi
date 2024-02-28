using FluentValidation;
using RestApiV1.Domain.Models;
using RestApiV1.Domain.Models.Enuns;
using RestApiV1.Domain.Validations.Documentos;
using static RestApiV1.Domain.Validations.Documentos.CpfValidacao;

namespace RestApiV1.Domain.Validations
{
    public class FornecedorValidation : AbstractValidator<Fornecedor>
    {
        public FornecedorValidation()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa estar preenchido")
                .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracters");
            When(x => x.TipoDoFornecedor == TipoFornecedor.PessoaFisica, () =>
            {
                RuleFor(x => x.Documento.Length).Equal(CpfValidacao.TAMANHO_CPF)
                .WithMessage("O campo Documento precisa ter {ComparisonValue} caracters e foi fornecido {PropertyValue}");
                RuleFor(x => CpfValidacao.Validar(x.Documento)).Equal(true)
                .WithMessage("O documento fornecido é invalido");
            });
            When(x => x.TipoDoFornecedor == TipoFornecedor.PessoaJuridica, () =>
            {
                RuleFor(x => x.Documento.Length).Equal(CnpjValidacao.TAMANHO_CNPJ)
                                .WithMessage("O campo Documento precisa ter {ComparisonValue} caracters e foi fornecido {PropertyValue}");
                RuleFor(x => CnpjValidacao.Validar(x.Documento)).Equal(true)
                .WithMessage("O documento fornecido é invalido");
            });
        }
        public void ValidarFornecedorParaAhRemocao()
        {
            
        }
    }
}
