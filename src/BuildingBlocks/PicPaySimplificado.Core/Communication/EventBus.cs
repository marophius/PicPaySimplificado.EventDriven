using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPaySimplificado.Core.Communication
{
    public class EventBus : IEventBus
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public EventBus(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }
        public async Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : PicPaySimplificado.Core.Messages.Event
        {
           await _publishEndpoint.Publish(message, cancellationToken);
        }
    }
}
