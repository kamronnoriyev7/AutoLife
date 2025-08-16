using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Infrastructure.PaymentServices;

public class PaymentService
{
    private readonly Dictionary<string, IPaymentProvider> _providers;

    public PaymentService(IEnumerable<IPaymentProvider> providers)
    {
        // Keylarni Name property orqali sozlaymiz (masalan, "Click", "Payme")
        _providers = providers.ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);
    }

    public IPaymentProvider GetProvider(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Provider nomi bo‘sh bo‘lishi mumkin emas.", nameof(name));

        if (!_providers.TryGetValue(name, out var provider))
            throw new KeyNotFoundException($"'{name}' nomli payment provider topilmadi.");

        return provider;
    }
}
