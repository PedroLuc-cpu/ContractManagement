using ContractManagement.Application.Pagamento.Command;
using ContractManagement.Infrastructure.Pagamento;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace ContractManagement.Presentation.Controllers
{
    [Route("payment")]
    [Produces("application/json")]
    [Authorize]
    public sealed class CheckoutController(ISender sender, IOptions<StripeOptions> options ): MainController
    {
        private readonly ISender _sender = sender;
        private readonly StripeOptions _options = options.Value;


        [HttpPost("{id}/checkout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Checkout(Guid id)
        {
            var result = await _sender.Send(new IniciarPagamentoCommand(id));

            return result.IsSuccess ? Ok(new {url = result.Value }) : BadRequest(result.Error);
        }

        [HttpPost("webhooks/stripe")]
        public async Task<IActionResult> StripeWebHook()
        {
            try
            {
                var json = await new StreamReader(Request.Body).ReadToEndAsync();
                var signature = Request.Headers["Stripe-Signature"].FirstOrDefault();

                if (string.IsNullOrEmpty(signature))
                {
                    return BadRequest("Stripe-Signature header missing");
                }

                var stripeEvent = EventUtility.ConstructEvent(json, signature, _options.WebhookSecret);

                if (stripeEvent.Type == "checkout.session.completed")
                {
                    var session = (Session)stripeEvent.Data.Object;
                    var IdPedido = Guid.Parse(session.Metadata["IdPedido"]);

                    await _sender.Send(new ConfirmarPagamentoCommand(IdPedido));
                }

                return Ok();
            } catch (StripeException ex)
            {
                return BadRequest(ex);
            }
            
        }
    }
}
