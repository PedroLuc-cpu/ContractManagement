using ContractManagement.Application.Product.Command;
using ContractManagement.Domain.DTO;
using ContractManagement.Domain.Entity.Catalogo;
using ContractManagement.Domain.Interfaces.Repository;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace ContractManagement.Presentation.Controllers
{
    [Route("produto")]
    [Produces("application/json")]
    public sealed class ProdutoController(IProdutoRepository produtoRepository, ISender sender) : MainController
    {
        private readonly IProdutoRepository _produtoRepository = produtoRepository;
        private readonly ISender _sender = sender;

        [HttpPost]
        [ProducesResponseType(typeof(Produto), 201)]
        public async Task<IActionResult> CriarProduto([FromBody] ProdutoRequestDto produto)
        {
            LimparErrosProcessamento();
            try
            {
                var existingProduto = await _produtoRepository.GetByCodigoAsync(produto.Codigo);
                if (existingProduto is not null)
                {
                    return CustomResponse("Já existe um produto com esse código.");
                }

                await _produtoRepository.CreateProduto(produto);
                return CreatedAtAction(nameof(ObterProdutoPorCodigo), new { codigo = produto.Codigo }, produto);
            }
            catch (Exception ex)
            {
                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();
            }
        }
        [HttpGet("{codigo}")]
        [ProducesResponseType(typeof(Produto), 200)]
        public async Task<IActionResult> ObterProdutoPorCodigo(string codigo)
        {
            LimparErrosProcessamento();
            try
            {
                var produto = await _produtoRepository.GetByCodigoAsync(codigo);
                if (produto is null)
                {
                    return CustomResponse("Produto não encontrado.");
                }
                return Ok(produto);
            }
            catch (Exception ex)
            {
                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();
            }
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Produto>), 200)]
        public async Task<IActionResult> ObterProdutos(IDistributedCache cache)
        {
            LimparErrosProcessamento();
            try
            {
                var produtos = await _produtoRepository.GetAllAsync();
                if (!produtos.Any())
                {
                    return CustomResponse("Nenhum produto encontrado.");
                }
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();
            }
        }
        [HttpPut]
        [ProducesResponseType(typeof(Produto), 200)]
        public async Task<IActionResult> Update([FromBody] ProdutoRequestDto produto, CancellationToken cancellationToken)
        {
            LimparErrosProcessamento();
            try
            {
                var produtoExist = await _produtoRepository.GetByCodigoAsync(produto.Codigo, cancellationToken);
                if (produtoExist is null)
                {
                    AdicionarErroProcessamento("Nâo foi encontrado nenhum produto com código informado");
                    return CustomResponse();
                }
                await _produtoRepository.UpdateProduto(produto, cancellationToken);
                return Ok(produtoExist);
            }
            catch (Exception ex)
            {
            
                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();
                
            }
        }


        [HttpPost("criar-promocao")]

        [ProducesResponseType(typeof(IEnumerable<Produto>), 201)]
        public async Task<IActionResult> CriarPromocao([FromBody] CreatePromotionCommand command, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(command) : BadRequest(result.Error);
        }


    }
}
