using PicPaySimplificado.Application.Queries.Extensions;
using PicPaySimplificado.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPaySimplificado.Application.Queries.DTOs
{
    public class UserQueries : IUserQueries
    {
        private readonly IUserMongoRepository _repository;

        public UserQueries(IUserMongoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TransactioDTO>> GetAllPayeeTransactions(Guid userId)
        {
            var listDTO = (await _repository.GetUserPayeeTransactions(userId)).ToList();
            return listDTO.ToDTOList();
        }

        public async Task<List<TransactioDTO>> GetAllPayerTransactions(Guid userId)
        {
            var listDTO = (await _repository.GetUserPayerTransactions(userId)).ToList();

            return listDTO.ToDTOList();
        }

        public async Task<TransactioDTO> GetPayeeTransactionById(Guid id, Guid userId)
        {
            var trans = await _repository.GetPayeeTransactionById(id, userId);

            return trans.ToDTO();
        }

        public async Task<TransactioDTO> GetPayerTransactionById(Guid id, Guid userId)
        {
            var trans = await _repository.GetPayerTransactionById(id, userId);

            return trans.ToDTO();
        }

        public async Task<UserDTO> GetUserById(Guid id)
        {
            var user = await _repository.GetUserById(id);

            return user.ToDTO();
        }
    }
}
