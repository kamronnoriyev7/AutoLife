using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Infrastructure.PaymentServices.PaymeServices;

public class PaymePaymentProvider : IPaymentProvider
{
    private readonly IConfiguration _config;

    public string Name => "Payme";

    public PaymePaymentProvider(IConfiguration config)
    {
        _config = config;
    }

    public string CreatePaymentUrl(decimal amount, string transactionId)
    {
        // Payme URL generator
        return $"https://checkout.paycom.uz/{transactionId}?amount={amount}";
    }

    public Task<bool> ProcessCallbackAsync(IFormCollection form)
    {
        // Payme callback check
        return Task.FromResult(true);
    }
}
