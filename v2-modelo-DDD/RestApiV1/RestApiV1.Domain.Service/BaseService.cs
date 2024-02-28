
using FluentValidation;
using RestApiV1.Domain.Models;

namespace RestApiV1.Domain.Service
{
    public class BaseService
    {

        protected void Notificar(string message)
        {

        }
        protected bool ExecutarValidacao<CV, TE>(CV classeValidacao, TE entidade) 
            where CV : AbstractValidator<TE>
            where TE : Entity
        {
            var validator = classeValidacao.Validate(entidade);

            if (validator.IsValid) return true;


            return false;
        }
    }
}
