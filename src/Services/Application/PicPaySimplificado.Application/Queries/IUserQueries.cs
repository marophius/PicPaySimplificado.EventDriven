using PicPaySimplificado.Application.Queries.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPaySimplificado.Application.Queries
{
    public interface IUserQueries
    {
        Task<UserDTO> GetUserById(Guid id);
        Task<TransactioDTO> GetPayerTransactionById(Guid id, Guid userId);
        Task<TransactioDTO> GetPayeeTransactionById(Guid id, Guid userId);
        Task<List<TransactioDTO>> GetAllPayerTransactions(Guid userId);
        Task<List<TransactioDTO>> GetAllPayeeTransactions(Guid userId);
    }
}
