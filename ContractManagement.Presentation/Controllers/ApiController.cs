using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContractManagement.Presentation.Controllers
{
    public abstract class ApiController(ISender sender) : ControllerBase
    {
        protected readonly ISender Sender = sender;
    }
}
