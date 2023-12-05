using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using PicPaySimplificado.Core.DomainObjects;
using PicPaySimplificado.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPaySimplificado.Data.Mappings
{
    public class UserMongoMap 
    {

        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Entity>(map =>
            {
                map.AutoMap();
                map.MapIdProperty(x => x.Id);
                map.SetIsRootClass(true);
            });
            BsonClassMap.RegisterClassMap<User>(map =>
            {
                map.MapProperty(u => u.Document)
                   .SetElementName("Document");

                map.MapProperty(u => u.UserType);

                map.MapProperty(u => u.Email)
                   .SetElementName("Email");

                map.MapProperty(u => u.Name)
                   .SetElementName("Name");

                map.MapProperty(u => u.Password)
                   .SetElementName("Password");

                map.MapProperty(u => u.TransactionsAsPayer)
                   .SetElementName("TransactionsAsPayer");

                map.MapProperty(u => u.TransactionsAsPayee)
                   .SetElementName("TransactionsAsPayee");
            });
        }
    }
}
