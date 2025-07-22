using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Identity.Services;

public  class PasswordHasherService : IPasswordHasherService
{
    public string GenerateSalt()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(16));
    }

    public string HashPassword(string password, string salt)
    {
        using var sha = SHA256.Create();
        var combined = Encoding.UTF8.GetBytes(password + salt);
        return Convert.ToBase64String(sha.ComputeHash(combined));
    }


    public bool Verify(string enteredPassword, string storedHash, string salt)
    {
        var hash = HashPassword(enteredPassword, salt);
        return hash == storedHash;
    }
}
