using ContractManagement.Application.Members.CreateMember;
using ContractManagement.Application.Members.Login;
using ContractManagement.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContractManagement.Presentation.Controllers
{
    [Route("member")]
    public sealed class MembersController(ISender sender) : ApiController(sender)
    {
        [HttpPost("login")]
        public async Task<IActionResult> LoginMember([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            var command = new LoginCommand(request.Email);
            Result<string> tokenResult = await Sender.Send(command, cancellationToken);

            if (tokenResult.IsFailure)
            {
                return HandlerFailure(tokenResult);
            }
            return Ok(tokenResult.Value);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterMember(CancellationToken cancellationToken)
        {
            var command = new CreateMemberCommand(
                "milan@milanjovanic.tech",
                "Milan",
                "Jovanovic");

            var result = await Sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }
    }
}
