using AutoLife.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities.PaymentTransactions;

public class PaymentTransaction : BaseEntity
{
    public Guid Id { get; set; }

    public required string Provider { get; set; }
    public Guid TransactionId { get; set; }
    public decimal Amount { get; set; }
    public PaymentStatus Status { get; set; }

    public Guid UserId { get; set; }
    public required User User { get; set; }
}

