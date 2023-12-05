using Mediator;
using Microsoft.Extensions.DependencyInjection;
using PicPaySimplificado.Core.Communication;
using PicPaySimplificado.Core.Messages.CommonMessages.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPaySimplificado.Core
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddMediator();
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IEventBus, EventBus>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            

            return services;
        }
    }
}
