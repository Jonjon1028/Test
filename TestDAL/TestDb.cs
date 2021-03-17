using System;
using System.Linq;
using System.Threading.Tasks;
using TestBOL;

namespace TestDAL
{
    public interface ITestDb
    {
        IQueryable<BankAccount> GetById(int id);
        IQueryable<BankAccount> GetByUserId(string id);
        Task<bool> Create(BankAccount account);
        Task<bool> Deposit(int accountId, decimal amount);
        Task<bool> Withdraw(int accountId, decimal amount);
    }


    public class TestDb : ITestDb
    {
        readonly DbContext context;

        public TestDb(DbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Create(BankAccount account)
        {
            try
            {
                context.Add(account);
                var result = await context.SaveChangesAsync();
                if (result != 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Deposit(int accountId, decimal amount)
        {
            try
            {
                var deposit = context.BankAccounts.Find(accountId);
                deposit.Balance += amount;
                context.Update(deposit);

                var result = await context.SaveChangesAsync();
                if (result != 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<BankAccount> GetById(int id)
        {
            try
            {
                return context.BankAccounts.Where(x => x.AccountId == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        
        public IQueryable<BankAccount> GetByUserId(string id)
        {
            try
            {
                return context.BankAccounts.Where(x => x.Id == id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Withdraw(int accountId, decimal amount)
        {
            try
            {
                var w = context.BankAccounts.Find(accountId);
                if (w.Balance >= amount)
                {
                    w.Balance -= amount;
                    context.Update(w);
                }

                var result = await context.SaveChangesAsync();
                if (result != 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
