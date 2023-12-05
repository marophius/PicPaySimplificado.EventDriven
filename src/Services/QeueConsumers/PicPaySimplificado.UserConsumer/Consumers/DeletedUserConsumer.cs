using MassTransit;
using PicPaySimplificado.Application.Events;
using PicPaySimplificado.Domain.Interfaces;

namespace PicPaySimplificado.UserConsumer.Consumers
{
    public class DeletedUserConsumer : IConsumer<UserRemoved>
    {
        private readonly IUserMongoRepository _repository;

        public DeletedUserConsumer(IUserMongoRepository repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<UserRemoved> context)
        {
            try
            {
                var @event = context.Message;

                if (@event is null) return;

                await _repository.DeleteUserMongo(@event.AgregateId);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
