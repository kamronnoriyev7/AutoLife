using System.ComponentModel.DataAnnotations.Schema;

namespace AutoLife.Domain.Entities;

public class Country : BaseEntity
{
    public string UzName { get; set; } = string.Empty;
    public string RuName { get; set; } = string.Empty;
    public string EnName { get; set; } = string.Empty;

    public IEnumerable<Region>? Regions { get; set; }
}