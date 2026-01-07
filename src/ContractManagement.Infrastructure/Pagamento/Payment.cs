using ContractManagement.Domain.Dto;
using ContractManagement.Domain.Interfaces.Payment;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace ContractManagement.Infrastructure.Pagamento
{
    public class Payment : IPayments
    {
        private readonly StripeOptions _options;
        public Payment(IOptions<StripeOptions> options)
        {
            _options = options.Value;
            StripeConfiguration.ApiKey = _options.SecretKey;
        }
       public async Task<string> CriarCheckoutAsync(Guid IdPedido, IReadOnlyCollection<ItemCheckout> itens)
        {
            var sessionOptions = new SessionCreateOptions
            {
                Mode = "payment",
                PaymentMethodTypes = ["card"],

                LineItems = [.. itens.Select(i => new SessionLineItemOptions
                {
                    Quantity = i.Quantidade,
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "brl",
                        UnitAmount = i.ValorUnitarioEmCentavos,
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = i.Nome
                        }
                    }
                })],
                Metadata = new Dictionary<string, string>
                {
                    ["IdPedido"] = IdPedido.ToString(),
                },
                SuccessUrl = $"{_options.FrontEndUrl}/checkout/sucesso?pedidoId={IdPedido}",
                CancelUrl = $"{_options.FrontEndUrl}/checkout/cancelado?pedidoId={IdPedido}",
            };

            var service = new SessionService();
            var session = await service.CreateAsync(sessionOptions);

            return session.Url;
        }
    }
}
