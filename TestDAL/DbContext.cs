using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using TestBOL;

namespace TestDAL
{
    public class DbContext : IdentityDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@""); // DB ConnectionString here
        }

        public DbSet<BankAccount> BankAccounts { get; set; }
    }
}
