using Mediator;
using PicPaySimplificado.Core.Messages;
using PicPaySimplificado.Core.Messages.CommonMessages.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPaySimplificado.Core.Communication
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly ISender _sender;
        private readonly IPublisher _publisher;

        public MediatorHandler(
            ISender sender, 
            IPublisher publisher)
        {
            _sender = sender;
            _publisher = publisher;

        }

        public async Task PublishNotification<T>(T notification) where T : DomainNotification
        {
            await _publisher.Publish(notification);
        }

        public async Task<bool> SendCommand<T>(T command) where T : Command
        {
            return await _sender.Send(command);
        }
    }
}
