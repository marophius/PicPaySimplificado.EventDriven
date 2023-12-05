using PicPaySimplificado.Core.Data;
using PicPaySimplificado.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPaySimplificado.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        // Users
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(Guid id);
        Task<User> GetByDocument(string document);
        Task<User> GetByEmail(string email);
        void CreateUser(User user);
        
        void UpdateUser(User user);
        
        void DeleteUser(Guid id);
        

        // Transactions
        Task<IEnumerable<Transaction>> GetAllTransactions();
        Task<IEnumerable<Transaction>> GetUserPayeeTransactions(Guid payeeId);
        Task<IEnumerable<Transaction>> GetUserPayerTransactions(Guid payerId);
        Task<IEnumerable<Transaction>> GetTransactionsOrdered(DateTime date);
        Task<Transaction> GetTransactionById(Guid id);
        void CreateTransaction(Transaction transaction);
    }
}
