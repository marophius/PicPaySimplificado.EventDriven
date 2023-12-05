using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPaySimplificado.Application.Queries.DTOs
{
    public record TransactioDTO(Guid Id, Guid PayerId, Guid PayeeId, DateTimeOffset TransactionDate, decimal Value);
}
