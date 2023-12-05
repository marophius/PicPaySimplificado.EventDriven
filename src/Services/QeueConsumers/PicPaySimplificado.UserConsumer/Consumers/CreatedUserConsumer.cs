using MassTransit;
using PicPaySimplificado.Application.Events;
using PicPaySimplificado.Domain.Entities;
using PicPaySimplificado.Domain.Interfaces;

namespace PicPaySimplificado.UserConsumer.Consumers
{
    public class CreatedUserConsumer : IConsumer<UserCreated>
    {
        private readonly IUserMongoRepository _userMongoRepository;

        public CreatedUserConsumer(IUserMongoRepository userMongoRepository)
        {
            _userMongoRepository = userMongoRepository;
        }

        public async Task Consume(ConsumeContext<UserCreated> context)
        {
            try
            {
                var @event = context.Message;

                if (@event is null) return;

                var user = new User(@event.AgregateId, @event.FirstName, @event.LastName, @event.Document, @event.Email, @event.Password, @event.Balance, @event.UserType, @event.RegisterDate);

                await _userMongoRepository.CreateUserMongo(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
