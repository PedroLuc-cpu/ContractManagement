using ContractManagement.Domain.Common.Exceptions;
using ContractManagement.Domain.Entity.Pedidos;
using ContractManagement.Domain.Interfaces.Repository.Pedidos;
using ContractManagement.Domain.Interfaces.Services;
using ContractManagement.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace ContractManagement.Presentation.Controllers.PedidoControllers
{
    /// <summary>
    /// rota de pedidos
    /// </summary>  
    [Route("pedido")]
    [Produces("application/json")]
    public sealed class PedidoController(IPedidoRepository pedidoRepository, IPedidoService pedidoService, IPedidoItemRepository pedidoItemRepository ) : MainController
    {
        private readonly IPedidoRepository _pedidoRepository = pedidoRepository;
        private readonly IPedidoItemRepository _pedidoItemRepository = pedidoItemRepository;
        private readonly IPedidoService _pedidoService = pedidoService;

        /// <summary>
        /// rota para listar todos os pedidos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(Pedido), 200)]

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
                var pedido = await _pedidoRepository.GetByIdAsync(id);
                if (pedido is not { ValorTotal: >= 0, Numero: "" })
                if (pedido is { })
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
        /// <summary>
        /// Rota para criar pedido
        /// </summary>
        /// <param name="numero">numero do pedido</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Pedido), 201)]
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
        [HttpGet("listar-pedios/{pageNumber:int}/{pageSize:int}")]
        public async Task<IActionResult> ListarPedidos(int pageNumber = 1, int pageSize = 10)
        {
            LimparErrosProcessamento();
            try
            {
                var pedidos = await _pedidoRepository.ListaPaginada(pageNumber, pageSize);
                if (!pedidos.Any())
                {
                    AdicionarErroProcessamento("Não foi encontrado nenhum pedido");
                    return CustomResponse();
                }
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();
            }
        }
        /// <summary>
        /// rota para adicionar valor do pedido
        /// </summary>
        /// <param name="id">id do pedido</param>
        /// <param name="valor">valor do pedido</param>
        /// <returns></returns>
        [HttpPut("{id}/adicionar")]
        [ProducesResponseType(typeof(Pedido), 201)]

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
        /// <summary>
        /// rota para buscar pedido com seus items por id
        /// </summary>
        /// <param name="id">id do pedido-item</param>
        /// <returns></returns>
        [HttpGet("pedido-com-items/id:{id:guid}")]
        [ProducesResponseType(typeof(ItemPedido), 200)]
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
        /// <summary>
        /// rota listar todos os pedidos por item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("pedido-items/id:{id:guid}")]
        [ProducesResponseType(typeof(ItemPedido), 200)]
        public async Task<IActionResult> ListarPedidoItems(Guid id)
        {
            LimparErrosProcessamento();
            try
            {
                var pedidoItems = await _pedidoItemRepository.GetItemPedidoByIdAsync(id);
                if (pedidoItems is null)
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

        /// <summary>
        /// rota para adicionar um novo item no pedido
        /// </summary>
        /// <param name="id">id do pedido que vai ser adicionado</param>
        /// <param name="produtoId">id do produto</param>
        /// <param name="nomeProduto">nome do produto</param>
        /// <param name="quantidade">quantidade do item (produto)</param>
        /// <param name="precoUnitario">preço unitário do item (produto)</param>
        /// <returns></returns>
        [HttpPost("{id}/adicionar-item-pedido")]
        [ProducesResponseType(typeof(ItemPedido), 201)]
        public async Task<IActionResult> AdicionarItemPedido(Guid id, Guid produtoId,  string nomeProduto, int quantidade, decimal precoUnitario)
            { 
            LimparErrosProcessamento();
            try
            {
                var pedido = await _pedidoRepository.GetByIdAsync(id);
                if (pedido is not null)
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
