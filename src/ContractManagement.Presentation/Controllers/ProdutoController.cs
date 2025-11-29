using ContractManagement.Application.Product.Command;
using ContractManagement.Application.Product.Query;
using ContractManagement.Domain.Entity.Catalogo;
using ContractManagement.Domain.Interfaces.Repository;
using ContractManagement.Domain.Shared;
using ContractManagement.Infrastructure.Identity;
using ContractManagement.Presentation.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ContractManagement.Presentation.Controllers
{
    [Authorize]
    [Route("product")]
    [Produces("application/json")]
    public sealed class ProdutoController(IProdutoRepository produtoRepository, ISender sender, SignInManager<ApplicationUser> signInManager, IHttpContextAccessor acessor) : MainController
    {
        private readonly IProdutoRepository _produtoRepository = produtoRepository;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly IHttpContextAccessor _accessor = acessor;

        private readonly ISender _sender = sender;

        [HttpPost]
        [ProducesResponseType(typeof(ProductRequest), 201)]
        public async Task<IActionResult> CriarProduto([FromForm] ProductRequest produtoRequest, CancellationToken cancellationToken)
        {
            LimparErrosProcessamento();
            try
            {
                byte[]? imagemToBytes = null;

                if (produtoRequest.Imagem is not null)
                {
                    using var ms = new MemoryStream();
                    await produtoRequest.Imagem.CopyToAsync(ms);
                    imagemToBytes = ms.ToArray();
                }

                var command = new CreateProductCommand(
                    produtoRequest.Nome,
                    imagemToBytes,
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
                var userAcessor = _accessor.HttpContext.User.Identity.Name;

                Console.WriteLine(userAcessor);
                var user = await _signInManager.UserManager.FindByEmailAsync("pedrolucas@bemasoft.com.br");
                if (user is null)
                {
                    return CustomResponse("Não existe esse usuário logado por aqui");
                }
                Console.WriteLine(user);

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
                    produtoUpdateReq.Imagem,
                    produtoUpdateReq.Cod,
                    produtoUpdateReq.Description,
                    produtoUpdateReq.UndMed,
                    produtoUpdateReq.CodBarr,
                    produtoUpdateReq.Active);

                var result = await _sender.Send(command, cancellationToken);
                return result.IsSuccess ? Ok("Produto foi atualizado!") : BadRequest(result);
                
            }
            catch (Exception ex)
            {
            
                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();
                
            }
        }


        [HttpPost("create-promotion")]

        [ProducesResponseType(typeof(IEnumerable<Produto>), 201)]
        public async Task<IActionResult> CriarPromocao([FromBody] CreatePromotionCommand command, CancellationToken cancellationToken)
        {
            var result = await _sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(command) : BadRequest(result.Error);
        }
    }

}
