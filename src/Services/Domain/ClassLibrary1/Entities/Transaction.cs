using PicPaySimplificado.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPaySimplificado.Domain.Entities
{
    public class Transaction : Entity
    {
        protected Transaction() { }

        public Transaction(
            decimal value,
            Guid payerId,
            Guid payeeId)
        {
            TransactionDate = DateTime.Now;
            ValidateValue(value);
            PayerId = payerId;
            PayeeId = payeeId;
        }

        public Guid PayerId { get; private set; }

        public virtual User PayerUser { get; set; }

        public Guid PayeeId { get; private set; }
        public virtual User PayeeUser { get; set; }

        public decimal Value { get; private set; }

        public DateTimeOffset TransactionDate { get; private set; }

        private void ValidateValue(decimal value)
        {
            if (value <= 0)
            {
                throw new DomainException(DomainExceptionMessages.TransactionValidateErrorMessage);
            }

            Value = value;
        }
    }
}
