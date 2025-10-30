using ContractManagement.Application.Order.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContractManagement.Presentation.Controllers.PedidoControllers
{
    [Route("item-pedido")]
    [Produces("application/json")]
    public sealed class ItemPedidoControllers(ISender sender) : ApiController(sender)
    {
        [HttpPost]
        public async Task<IActionResult> RegisterItemOrder([FromBody] CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok(command) : BadRequest(result.Error);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetItemOrder(Guid id, CancellationToken cancellationToken)
        //{
        //    var query = new GetItemOrderByIdQuery(id);
        //    Result<ItemOrderResponse> response = await Sender.Send(query, cancellationToken);
        //    return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetItemsOrders(CancellationToken cancellationToken)
        //{
        //    var query = new GetItemsOrdersQuery();
        //    Result<List<ItemsOrdersResponse>> response = await Sender.Send(query, cancellationToken);
        //    return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
            
            
        //}        

    }

    
}
