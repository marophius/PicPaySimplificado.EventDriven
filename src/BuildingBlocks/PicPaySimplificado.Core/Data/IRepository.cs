using PicPaySimplificado.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPaySimplificado.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAgregateRoot
    {
    }
}
