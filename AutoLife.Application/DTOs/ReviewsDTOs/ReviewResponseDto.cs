using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Application.DTOs.ReviewsDTOs;

public class ReviewResponseDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = default!;
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
}

