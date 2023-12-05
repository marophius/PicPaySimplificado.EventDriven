using Microsoft.Extensions.DependencyInjection;
using PicPaySimplificado.Application.Queries.DTOs;
using PicPaySimplificado.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediator;
using PicPaySimplificado.Application.Commands;

namespace PicPaySimplificado.Application
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddAplication(this IServiceCollection services)
        {
            services.AddMediator();
            services.AddScoped<IUserQueries, UserQueries>();
            services.AddScoped<IRequestHandler<CreateUserCommand, bool>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateUserCommand, bool>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveUserCommand, bool>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<CreateTransactionCommand, bool>, UserCommandHandler>();
            

            return services;
        }
    }
}
