using ContractManagement.Application.Services.Contracts;
using ContractManagement.Domain.Entity.Pedido;
using ContractManagement.Infrastructure.Repository.Contracts.IPedido;
using Microsoft.AspNetCore.Mvc;

namespace ContractManagement.Api.Controllers.PedidoControllers
{
    [Route("pedido")]
    [Produces("application/json")]
    public class PedidoController(IPedidoRepository pedidoRepository, IPedidoService pedidoService ) : MainController
    {
        private readonly IPedidoRepository _pedidoRepository = pedidoRepository;
        private readonly IPedidoService _pedidoService = pedidoService;

        [HttpGet]
        [ProducesResponseType(typeof(PedidoEntity), 200)]

        public async Task<IActionResult> Carregar()
        {
            var pedidos = await _pedidoRepository.Listar();
            if (pedidos.Count > 0)
            {
                return Ok(pedidos);
            }
            return CustomResponse("Não foi encontrado nenhum pedido");

        }
        [HttpGet("id:{id:guid}")]
        [ProducesResponseType(typeof(PedidoEntity), 200)]

        public async Task<IActionResult> Carregar(Guid id)
        {
            var pedido = await _pedidoRepository.ObterPorId(id);
            if (pedido.Id.Equals(id))
            {
                return Ok(pedido);
            }
            return CustomResponse("Não foi encontrado nenhum pedido com esse identificador");
        }
        [HttpPost]
        [ProducesResponseType(typeof(PedidoEntity), 201)]
        public async Task<IActionResult> Inserir()
        {
            var pedido = await _pedidoService.CriarPedido();
            return Ok(pedido);

        }
        [HttpPut("{id}/adicionar")]
        [ProducesResponseType(typeof(PedidoEntity), 201)]

        public async Task<IActionResult> AdicionarValor(Guid id, [FromBody] decimal valor)
        {
            await _pedidoService.AdicionarValor(id, valor);
            return Ok("Valor do pedido foi adicionado.");
        }
    }
}
