using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using egzas_3.Entities;
using egzas_3.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace egzas_3.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;



        public AuthService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> Login(string username, string password)
        {
            var accountService = new AccountService();
            // Find the account
            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.AccountName == username);

            // Validate account exists and password is correct
            if (account == null ||
                !accountService.VerifyPasswordHash(password, account.AccountPasswordHash, account.AccountPasswordSalt))
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            // Generate and return JWT token
            return GenerateJwtToken(account);
        }

        public string GenerateJwtToken(Account account)
        {
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ??
                    throw new InvalidOperationException("JWT Key is not configured"))
            );
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, account.AccountId.ToString()),
                new Claim(ClaimTypes.Name, account.AccountName),
                new Claim(ClaimTypes.Role, account.AccountRole)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        
    }
}