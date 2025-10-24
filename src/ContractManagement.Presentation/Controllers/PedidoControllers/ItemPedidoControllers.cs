using ContractManagement.Application.Command.Product;
using ContractManagement.Application.Query.GetItemOrderById;
using ContractManagement.Application.Query.GetItemsOrders;
using ContractManagement.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContractManagement.Presentation.Controllers.PedidoControllers
{
    [Route("item-pedido")]
    [Produces("application/json")]
    public sealed class ItemPedidoControllers(ISender sender) : ApiController(sender)
    {
        [HttpPost]
        public async Task<IActionResult> RegisterItemOrder([FromBody] ItemOrderResponse item, CancellationToken cancellationToken)
        {
            var command = new CreateProductCommand(item.ProductName, item.Quantity, item.UnitPrice);
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

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetItemsOrders(CancellationToken cancellationToken)
        {
            var query = new GetItemsOrdersQuery();
            Result<List<ItemsOrdersResponse>> response = await Sender.Send(query, cancellationToken);
            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
            
            
        }        

    }

    
}
