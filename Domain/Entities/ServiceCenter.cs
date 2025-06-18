using AutoLife.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class ServiceCenter : BaseEntity
{
    public long Id { get; set; }
    public string Name { get; set; } = default!;
    public Address Address { get; set; } = new ();
    public ICollection<Image> Images { get; set; } = new List<Image>();
    public string? Description { get; set; }
    public string? PhoneNumber { get; set; }
    public ServiceType ServiceType { get; set; }
}
