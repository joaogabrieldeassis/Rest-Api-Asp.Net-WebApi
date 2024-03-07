
using FluentValidation;
using FluentValidation.Results;
using RestApiV1.Domain.Interfaces.Notification;
using RestApiV1.Domain.Models;
using RestApiV1.Domain.Notification;

namespace RestApiV1.Domain.Service
{
    public class BaseService
    {
        private readonly INotificador _notificador;

        public BaseService(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }

        protected void Notificar(string message)
        {
            _notificador.Handle(new Notificacao(message));
        }

        protected bool ExecutarValidacao<CV, TE>(CV classeValidacao, TE entidade) 
            where CV : AbstractValidator<TE>
            where TE : Entity
        {
            var validator = classeValidacao.Validate(entidade);

            if (validator.IsValid) return true;

            Notificar(validator);

            return false;
        }
    }
}
