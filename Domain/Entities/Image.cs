using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class Image : BaseEntity
{
    public long Id { get; set; }
    public long EntityId { get; set; }
    public string ImageUrl { get; set; } = default!;
}
