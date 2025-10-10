using ContractManagement.Application.Contracts.Services;
using ContractManagement.Application.Interfaces.Repository.IPedido;
using ContractManagement.Domain.Common.Exceptions;
using ContractManagement.Domain.Entity.Pedido;
using Microsoft.AspNetCore.Mvc;

namespace ContractManagement.Api.Controllers.PedidoControllers
{
    [Route("pedido")]
    [Produces("application/json")]
    public class PedidoController(IPedidoRepository pedidoRepository, IPedidoService pedidoService, IPedidoItemRepository pedidoItemRepository ) : MainController
    {
        private readonly IPedidoRepository _pedidoRepository = pedidoRepository;
        private readonly IPedidoItemRepository _pedidoItemRepository = pedidoItemRepository;
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
        public async Task<IActionResult> Inserir([FromBody] string numero)
        {
            LimparErrosProcessamento();
            try
            {
                var pedido = await _pedidoService.CriarPedido(numero);
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

        [HttpGet("pedido-com-items/id:{id:guid}")]
        [ProducesResponseType(typeof(ItemPedidoEntity), 200)]
        public async Task<IActionResult> ListarPedidoComItems(Guid id)
        {
            LimparErrosProcessamento();
            try
            {
                var pedidoItems = await _pedidoRepository.ListarComItens(id);
                if (pedidoItems.Items.Count <= 0)
                {
                    AdicionarErroProcessamento("Não foi encontrado nenhum pedido com esse identificador");
                    return CustomResponse();
                }
                return Ok(pedidoItems);
            }
            catch (DomainException ex)
            {
                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();
            }
        }

        [HttpGet("pedido-items/id:{id:guid}")]
        [ProducesResponseType(typeof(ItemPedidoEntity), 200)]
        public async Task<IActionResult> ListarPedidoItems(Guid id)
        {
            LimparErrosProcessamento();
            try
            {
                var pedidoItems = await _pedidoItemRepository.GetItemPedidoByIdAsync(id);
                if (pedidoItems.Id != id)
                {
                    AdicionarErroProcessamento("Não foi encontrado nenhum pedido com esse identificador");
                    return CustomResponse();
                }
                return Ok(pedidoItems);
            }
            catch (DomainException ex)
            {
                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();
            }
        }


        [HttpPost("{id}/adicionar-item-pedido")]
        [ProducesResponseType(typeof(ItemPedidoEntity), 201)]
        public async Task<IActionResult> AdicionarItemPedido(Guid id, Guid produtoId,  string nomeProduto, int quantidade, decimal precoUnitario)
            { 
            LimparErrosProcessamento();
            try
            {
                var pedido = await _pedidoRepository.GetByIdAsync(id);
                if (pedido.Id == id)
                {
                    await _pedidoService.AdicionarItemPedido(id, produtoId, nomeProduto, quantidade, precoUnitario);
                    return Ok("Item adicionado no pedido");
                }
                AdicionarErroProcessamento("Ocorreu um erro de adicionar novo item no pedido");
                return CustomResponse();                
                
            }
            catch (DomainException ex)
            {
                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();
            }
        }
    }
}
