using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Infrastructure.PaymentServices;

public interface IPaymentProvider
{
    string Name { get; }

    /// <summary>
    /// To‘lov yaratish va foydalanuvchini to‘lov sahifasiga yo‘naltirish URL qaytarish.
    /// </summary>
    string CreatePaymentUrl(decimal amount, string transactionId);

    /// <summary>
    /// Tizimdan qaytgan callback ma’lumotlarini qayta ishlash.
    /// </summary>
    Task<bool> ProcessCallbackAsync(IFormCollection form);

}
