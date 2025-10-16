using ContractManagement.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContractManagement.Presentation.Controllers
{
    public abstract class ApiController(ISender sender) : ControllerBase
    {
        protected readonly ISender Sender = sender;

        protected IActionResult HandlerFailure(Result result)
        {
            if (result is null)
            {
                throw new ArgumentNullException(nameof(result));
            }
            return null;
        }
    }
}
