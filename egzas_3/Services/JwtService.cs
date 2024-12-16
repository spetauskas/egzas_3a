﻿using egzas_3.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using egzas_3.Services.Interfaces;


namespace egzas_3.Services;

public class JwtService : IJwtService
{
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;
    public JwtService(IConfiguration conf)
    {
        _secretKey = conf.GetValue<string>("Jwt:Key") ?? "";
        _issuer = conf.GetSection("Jwt:Issuer").Value ?? "";
        _audience = conf.GetSection("Jwt:Audience").Value ?? "";
    }
    public string GetJwtToken(Account account)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new (ClaimTypes.NameIdentifier, account.AccountId.ToString()),
                new (ClaimTypes.Name, account.AccountName.ToString()),
                new (ClaimTypes.Role, account.AccountRole),
                new (ClaimTypes.Email, account.AccountEmail)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _issuer,
            Audience = _audience
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
