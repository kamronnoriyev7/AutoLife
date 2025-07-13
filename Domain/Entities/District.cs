using System.ComponentModel.DataAnnotations.Schema;

namespace AutoLife.Domain.Entities;

public class District : BaseEntity
{
    public string UzName { get; set; } = string.Empty;
    public string RuName { get; set; } = string.Empty;
    public string EnName { get; set; } = string.Empty;

    [ForeignKey(nameof(Region))]
    public long RegionId { get; set; }
    public Region? Region { get; set; }
}