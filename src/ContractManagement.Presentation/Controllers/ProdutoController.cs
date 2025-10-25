using ContractManagement.Domain.DTO;
using ContractManagement.Domain.Entity;
using ContractManagement.Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ContractManagement.Presentation.Controllers
{
    [Route("produto")]
    [Produces("application/json")]
    public sealed class ProdutoController(IProdutoRepository produtoRepository) : MainController
    {
        private readonly IProdutoRepository _produtoRepository = produtoRepository;

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
        [HttpGet("produtos")]
        [ProducesResponseType(typeof(IEnumerable<Produto>), 200)]
        public async Task<IActionResult> ObterProdutos()
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
    }
}
