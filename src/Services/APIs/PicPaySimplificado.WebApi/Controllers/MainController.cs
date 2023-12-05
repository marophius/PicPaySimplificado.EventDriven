using Mediator;
using Microsoft.AspNetCore.Mvc;
using PicPaySimplificado.Core.Communication;
using PicPaySimplificado.Core.Messages.CommonMessages.Notifications;

namespace PicPaySimplificado.WebApi.Controllers
{
    public abstract class MainController : ControllerBase
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediatorHandler;


        public MainController( INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediatorHandler = mediatorHandler;
        }

        protected bool OperacaoValida()
        {
            return !_notifications.TemNotificacao();
        }

        protected IEnumerable<string> ObterMensagensErro()
        {
            return _notifications.ObterNotificacoes().Select(c => c.Value).ToList();
        }

        protected void NotificarErro(string codigo, string mensagem)
        {
            _mediatorHandler.PublishNotification(new DomainNotification(codigo, mensagem));
        }
    }
}
