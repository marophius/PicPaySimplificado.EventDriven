using PicPaySimplificado.Application.Queries.DTOs;
using PicPaySimplificado.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPaySimplificado.Application.Queries.Extensions
{
    public static class TransactionExtension
    {
        public static TransactioDTO ToDTO(this Transaction trans)
        {
            return new TransactioDTO(trans.Id, trans.PayerId, trans.PayeeId, trans.TransactionDate, trans.Value);
        }

        public static List<TransactioDTO> ToDTOList(this List<Transaction> transList)
        {
            return  transList.Select(x => x.ToDTO()).ToList();
        }
    }
}
