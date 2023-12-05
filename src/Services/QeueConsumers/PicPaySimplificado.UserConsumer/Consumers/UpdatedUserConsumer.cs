using MassTransit;
using PicPaySimplificado.Application.Events;
using PicPaySimplificado.Domain.Interfaces;

namespace PicPaySimplificado.UserConsumer.Consumers
{
    public class UpdatedUserConsumer : IConsumer<UserUpdated>
    {
        private readonly IUserMongoRepository _repository;

        public UpdatedUserConsumer(IUserMongoRepository repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<UserUpdated> context)
        {
            try
            {
                var @event = context.Message;

                if (@event is null) return;

                var user = await _repository.GetUserById(@event.AgregateId);

                user.FullUpdate(@event.FirstName, @event.LastName, @event.Document, @event.Email, @event.Password, @event.Balance, @event.UserType);

                await _repository.UpdateUserMongo(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
