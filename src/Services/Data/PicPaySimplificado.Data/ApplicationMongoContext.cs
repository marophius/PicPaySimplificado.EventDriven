using MongoDB.Driver;
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
            string databaseName)
        {
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
    }
}
