using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class UserLocationHistory : BaseEntity
{
    public long Id { get; set; }

    public long UserId { get; set; }
    public User User { get; set; } = default!;

    public ICollection<Address>? Addresses { get; set; } 

    public DateTime LoggedAt { get; set; } = DateTime.UtcNow;
}
