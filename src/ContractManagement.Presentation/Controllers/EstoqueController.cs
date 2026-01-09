using ContractManagement.Application.Estoques.Command;
using ContractManagement.Application.ImportarNFe;
using ContractManagement.Application.NotaDeEntrada;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContractManagement.Presentation.Controllers
{
    [Route("estoque")]
    [Produces("application/json")]
    [Authorize]
    public sealed class EstoqueController(ISender sender) : MainController
    {
        private readonly ISender _sender = sender;

        [HttpPost("adicionar-estoque")]
        [ProducesResponseType(typeof(EstoqueCriadaCommand), 201)]
        public async Task<IActionResult> CriarEstoque([FromBody] EstoqueCriadaCommand request, CancellationToken cancellationToken)
        {
            LimparErrosProcessamento();
            try
            {
                var command = new EstoqueCriadaCommand(request.IdProduto, request.QuantidadeInicial);
                var result = await _sender.Send(command, cancellationToken);
                return result.IsSuccess ? Ok() : BadRequest(result.Error);
            }
            catch (Exception ex)
            {
                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();
            }
        }
        [HttpPost("registrar-nota-entrada")]
        [ProducesResponseType(typeof(RegistrarNotaEntradaCommand), 201)]
        public async Task<IActionResult> RegistrarNotaEntrada([FromBody] RegistrarNotaEntradaCommand request, CancellationToken cancellationToken)
        {
            LimparErrosProcessamento();
            try
            {
                var command = new RegistrarNotaEntradaCommand(request.NumeroDocumento, request.Itens);
                var result = await _sender.Send(command, cancellationToken);
                return result.IsSuccess ? Ok() : BadRequest(result.Error);
            }
            catch (Exception ex)
            {
                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();
            }
        }

        [HttpPost("importar-nfe")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ImportarNFe(IFormFile xml, CancellationToken cancellationToken)
        {
            LimparErrosProcessamento();
            try
            {
                if (xml.Length <= 0)
                {
                    AdicionarErroProcessamento("O arquivo xml não foi inserido para realizar a importação da nota de entrada.");
                    return CustomResponse();
                }

                using var reader = new StreamReader(xml.OpenReadStream());
                var xmlContent = await reader.ReadToEndAsync(cancellationToken);

                var command = new ImportarNFeCommand(xmlContent);
                var result = await _sender.Send(command, cancellationToken);

                return result.IsSuccess ? Ok(new { NotaEntradaId = result.Value }) : BadRequest(result.Error);
            }
            catch (Exception ex)
            {
                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();
            }
        }

    }
}
