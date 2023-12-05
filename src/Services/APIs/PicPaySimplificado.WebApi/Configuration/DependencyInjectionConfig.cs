using Mediator;
using MongoDB.Driver;
using PicPaySimplificado.Application;
using PicPaySimplificado.Application.Commands;
using PicPaySimplificado.Application.Queries;
using PicPaySimplificado.Application.Queries.DTOs;
using PicPaySimplificado.Core;
using PicPaySimplificado.Core.Communication;
using PicPaySimplificado.Core.Messages.CommonMessages.Notifications;
using PicPaySimplificado.Data;
using PicPaySimplificado.Data.Mappings;
using PicPaySimplificado.Data.Repository;
using PicPaySimplificado.Domain.Interfaces;
using PicPaySimplificado.Domain.Interfaces.Services;
using PicPaySimplificado.Infrastructure.Services;

namespace PicPaySimplificado.WebApi.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            #region Databases
            var client = new MongoClient(configuration.GetConnectionString("PicPaySimplificadoMongoDb"));
            services.AddSingleton<IMongoClient>(client);
            UserMongoMap.Configure();
            services.AddScoped<ApplicationMongoContext>();
            services.AddScoped<ApplicationDbContext>();
            #endregion

            #region HttpClient
            services.AddHttpClient<IAuthorizationService, AuthorizationService>((httpClient) =>
            {
                httpClient.BaseAddress = new Uri("https://run.mocky.io/v3");
            });
            services.AddHttpClient<INotificationService, NotificationService>((httpClient) =>
            {
                httpClient.BaseAddress = new Uri("http://o4d9z.mocklab.io");
            });
            #endregion
            services.AddScoped<IUserRepository, UserRepository>();
            
            services.AddScoped<IUserMongoRepository, UserMongoRepository>();
            services.AddCore();
            services.AddAplication();
            
            return services;
        }
    }
}
