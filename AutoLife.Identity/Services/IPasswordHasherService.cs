using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Identity.Services;

public interface IPasswordHasherService
{
    string GenerateSalt();
    string HashPassword(string password, string salt);
    bool Verify(string enteredPassword, string storedHash, string salt);
}
