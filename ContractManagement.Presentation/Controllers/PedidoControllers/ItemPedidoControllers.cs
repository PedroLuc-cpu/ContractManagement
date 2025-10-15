using ContractManagement.Application.Command.Product;
using ContractManagement.Application.Query.GetItemOrderById;
using ContractManagement.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContractManagement.Presentation.Controllers.PedidoControllers
{
    [Route("item-pedido")]
    public sealed class ItemPedidoControllers : ApiController
    {
        public ItemPedidoControllers(ISender sender) : base(sender) { }

        [HttpPost]
        public async Task<IActionResult> RegisterItemOrder(CancellationToken cancellationToken)
        {
            var command = new CreateProductCommand("Carne", 1, 20);
            var result = await Sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok(command) : BadRequest(result.Error);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemOrder(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetItemOrderByIdQuery(id);
            Result<ItemOrderResponse> response = await Sender.Send(query, cancellationToken);
            return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
        }

    }

    
}
