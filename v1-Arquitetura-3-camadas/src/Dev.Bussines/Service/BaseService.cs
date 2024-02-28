using Dev.Bussines.Interfaces;
using Dev.Bussines.Notificacoes;
using FluentValidation;
using FluentValidation.Results;
using MinhaAp.Busines.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Bussines.Service
{
    public abstract class BaseService
    {
        private readonly INotificador _notificador;
        protected BaseService(INotificador notificador)
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
        protected void Notificar(string menssagem)
        {
            _notificador.Handle(new Notificacao(menssagem));
        }
        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
        {
            ValidationResult _validacao = validacao.Validate(entidade);

            if (_validacao.IsValid)
                return true;

            Notificar(_validacao);

            return false;
        }
    }
}
