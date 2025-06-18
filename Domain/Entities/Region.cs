using System.ComponentModel.DataAnnotations.Schema;

namespace AutoLife.Domain.Entities;

public class Region : BaseEntity
{
    public string UzName { get; set; } = string.Empty;
    public string RuName { get; set; } = string.Empty;
    public string EnName { get; set; } = string.Empty;

    [ForeignKey(nameof(Country))]
    public Guid CountryId { get; set; }
    public Country? Country { get; set; }

    [InverseProperty(nameof(District.Region))]
    public IEnumerable<District>? Districts { get; set; }
}