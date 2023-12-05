using MassTransit;
using PicPaySimplificado.Application.Events;
using PicPaySimplificado.Domain.Entities;
using PicPaySimplificado.Domain.Interfaces;

namespace PicPaySimplificado.TransactionConsumer.Consumers
{
    public class CreatedTransactionConsumer : IConsumer<TransactionCreated>
    {
        private readonly IUserMongoRepository _repository;

        public CreatedTransactionConsumer(IUserMongoRepository repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<TransactionCreated> context)
        {
            try
            {
                var message =  context.Message;

                if (message is null) return;

                var trans = new Transaction(message.TransactionId, message.Value, message.PayerId, message.PayeeId, message.TransactionDate);

                var payer = await _repository.GetUserById(trans.PayerId);

                var payee = await _repository.GetUserById(trans.PayeeId);

                payer.SendMoney(trans);

                payee.ReceiveMoney(trans);

                await _repository.UpdateUserMongo(payer);
                await _repository.UpdateUserMongo(payee);

            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
