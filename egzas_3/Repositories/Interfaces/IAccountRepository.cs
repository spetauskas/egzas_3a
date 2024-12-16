using egzas_3.Entities;

namespace egzas_3.Repositories.Interfaces;

public interface IAccountRepository
{
    Guid Create(Account model);
    void Delete(Guid id);
    bool Exists(Guid id);
    Account? Get(string userName);
}
