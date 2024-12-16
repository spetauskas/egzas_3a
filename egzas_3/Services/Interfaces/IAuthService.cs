using egzas_3.Entities;
using System.Security.Claims;

namespace egzas_3.Services.Interfaces;

public interface IAuthService
{
    Task<string> Login(string accountName, string accountPassword);
    string GenerateJwtToken(Account account);
    
}

