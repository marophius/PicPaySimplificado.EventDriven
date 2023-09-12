using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PicPaySimplificado.Data.DatabaseConfig;
using PicPaySimplificado.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPaySimplificado.Data
{
    public class ApplicationMongoContext
    {
        private readonly IMongoDatabase _database;

        public ApplicationMongoContext(IMongoClient client, 
            IOptions<MongoDbConfig> config)
        {
            _database = client.GetDatabase(config.Value.DatabaseName);
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
    }
}
