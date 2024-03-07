using RestApiV1.Domain.Notification;

namespace RestApiV1.Domain.Interfaces.Notification
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}
