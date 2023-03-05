using Dev.Bussines.Notificacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Bussines.Interfaces
{
    public interface INotificador
    {
        #region Public Methods

        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);

        #endregion Public Methods
    }
}
