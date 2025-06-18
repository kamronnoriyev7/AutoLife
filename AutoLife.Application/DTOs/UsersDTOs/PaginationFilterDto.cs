using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Application.DTOs.UsersDTOs;

public class PaginationFilterDto
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

