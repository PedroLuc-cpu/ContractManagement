using ContractManagement.Application.Client.Command;
using ContractManagement.Domain.Entity.Clientes;
using ContractManagement.Domain.Interfaces.Repository.Clientes;
using ContractManagement.Presentation.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ContractManagement.Presentation.Controllers
{
    [Route("client")]
    [Produces("application/json")]
    [Authorize]
    public sealed class ClienteController(IClienteRepository clienteRepository, ISender sender) : MainController
    {
        private readonly IClienteRepository _clienteRepository = clienteRepository;
        private readonly ISender _sender = sender;


        [HttpGet("clientes/{pageNumber:int}/{pageSize:int}")]
        [ProducesResponseType(typeof(IEnumerable<ClienteResponse>), 200)]
        public async Task<IActionResult> ObterClientes(int pageNumber, int pageSize)
        {
            LimparErrosProcessamento();
            try
            {
                var clientes = await _clienteRepository.GetAllClientsAsync(pageNumber, pageSize);
                if (!clientes.Any())
                {
                    return CustomResponse("Nenhum cliente encontrado.");
                }
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();
            }
        }
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ClienteResponse), 200)]
        public async Task<IActionResult> ObterClientePorId(Guid id)
        {
            LimparErrosProcessamento();
            try
            {
                var cliente = await _clienteRepository.GetByIdAsync(id);
                if (cliente is null)
                {
                    return CustomResponse("Cliente não encontrado.");
                }
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();
            }
        }
        [HttpGet("email/{email}")]
        [ProducesResponseType(typeof(ClienteResponse), 200)]
        public async Task<IActionResult> ObterClientePorNome(string email)
        {
            LimparErrosProcessamento();
            try
            {
                var clientes = await _clienteRepository.GetByEmailAsync(email);
                if (clientes is null)
                {
                    return CustomResponse("Nenhum cliente encontrado com esse email.");
                }

                return Ok(clientes);
            }
            catch (Exception ex)
            {
                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();
            }
        }
        [HttpPost]
        [ProducesResponseType(typeof(Cliente), 201)]
        public async Task<IActionResult> CriarCliente([FromBody] ClienteRequest cliente, CancellationToken cancellationToken)
        {
            LimparErrosProcessamento();
            try
            {
                var command = new CreateClientCommand(
                    cliente.Nome,
                    cliente.Sobrenome,
                    cliente.Email,
                    cliente.Endereco.Rua,
                    cliente.Endereco.Numero,
                    cliente.Endereco.Cidade,
                    cliente.Endereco.Estado,
                    cliente.Endereco.Cep);

                var result = await _sender.Send(command, cancellationToken);

                return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
            }
            catch (Exception ex)
            {
                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();
            }
        }
        [HttpPut]
        [ProducesResponseType(typeof(ClienteUpdateRequest), 200)]
        public async Task<IActionResult> AtualizarCliente([FromBody] ClienteUpdateRequest clienteReq, CancellationToken cancellationToken)
        {
            LimparErrosProcessamento();
            try
            {
                var commmand = new UpdateClientCommand(
                    clienteReq.Nome,
                    clienteReq.Sobrenome,
                    clienteReq.Email,
                    clienteReq.Endereco.Rua,
                    clienteReq.Endereco.Numero,
                    clienteReq.Endereco.Cidade,
                    clienteReq.Endereco.Cidade,
                    clienteReq.Endereco.Cep);

                var result = await _sender.Send(commmand, cancellationToken);
                return result.IsSuccess ? Ok("Cliente atualizado com sucesso") : BadRequest(result);

            }
            catch (Exception ex)
            {
                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();
            }
        }
    }
}
