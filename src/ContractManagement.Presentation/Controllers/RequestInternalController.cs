using ContractManagement.Application.RequestInternal.Command;
using ContractManagement.Application.RequestInternal.Query;
using ContractManagement.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContractManagement.Presentation.Controllers
{
    [Authorize]
    [Route("solicitacao-interna")]
    [Produces("application/json")]
    public sealed class RequestInternalController(ISender sender) : MainController
    {
        private readonly ISender _sender = sender;

        [HttpPost("criar")]
        [ProducesResponseType(typeof(CreateRequestInternalCommand), 201)]
        public async Task<IActionResult> Add([FromBody] CreateRequestInternalCommand request, CancellationToken cancellationToken)
        {
            LimparErrosProcessamento();
            try
            {
                var command = new CreateRequestInternalCommand(request.IdFuncionario, request.Periodo, request.Titulo, request.Descricao);

                var result = await _sender.Send(command, cancellationToken);

                return result.IsSuccess ? Ok() : BadRequest(result.Error);                
            }catch (Exception ex)
            {
                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();
            }
        }
        [HttpPost("analisar")]
        [ProducesResponseType(typeof(AnalyzeRequestInternalCommand), 200)]
        public async Task<IActionResult> Analisar(AnalyzeRequestInternalCommand request, CancellationToken cancellationToken)
        {
            LimparErrosProcessamento();
            try
            {
                var command = new AnalyzeRequestInternalCommand(request.Id, request.Comentario);
                var result = await _sender.Send(command, cancellationToken);

                return result.IsSuccess ? Ok() : BadRequest(result.Error);
            }
            catch (Exception ex)
            {

                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();

            }
        }
        [HttpPost("aprovar")]
        [ProducesResponseType(typeof(ApproveRequestInternalCommand), 200)]
        public async Task<IActionResult> Aprovar(ApproveRequestInternalCommand request, CancellationToken cancellationToken)
        {
            LimparErrosProcessamento();
            try
            {
                var command = new ApproveRequestInternalCommand(request.Id, request.Comentario);
                var result = await _sender.Send(command, cancellationToken);

                return result.IsSuccess ? Ok() : BadRequest(result.Error);
            } catch(Exception ex) {

                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();

            }
        }
        
        [HttpPost("rejeitar")]
        [ProducesResponseType(typeof(RejectRequestInternalCommand), 200)]
        public async Task<IActionResult> Rejeitar(RejectRequestInternalCommand request, CancellationToken cancellationToken)
        {
            LimparErrosProcessamento();
            try
            {
                var command = new RejectRequestInternalCommand(request.Id, request.Comentario);
                var result = await _sender.Send(command, cancellationToken);

                return result.IsSuccess ? Ok() : BadRequest(result.Error);
            }
            catch (Exception ex)
            {

                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();

            }
        }
        [HttpPut("editar")]
        [ProducesResponseType(typeof(EditRequestInternalCommand), 200)]
        public async Task<IActionResult> Edit(EditRequestInternalCommand request, CancellationToken cancellationToken)
        {
            LimparErrosProcessamento();
            try
            {
                var command = new EditRequestInternalCommand(request.Id, request.Titulo, request.Descricao, request.Periodo, request.Comentario);

                var result = await _sender.Send(command, cancellationToken);

                return result.IsSuccess ? Ok(command) : BadRequest(result.Error);

            }
            catch (Exception ex)
            {
                AdicionarErroProcessamento(ex.Message); 
                return CustomResponse();
            }
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetRequestInternalResponse>), 200)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            LimparErrosProcessamento();
            try
            {
                var query = new GetAllRequestInternalQuery();

                Result<IEnumerable<GetRequestInternalResponse>> response = await _sender.Send(query, cancellationToken);

                return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
            }
            catch (Exception ex)
            {
                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();
            }
        }
    }
}
