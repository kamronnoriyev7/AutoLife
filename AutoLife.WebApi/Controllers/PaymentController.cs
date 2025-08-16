using AutoLife.Infrastructure.PaymentServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoLife.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;

        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("create")]
        public IActionResult CreatePayment(string provider, decimal amount, string transactionId)
        {
            var paymentProvider = _paymentService.GetProvider(provider);
            var url = paymentProvider.CreatePaymentUrl(amount, transactionId);
            return Ok(new { paymentUrl = url });
        }

        [HttpPost("callback/{provider}")]
        public async Task<IActionResult> Callback(string provider, [FromForm] IFormCollection form)
        {
            var paymentProvider = _paymentService.GetProvider(provider);
            var success = await paymentProvider.ProcessCallbackAsync(form);

            return Ok(new { success });
        }
    }

}
