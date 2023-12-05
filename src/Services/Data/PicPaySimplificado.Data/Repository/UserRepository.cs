using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using PicPaySimplificado.Core.Data;
using PicPaySimplificado.Domain.Entities;
using PicPaySimplificado.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPaySimplificado.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        
        public IUnitOfWork UnitOfWork => (IUnitOfWork)_context;
        public UserRepository(
            ApplicationDbContext context,
            ApplicationMongoContext mongoContext
            )
        {

            _context = context;
        }
        public void CreateUser(User user)
        {
            _context.Users.Add(user);
        }

        public void UpdateUser(User user)
        {
            _context.Update(user);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var list = await _context.Users.ToListAsync();

            return list;
        }

        public async void DeleteUser(Guid id)
        {
            var user = await GetUserById(id);

            _context.Remove(user);
        }

        public async Task<User> GetByDocument(string document) => await _context.Users.FirstOrDefaultAsync(x => x.Document.Number == document);

        public async Task<User> GetByEmail(string email) => await _context.Users.FirstOrDefaultAsync(x => x.Email.Address == email);

        public async Task<User> GetUserById(Guid id) => await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<Transaction>> GetAllTransactions() => await _context.Transactions.ToListAsync();

        public async Task<Transaction> GetTransactionById(Guid id) => await _context.Transactions.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<Transaction>> GetTransactionsOrdered(DateTime date) => await _context.Transactions.Where(x => x.TransactionDate.Date == date)
                                                                                                                        .OrderBy(x => x.TransactionDate.Date == date)
                                                                                                                        .ThenBy(x => x.TransactionDate.TimeOfDay)
                                                                                                                        .ThenBy(x => x.TransactionDate.Minute)
                                                                                                                        .ThenBy(x => x.TransactionDate.Second)
                                                                                                                        .ToListAsync();

        public async Task<IEnumerable<Transaction>> GetUserPayeeTransactions(Guid payeeId) => await _context.Transactions.Where(x => x.PayeeId == payeeId)
                                                                                                                   .ToListAsync();

        public async Task<IEnumerable<Transaction>> GetUserPayerTransactions(Guid payerId) => await _context.Transactions.Where(x => x.PayerId == payerId)
                                                                                                                   .ToListAsync();

        public void Dispose()
        {
            _context.Dispose();
        }

        public void CreateTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
        }
    }
}
