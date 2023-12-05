using PicPaySimplificado.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPaySimplificado.Application.Commands
{
    public class RemoveUserCommand : Command
    {
        public RemoveUserCommand(Guid agregateId)
        {
            AgregateId = agregateId;
        }
    }
}
