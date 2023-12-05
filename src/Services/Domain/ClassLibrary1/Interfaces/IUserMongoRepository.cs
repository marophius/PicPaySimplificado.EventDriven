using PicPaySimplificado.Core.Data;
using PicPaySimplificado.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPaySimplificado.Domain.Interfaces
{
    public interface IUserMongoRepository : IMongoRepository<User>
    {
        Task CreateUserMongo(User user);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(Guid id);
        Task UpdateUserMongo(User user);
        Task DeleteUserMongo(Guid id);

        // Transactions
        Task<IEnumerable<Transaction>> GetUserPayeeTransactions(Guid payeeId);
        Task<IEnumerable<Transaction>> GetUserPayerTransactions(Guid payerId);
        Task<Transaction> GetPayerTransactionById(Guid id, Guid userId);
        Task<Transaction> GetPayeeTransactionById(Guid id, Guid userId);
    }
}
