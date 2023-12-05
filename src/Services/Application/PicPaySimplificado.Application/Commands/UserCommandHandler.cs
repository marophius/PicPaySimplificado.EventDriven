using Mediator;
using PicPaySimplificado.Application.Events;
using PicPaySimplificado.Core.Communication;
using PicPaySimplificado.Core.Messages;
using PicPaySimplificado.Core.Messages.CommonMessages.Notifications;
using PicPaySimplificado.Domain.Entities;
using PicPaySimplificado.Domain.Interfaces;
using PicPaySimplificado.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPaySimplificado.Application.Commands
{
    public class UserCommandHandler :
        IRequestHandler<CreateUserCommand, bool>,
        IRequestHandler<UpdateUserCommand, bool>,
        IRequestHandler<RemoveUserCommand, bool>,
        IRequestHandler<CreateTransactionCommand, bool>
    {
        private readonly IUserRepository _repository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IEventBus _eventBus;
        private readonly INotificationService _notificationService;
        private readonly IAuthorizationService _authorizationService;

        public UserCommandHandler(
            IUserRepository repository, 
            IMediatorHandler mediatorHandler,
            IEventBus eventBus,
            INotificationService notificationService,
            IAuthorizationService authorizationService)
        {
            _repository = repository;
            _mediatorHandler = mediatorHandler;
            _eventBus = eventBus;
            _notificationService = notificationService;
            _authorizationService = authorizationService;
        }
        public async ValueTask<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request))
            {
                return false;
            }

            var userExists = await Task.WhenAny(_repository.GetByEmail(request.Email), _repository.GetByDocument(request.Document));

            if(userExists.Result is not null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, "There is already a user with this email or document."));
                return false;
            }

            var user = new User(request.FirstName, request.LastName, request.Document, request.Email, request.Password, request.Balance, request.UserType);

            await Task.Run(() => _repository.CreateUser(user));

            await _eventBus.PublishAsync(new UserCreated(user.Id, 
                request.FirstName, 
                request.LastName, 
                request.Email, 
                request.Document, 
                request.Password, 
                request.Balance, 
                request.UserType,
                user.RegisterDate));

            return await _repository.UnitOfWork.Commit();

        }

        public async ValueTask<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request)) return false;

            var user = await _repository.GetUserById(request.AgregateId);

            if(user is null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, "No user found with these data"));
                return false;
            }

            user.FullUpdate(request.FirstName, request.LastName, request.Document, request.Email, request.Password, request.Balance, request.UserType);

            await Task.Run(() => _repository.UpdateUser(user));

            await _eventBus.PublishAsync(new UserUpdated(request.AgregateId, request.FirstName, request.LastName, request.Email, request.Document, request.Password, request.Balance, request.UserType));

            return await _repository.UnitOfWork.Commit();
        }

        public async ValueTask<bool> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            await Task.Run(() => _repository.DeleteUser(request.AgregateId));

            await _eventBus.PublishAsync(new UserRemoved(request.AgregateId));

            return await _repository.UnitOfWork.Commit();
        }

        public async ValueTask<bool> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            if (request.Value <= 0) return false;

            var sender = await _repository.GetUserById(request.PayerId);
            var receiver = await _repository.GetUserById(request.PayeeId);
            if(sender is null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, "No payer found"));
                return false;
            }
            if ((receiver is null))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, "No payee found"));
                return false;
            }

            Transaction trans = new Transaction(request.Value, request.PayerId, request.PayeeId);

            if(!(await ValidateTransaction(sender, receiver, trans)))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, "Can't validate transaction."));
                return false;
            }
            
            await _eventBus.PublishAsync(new TransactionCreated(trans.Id, request.PayerId, request.Value, request.PayeeId, trans.TransactionDate));
            return await _repository.UnitOfWork.Commit();
        }

        private bool ValidateCommand(Command request)
        {
            if(request.IsValid()) return true;

            foreach(var error in request.ValidationResult.Errors)
            {
                _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, error.ErrorMessage));
            }

            return false;
        }

        private async Task<bool> ValidateTransaction(User sender, User receiver, Transaction trans)
        {
            sender.SendMoney(trans);
            receiver.ReceiveMoney(trans);

            bool authorizationCall = await _authorizationService.AuthorizaAsync();

            if (!authorizationCall)
            {
                return authorizationCall;
            }
            await Task.Run(() => _repository.UpdateUser(sender));
            await Task.Run(() => _repository.UpdateUser(receiver));
            await Task.Run(() => _repository.CreateTransaction(trans));
            await _notificationService.SendNotificationAsync(receiver, $"You received {trans.Value.ToString("C")}");

            return authorizationCall;
        }
    }
}
