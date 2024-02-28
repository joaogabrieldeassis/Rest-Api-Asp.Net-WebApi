using FluentValidation;
using MinhaAp.Busines.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Bussines.Model.Validation
{
    public class ProdutoValidation : AbstractValidator<Produto>
    {
        public ProdutoValidation()
        {
            RuleFor(produto => produto.Nome)
                .NotEmpty()
                    .WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 200)
                    .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(produto => produto.Descricao)
                .NotEmpty()
                    .WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 1000)
                    .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(produto => produto.Valor)
                .GreaterThan(0)
                    .WithMessage("O campo {PropertyName} precisa ser maior que {ComparisonValue}");
        }
    }
}
