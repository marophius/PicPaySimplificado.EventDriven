using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPaySimplificado.Core.Messages.CommonMessages.Notifications
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        private List<DomainNotification> _domainNotifications;

        public DomainNotificationHandler()
        {
            _domainNotifications = new List<DomainNotification>();
        }

        public ValueTask Handle(DomainNotification notification, CancellationToken cancellationToken)
        {
            _domainNotifications.Add(notification);
            return ValueTask.CompletedTask;
        }

        public virtual List<DomainNotification> ObterNotificacoes()
        {
            return _domainNotifications;
        }

        public virtual bool TemNotificacao()
        {
            return ObterNotificacoes().Any();
        }

        public void Dispose()
        {
            _domainNotifications = new List<DomainNotification>();
        }
    }
}
