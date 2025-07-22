using AutoLife.Identity.Models.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Identity.Services;

public interface ITokenService
{
    string GenerateAccessToken(IdentityUser user);
}
