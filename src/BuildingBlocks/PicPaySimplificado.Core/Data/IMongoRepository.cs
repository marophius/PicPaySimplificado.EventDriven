using PicPaySimplificado.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPaySimplificado.Core.Data
{
    public interface IMongoRepository<T> where T : IAgregateRoot
    {
    }
}
