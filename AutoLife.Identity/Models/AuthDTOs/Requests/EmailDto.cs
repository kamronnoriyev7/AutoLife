using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Identity.Models.AuthDTOs.Requests;

public class EmailDto
{
    public string To { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string Body { get; set; } = null!;
}
