using PicPaySimplificado.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPaySimplificado.Application.Events
{
    public class UserRemoved : Event
    {
        public UserRemoved(Guid id)
        {
            AgregateId = id;
        }
    }
}
