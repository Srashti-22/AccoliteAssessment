using AccoliteAssessment.Core.Users.Create;
using AccoliteAssessment.Core.Users.Delete;
using AccoliteAssessment.Core.Users.Get;
using AccoliteAssessment.Core.Users.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AccoliteAssessment.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetUserRequestModel() { });
            if (result.Count() > 0)
            {
                return Ok(result);
            }
            return NotFound("No User exists");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new GetUserByIdRequestModel() { UserId = id });
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("User Not Found");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddUserRequestModel user)
        {
            var result = await _mediator.Send(user);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Invalid data");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteUserRequestModel() { UserId = id });
            if (!string.IsNullOrEmpty(result))
            {
                return Ok(result);
            }
            return NotFound("User Not Found");
        }
    }
}
