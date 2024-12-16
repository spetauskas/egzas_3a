using egzas_3.Entities;
using egzas_3.InitialData;
using Microsoft.EntityFrameworkCore;

namespace egzas_3
{
    public class ApplicationDbContext : DbContext
    {
        public bool SkipSeeding { get; set; } = false;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Account> Accounts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            if (!SkipSeeding)
            {
                modelBuilder.Entity<Account>()
                    .HasData(AccountInitialDataSeed.Accounts);
                modelBuilder.Entity<User>()
                    .HasData(UsersInitialDataSeed.Users);
            }


            //// Configure relationships with fluent API
            //modelBuilder.Entity<User>()
            //    .HasOne(u => u.Account)
            //    .WithOne(a => a.User)
            //    .HasForeignKey<User>(u => u.AccountId);
            //    //.OnDelete(DeleteBehavior.Cascade); // or .Restrict based on your requirements


        }
    }
}

