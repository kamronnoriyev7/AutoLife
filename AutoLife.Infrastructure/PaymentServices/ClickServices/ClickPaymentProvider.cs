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
        var merchantId = _config["ClickSettings:MerchantId"];
        var serviceId = _config["ClickSettings:ServiceId"];
        var apiUrl = _config["ClickSettings:ApiUrl"];

        return $"{apiUrl}/payment?merchant_id={merchantId}&service_id={serviceId}" +
               $"&transaction_param={transactionId}&amount={amount}";
    }

    public async Task<bool> ProcessCallbackAsync(IFormCollection form)
    {
        var secretKey = _config["ClickSettings:SecretKey"];
        var providedSign = form["sign_string"];

        var rawString = $"{form["click_trans_id"]}{form["service_id"]}{secretKey}";
        var calculatedSign = BitConverter.ToString(
            System.Security.Cryptography.MD5.Create().ComputeHash(
                System.Text.Encoding.UTF8.GetBytes(rawString)
            )).Replace("-", "").ToLower();

        // Bu yerda DB’ga yozish yoki boshqa biznes logika bo‘lishi mumkin
        return await Task.FromResult(calculatedSign == providedSign);
    }
}
