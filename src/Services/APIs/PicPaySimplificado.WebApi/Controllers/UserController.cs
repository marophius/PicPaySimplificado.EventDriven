using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PicPaySimplificado.Application.Commands;
using PicPaySimplificado.Application.Queries;
using PicPaySimplificado.Core.Communication;
using PicPaySimplificado.Core.Messages.CommonMessages.Notifications;
using PicPaySimplificado.Domain.Interfaces;

namespace PicPaySimplificado.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : MainController
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserQueries _queries;
        private readonly IMediatorHandler _mediatorHandler;
        public UserController(
            INotificationHandler<DomainNotification> notifications, 
            IMediatorHandler mediatorHandler,
            IUserRepository userRepository,
            IUserQueries queries) : base(notifications, mediatorHandler)
        {
            _userRepository = userRepository;
            _queries = queries;
            _mediatorHandler = mediatorHandler;
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var result = await _mediatorHandler.SendCommand(command);

            if(!result) return BadRequest(result);

            return Ok(new { Status = 200, Success = result }) ;
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser([FromBody] UpdateUserCommand command)
        {
            var result = await _mediatorHandler.SendCommand(command);

            if (!result) return BadRequest(result);

            return Ok(new { Status = 200, StatusMessage = result });
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetUserById([FromRoute] Guid id)
        {
            var user = await _queries.GetUserById(id);

            return Ok(user);
        }

        [HttpPost("transactions")]
        public async Task<ActionResult> CreateTransaction([FromBody] CreateTransactionCommand command)
        {
            var result = await _mediatorHandler.SendCommand(command);

            if (!result) return BadRequest(result);

            return Ok(new { Status = 200, StatusMessage = result });
        }

        [HttpGet("transactions/payer-transaction/{id:guid}/{userId:guid}")]
        public async Task<ActionResult> GetPayerTransactionById(
            [FromRoute] Guid id,
            [FromRoute] Guid userId)
        {
            var trans = await _queries.GetPayerTransactionById(id, userId);

            if(trans is null) return NotFound();

            return Ok(new {Status = 200, Data = trans });
        }

        [HttpGet("transactions/payee-transaction/{id:guid}/{userId:guid}")]
        public async Task<ActionResult> GetPayeeTransactionById(
            [FromRoute] Guid id,
            [FromRoute] Guid userId)
        {
            var trans = await _queries.GetPayeeTransactionById(id, userId);

            if (trans is null) return NotFound();

            return Ok(new { Status = 200, Data = trans });
        }

        [HttpGet("transactions/payer-transactions/{userId:guid}")]
        public async Task<ActionResult> GetPayerTransactions([FromRoute] Guid userId)
        {
            var list = await _queries.GetAllPayerTransactions(userId);

            if(list is null) return NotFound();

            return Ok(new { Status = 200, Data = list });
        }

        [HttpGet("transactions/payee-transactions/{userId:guid}")]
        public async Task<ActionResult> GetPayeeTransactions([FromRoute] Guid userId)
        {
            var list = await _queries.GetAllPayeeTransactions(userId);

            if (list is null) return NotFound();

            return Ok(new { Status = 200, Data = list });
        }
    }
}
