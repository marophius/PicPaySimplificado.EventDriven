using MassTransit;
using MassTransit.Transports.Fabric;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PicPaySimplificado.Application.Commands;
using PicPaySimplificado.Application.Events;
using PicPaySimplificado.Application.Settings;
using PicPaySimplificado.Core.Communication;
using PicPaySimplificado.Data;
using PicPaySimplificado.Data.DatabaseConfig;
using PicPaySimplificado.WebApi.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("PicPaySimplificadoSqlServer"),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                });
});
builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));
builder.Services.Configure<MessageBrokerSettings>(builder.Configuration.GetSection(nameof(MessageBrokerSettings)));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);
// builder.Services.AddMediator();
builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();
    x.UsingRabbitMq((context, busFactoryConfigurator) =>
    {
        MessageBrokerSettings messageBrokerSettings = context.GetRequiredService<MessageBrokerSettings>();
        busFactoryConfigurator.Host(new Uri(messageBrokerSettings.Host), h =>
        {
            h.Username(messageBrokerSettings.Username);
            h.Password(messageBrokerSettings.Password);
        });
    });
});
builder.Services.ResolveDependencies(builder.Configuration);
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
    context.Database.Migrate();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
