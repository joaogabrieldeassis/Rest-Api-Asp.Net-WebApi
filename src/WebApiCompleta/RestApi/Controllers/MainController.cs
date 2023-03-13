using Dev.Bussines.Interfaces;
using Dev.Bussines.Notificacoes;
using DevIO.Business.Intefaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace RestApi.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotificador _notificador;
        public readonly IUser _appUser;

        public Guid UsuarioId { get; set; }
        public bool UsuarioAutenticado{ get; set; }

        public MainController(IUser appUser,INotificador notificador)
        {
            _appUser = appUser;
            _notificador = notificador;

            if (_appUser.IsAuthenticated())
            {
                UsuarioId = appUser.GetUserId();
                UsuarioAutenticado = true;
            }
        }
        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }
       
       protected ActionResult CustomReponse(object result = null)
       {
            if (OperacaoValida())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }
            return BadRequest(new
            {
                success = false,
                errors = _notificador.ObterNotificacoes().Select(x => x.Mensagem)
            }) ;
       }
        protected ActionResult CustomReponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotificarErrorModelInvalida(modelState);
            return CustomReponse();
        }
        protected void NotificarErrorModelInvalida(ModelStateDictionary modelState)
        {
            var toTakeErros = modelState.Values.SelectMany(x => x.Errors);
            foreach (var error in toTakeErros)
            {
                var receiveError = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                NotificarErro(receiveError);
            }
        }
        protected void NotificarErro(string message)
        {
            _notificador.Handle(new Notificacao(message));
        }

    }
}