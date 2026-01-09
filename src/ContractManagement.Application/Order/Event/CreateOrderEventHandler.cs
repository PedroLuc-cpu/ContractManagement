using ContractManagement.Domain.Events.Pedidos;
using ContractManagement.Domain.Interfaces.Services;
using MediatR;

namespace ContractManagement.Application.Order.Event
{
    internal sealed class CreateOrderEventHandler(IEmailService emailService) : INotificationHandler<PedidoCriadoEvent>
    {
        private readonly IEmailService _emailService = emailService;

        public async Task Handle(PedidoCriadoEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"[EVENT] Pedido criado | PedidoId: {notification.IdPedido}");
            
            await _emailService.SendEmailAsync("teste@teste.com.br", "CreateOrderEventHandler", "Estou testando se o evendo foi enviado com sucesso");
        }
    }
}
