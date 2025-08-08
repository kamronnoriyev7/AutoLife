using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Infrastructure.PaymentServices.ClickServices;

public class ClickPaymentProvider : IPaymentProvider
{
    private readonly IConfiguration _config;

    public string Name => "Click"; // Payment service name

    public ClickPaymentProvider(IConfiguration config)
    {
        _config = config;
    }

    public string CreatePaymentUrl(decimal amount, string transactionId)
    {
        var merchantId = _config["Click:MerchantId"];  
        var serviceId = _config["Click:ServiceId"];
        var returnUrl = _config["Click:ReturnUrl"];

        return $"https://my.click.uz/services/pay?service_id={serviceId}&merchant_id={merchantId}&amount={amount}&transaction_param={transactionId}&return_url={returnUrl}";
    }

    public Task<bool> ProcessCallbackAsync(IFormCollection form)
    {
        var status = form["error"];
        return Task.FromResult(status == "0"); // 0 => success
    }
}
