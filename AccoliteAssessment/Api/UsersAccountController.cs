using AccoliteAssessment.Core.UsersAccount.CreateAccountByUserId;
using AccoliteAssessment.Core.UsersAccount.GetAccountByUserId;
using AccoliteAssessment.Core.UsersAccount.UpdateAccountByUserId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AccoliteAssessment.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersAccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersAccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{userId}/{accountId}")]
        public async Task<IActionResult> Get(Guid userId, Guid accountId)
        {
            var result = await _mediator.Send(new GetAccountByUserIdRequestModel() { UserId = userId, AccountId = accountId });
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("Account Not Found");
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> Post(Guid userId, [FromBody] CreateAccountByUserIdRequestModel account)
        {
            account.UserId = userId;
            var result = await _mediator.Send(account);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPut("{userId}/{accountId}")]
        public async Task<IActionResult> Put(Guid userId, Guid accountId, [FromBody] UpdateAccountByUserIdRequestModel account)
        {
            account.UserId = userId;
            account.AccountId = accountId;
            var result = await _mediator.Send(account);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpDelete("{userId}/{accountId}")]
        public async Task<IActionResult> Delete(Guid userId, Guid accountId)
        {
            var result = await _mediator.Send(new DeleteAccountByUserIdRequestModel() { UserId = userId, AccountId = accountId });
            if (!string.IsNullOrEmpty(result))
            {
                return Ok(result);
            }
            return NotFound("Account Not Found");
        }
    }
}
