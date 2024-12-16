using egzas_3.Entities;

namespace egzas_3.Services.Interfaces;

public interface IJwtService
{
    string GetJwtToken(Account account);
}
