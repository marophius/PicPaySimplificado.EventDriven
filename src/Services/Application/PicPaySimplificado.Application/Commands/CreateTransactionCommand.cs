using PicPaySimplificado.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPaySimplificado.Application.Commands
{
    public class CreateTransactionCommand : Command
    {
        public CreateTransactionCommand(
            Guid payerId, 
            decimal value, 
            Guid payeeId)
        {
            PayerId = payerId;
            Value = value;
            PayeeId = payeeId;
        }

        public Guid PayerId { get; private set; }
        public decimal Value { get; private set; }
        public Guid PayeeId { get; private set; }
    }
}
