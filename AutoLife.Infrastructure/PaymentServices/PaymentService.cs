using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Infrastructure.PaymentServices;

public class PaymentService
{
    private readonly IEnumerable<IPaymentProvider> _providers;

    public PaymentService(IEnumerable<IPaymentProvider> providers)
    {
        _providers = providers;
    }

    public IPaymentProvider GetProvider(string name)
    {
        var provider = _providers.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (provider == null)
            throw new ArgumentException($"Payment provider '{name}' not found.");

        return provider;
    }
}
