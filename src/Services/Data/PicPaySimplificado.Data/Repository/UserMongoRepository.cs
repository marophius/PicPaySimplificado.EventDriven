using PicPaySimplificado.Domain.Entities;
using PicPaySimplificado.Domain.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PicPaySimplificado.Domain.Enums;

namespace PicPaySimplificado.Data.Repository
{
    public class UserMongoRepository : IUserMongoRepository
    {
        private readonly ApplicationMongoContext _mongoContext;

        public UserMongoRepository(ApplicationMongoContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        public async Task CreateUserMongo(User user)
        {
            await _mongoContext.Users.InsertOneAsync(user);
        }

        public async Task DeleteUserMongo(Guid id)
        {
            await _mongoContext.Users.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
           return await _mongoContext.Users.Find(x => true).ToListAsync();
        }

        public async Task<Transaction> GetPayerTransactionById(Guid id, Guid userId)
        {
            var result = from user in _mongoContext.Users.AsQueryable()
                                                  where user.Id == userId
                                                  from Transactions in user.TransactionsAsPayer
                                                  where Transactions.Id == id
                                                  select Transactions;

            return await result.FirstOrDefaultAsync();
        }

        public async Task<Transaction> GetPayeeTransactionById(Guid id, Guid userId)
        {
            var result = from user in _mongoContext.Users.AsQueryable()
                         where user.Id == userId
                         from Transactions in user.TransactionsAsPayee
                         where Transactions.Id == id
                         select Transactions;

            return await result.FirstOrDefaultAsync();
        }

        public async Task<User> GetUserById(Guid id)
        {
            var result = await (from user in _mongoContext.Users.AsQueryable()
                          where user.Id == id
                          select user).FirstOrDefaultAsync();

            return result;
        }

        public async Task<IEnumerable<Transaction>> GetUserPayeeTransactions(Guid payeeId)
        {
            var result = (from user in _mongoContext.Users.AsQueryable()
                          where user.Id ==  payeeId
                          from transactions in user.TransactionsAsPayee
                          select transactions).ToListAsync();
            return await result;
        }

        public async Task<IEnumerable<Transaction>> GetUserPayerTransactions(Guid payerId)
        {
            var result = (from user in _mongoContext.Users.AsQueryable()
                          where user.Id == payerId
                          from transactions in user.TransactionsAsPayer
                          select transactions).ToListAsync();
            return await result;
        }

        public async Task UpdateUserMongo(User user)
        {
            await _mongoContext.Users.ReplaceOneAsync(x => x.Id == user.Id, user);
        }
    }
}
