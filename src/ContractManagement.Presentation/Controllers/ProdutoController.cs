using ContractManagement.Application.Product.Command;
using ContractManagement.Application.Product.Query;
using ContractManagement.Domain.Entity.Catalogo;
using ContractManagement.Domain.Interfaces;
using ContractManagement.Domain.Interfaces.Repository;
using ContractManagement.Domain.Shared;
using ContractManagement.Presentation.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContractManagement.Presentation.Controllers
{
    [Route("produto")]
    [Produces("application/json")]
    public sealed class ProdutoController(IProdutoRepository produtoRepository, ISender sender, IRedisCacheRepository redisCacheRepository) : MainController
    {
        private readonly IProdutoRepository _produtoRepository = produtoRepository;
        private readonly ISender _sender = sender;
        private readonly IRedisCacheRepository _redisCacheRepository = redisCacheRepository;

        [HttpPost]
        [ProducesResponseType(typeof(ProductRequest), 201)]
        public async Task<IActionResult> CriarProduto([FromBody] ProductRequest produtoRequest, CancellationToken cancellationToken)
        {
            LimparErrosProcessamento();
            try
            {
                var command = new CreateProductCommand(
                    produtoRequest.Nome,
                    produtoRequest.Observacao,
                    produtoRequest.UnidadeMedida,
                    produtoRequest.CodigoBarras,
                    produtoRequest.Codigo,
                    produtoRequest.PrecoVenda,
                    produtoRequest.PrecoCusto,
                    produtoRequest.EstoqueAtual,
                    produtoRequest.EstoqueMinino,
                    produtoRequest.EstoqueMaximo,
                    produtoRequest.Ativo);
                var result = await _sender.Send(command, cancellationToken);

                return result.IsSuccess ? Ok("Produto Cadastrado!") : BadRequest(result.Error);
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
        public async Task<IActionResult> ObterProdutos(CancellationToken cancellationToken)
        {
            LimparErrosProcessamento();
            try
            {
                var query = new GetAllProductQuery();

                Result<IEnumerable<GetProductResponse>> response = await _sender.Send(query, cancellationToken);

                return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
            }
            catch (Exception ex)
            {
                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();
            }
        }
        [HttpPut]
        [ProducesResponseType(typeof(ProdutoUpdateRequest), 200)]
        public async Task<IActionResult> Update([FromBody] ProdutoUpdateRequest produtoUpdateReq, CancellationToken cancellationToken)
        {
            LimparErrosProcessamento();
            try
            {
                var command = new UpdateProductCommand(
                    produtoUpdateReq.Name,
                    produtoUpdateReq.Cod,
                    produtoUpdateReq.Description,
                    produtoUpdateReq.UndMed,
                    produtoUpdateReq.CodBarr);

                var result = await _sender.Send(command, cancellationToken);
                return result.IsSuccess ? Ok("Produto foi atualizado!") : BadRequest(result);
                
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
