using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PicPaySimplificado.Application.Settings;
using PicPaySimplificado.Data;
using PicPaySimplificado.Data.DatabaseConfig;
using PicPaySimplificado.Data.Repository;
using PicPaySimplificado.Domain.Interfaces;
using PicPaySimplificado.TransactionConsumer.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<MessageBrokerSettings>(builder.Configuration.GetSection(nameof(MessageBrokerSettings)));
builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);
var client = new MongoClient(builder.Configuration.GetConnectionString("PicPaySimplificadoMongoDb"));
builder.Services.AddSingleton<IMongoClient>(client);
builder.Services.AddScoped<ApplicationMongoContext>();
builder.Services.AddScoped<IUserMongoRepository, UserMongoRepository>();
// Configure the HTTP request pipeline.
builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();
    x.AddConsumer<CreatedTransactionConsumer>();
    x.UsingRabbitMq((context, busFactoryConfigurator) =>
    {
        MessageBrokerSettings messageBrokerSettings = context.GetRequiredService<MessageBrokerSettings>();
        busFactoryConfigurator.Host(new Uri(messageBrokerSettings.Host), h =>
        {
            h.Username(messageBrokerSettings.Username);
            h.Password(messageBrokerSettings.Password);
        });

        busFactoryConfigurator.ConfigureEndpoints(context);
    });
});

var app = builder.Build();


app.Run();