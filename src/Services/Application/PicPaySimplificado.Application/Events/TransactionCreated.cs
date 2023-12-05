using PicPaySimplificado.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPaySimplificado.Application.Events
{
    public class TransactionCreated : Event
    {
        public TransactionCreated(
            Guid transactionId,
            Guid payerId, 
            decimal value, 
            Guid payeeId,
            DateTimeOffset transactionDate)
        {
            TransactionId = transactionId;
            PayerId = payerId;
            Value = value;
            PayeeId = payeeId;
            TransactionDate = transactionDate;
        }

        public Guid TransactionId { get; private set; }
        public Guid PayerId { get; private set; }
        public decimal Value { get; private set; }
        public Guid PayeeId { get; private set; }
        public DateTimeOffset TransactionDate { get; private set; }
    }
}
