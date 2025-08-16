using System.ComponentModel.DataAnnotations.Schema;

namespace AutoLife.Domain.Entities;

public class Region : BaseEntity
{
    public string UzName { get; set; } = string.Empty;
    public string RuName { get; set; } = string.Empty;
    public string EnName { get; set; } = string.Empty;

    public Guid CountryId { get; set; }
    public Country Country { get; set; } = default!;

    public ICollection<District> Districts { get; set; } = new List<District>();
}
