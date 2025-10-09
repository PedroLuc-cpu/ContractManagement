using ContractManagement.Application.Contracts.Repository.IPedido;
using ContractManagement.Application.Contracts.Services;
using ContractManagement.Domain.Common.Exceptions;
using ContractManagement.Domain.Entity.Pedido;
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
            LimparErrosProcessamento();
            try
            {
                var pedidos = await _pedidoRepository.GetAllAsync();
                if (!pedidos.Any())
                {
                    return CustomResponse("Não foi encontrado nenhum pedido");
                }
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();
            }

        }
        [HttpGet("id:{id:guid}")]
        [ProducesResponseType(typeof(PedidoEntity), 200)]

        public async Task<IActionResult> Carregar(Guid id)
        {
            LimparErrosProcessamento();
            try
            {
                var pedido = await _pedidoRepository.GetByIdAsync(id);
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
        [HttpPost]
        [ProducesResponseType(typeof(PedidoEntity), 201)]
        public async Task<IActionResult> Inserir()
        {
            LimparErrosProcessamento();
            try
            {
                var pedido = await _pedidoService.CriarPedido();
                return Ok(pedido);
            }
            catch (DomainException ex)
            {
                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();

            }

        }
        [HttpPut("{id}/adicionar")]
        [ProducesResponseType(typeof(PedidoEntity), 201)]

        public async Task<IActionResult> AdicionarValor(Guid id, [FromBody] decimal valor)
        {
            LimparErrosProcessamento();
            try
            {
                await _pedidoService.AdicionarValor(id, valor);
                return Ok("Valor do pedido foi adicionado.");
            }
            catch (DomainException ex)
            {
                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();
            }
        }
    }
}
