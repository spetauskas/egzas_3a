using egzas_3.Entities;
using egzas_3.Repositories.Interfaces;

namespace egzas_3.Repositories;
public class AccountRepository : IAccountRepository
{
    private readonly ApplicationDbContext _context;

    public AccountRepository(ApplicationDbContext context)
    {
        //context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        _context = context;
    }

    public Guid Create(Account model)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model));

        var exists = _context.Accounts.Any(x => x.AccountName == model.AccountName);
        if (exists)
            throw new ArgumentException("Username already exists");

        _context.Accounts.Add(model);
        _context.SaveChanges();
        return model.AccountId;
    }
    public Account? Get(string AccountName)
    {
        if (AccountName == null)
            throw new ArgumentNullException(nameof(AccountName));

        return _context.Accounts.FirstOrDefault(x => x.AccountName == AccountName);
    }
    public bool Exists(Guid id)
    {
        return _context.Accounts.Any(x => x.AccountId == id);
    }
    public void Delete(Guid id)
    {
        var account = _context.Accounts.Find(id);
        if (account != null)
        {
            _context.Accounts.Remove(account);
            _context.SaveChanges();
        }
    }
}
