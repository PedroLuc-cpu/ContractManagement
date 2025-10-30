using ContractManagement.Domain.DTO;
using ContractManagement.Domain.Entity.Clientes;
using ContractManagement.Domain.Interfaces.Repository.Clientes;
using Microsoft.AspNetCore.Mvc;

namespace ContractManagement.Presentation.Controllers
{
    [Route("cliente")]
    [Produces("application/json")]
    public sealed class ClienteController(IClienteRepository clienteRepository) : MainController
    {
        private readonly IClienteRepository _clienteRepository = clienteRepository;

        [HttpGet("clientes/{pageNumber:int}/{pageSize:int}")]
        [ProducesResponseType(typeof(IEnumerable<Cliente>), 200)]
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
        [ProducesResponseType(typeof(Cliente), 200)]
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
        [ProducesResponseType(typeof(Cliente), 200)]
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
        public async Task<IActionResult> CriarCliente([FromBody] ClienteRequestDto cliente, CancellationToken cancellationToken = default)
        {
            LimparErrosProcessamento();
            try
            {
                var existingClient = await _clienteRepository.GetByEmailAsync(cliente.Email, cancellationToken);
                if (existingClient is not null)
                {
                    AdicionarErroProcessamento("Já existe um cliente cadastrado com esse email.");
                    return CustomResponse();
                }

                await _clienteRepository.CreateClientAsync(cliente.Nome, cliente.Sobrenome, cliente.Email, cliente.Endereco, cancellationToken);
                return Ok("Cliente foi salvo com sucesso.");
            }
            catch (Exception ex)
            {
                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();
            }
        }
        [HttpPut]
        [ProducesResponseType(typeof(Cliente), 200)]
        public async Task<IActionResult> AtualizarCliente([FromBody] ClienteUpdateRequestDto cliente, CancellationToken cancellationToken = default)
        {
            LimparErrosProcessamento();
            try
            {
                var existingClient = await _clienteRepository.GetByEmailAsync(cliente.Email.Value, cancellationToken);
                if (existingClient is null)
                {
                    AdicionarErroProcessamento("Cliente não encontrado.");
                    return CustomResponse();
                }
                await _clienteRepository.UpdateClientAsync(cliente, cancellationToken);
                return Ok("Cliente atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                AdicionarErroProcessamento(ex.Message);
                return CustomResponse();
            }
        }
    }
}
