using ContractManagement.Application.Order.Command;
using ContractManagement.Application.Order.Query;
using ContractManagement.Domain.Entity.Pedidos;
using ContractManagement.Domain.Interfaces.Repository.Pedidos;
using ContractManagement.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContractManagement.Presentation.Controllers
{
    /// <summary>
    /// rota de pedidos
    /// </summary>  
    [Route("order")]
    [Produces("application/json")]
    [Authorize]
    public sealed class PedidoController(IPedidoRepository pedidoRepository, ISender sender) : MainController
    {
        private readonly IPedidoRepository _pedidoRepository = pedidoRepository;
        private readonly ISender _sender = sender;

        /// <summary>
        /// Rota para criar pedido
        /// </summary>
        [HttpPost()]
        [ProducesResponseType(typeof(CreateOrderCommand), 201)]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand request, CancellationToken cancellationToken)
        {
            LimparErrosProcessamento();
            try
            {
                var command = new CreateOrderCommand(request.IdCliente, request.Itens);
                var result = await _sender.Send(command, cancellationToken);
                return result.IsSuccess ? Ok() : BadRequest(result.Error);
            }
            catch (Exception ex)
            {
                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();
            }
        }
        /// <summary>
        /// rota para buscar pedido por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("id:{id:guid}")]
        [ProducesResponseType(typeof(Pedido), 200)]

        public async Task<IActionResult> Carregar(Guid id)
        {
            LimparErrosProcessamento();
            try
            {
                var pedido = await _pedidoRepository.ObterPedidoPorId(id);
                if (pedido is not null)
                if (pedido.Id.Equals(id))
                {
                    return Ok(pedido);
                }
                return CustomResponse("Não foi encontrado nenhum pedido com esse identificador");

            } catch (Exception ex)
            {
                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();
            }

        }
        [HttpGet("pedidos/{pageNumber:int}/{pageSize:int}")]
        public async Task<IActionResult> ListarPedidos(int pageNumber = 1, int pageSize = 10)
        {
            LimparErrosProcessamento();
            try
            {
                var query = new GetOrdersQuery(pageNumber, pageSize);

                Result<IEnumerable<GetOrdersQueryResponse>> response = await _sender.Send(query);

                return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);

            }
            catch (Exception ex)
            {
                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();
            }
        }
    }
}
