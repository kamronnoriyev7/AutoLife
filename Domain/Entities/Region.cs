using System.ComponentModel.DataAnnotations.Schema;

namespace AutoLife.Domain.Entities;

public class Region : BaseEntity
{
    public string UzName { get; set; } = string.Empty;
    public string RuName { get; set; } = string.Empty;
    public string EnName { get; set; } = string.Empty;

    public long CountryId { get; set; }
    public Country? Country { get; set; }

    public IEnumerable<District>? Districts { get; set; }
}