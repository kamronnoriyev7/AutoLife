using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Application.DTOs.ReviewsDTOs;

public class CreateReviewDto
{
    public Guid TargetId { get; set; }  // zapravka yoki servis center Id
    public string TargetType { get; set; } = default!; // "fuelstation" yoki "servicecenter"
    public int Rating { get; set; } // 1 - 5
    public string? Comment { get; set; }
}

