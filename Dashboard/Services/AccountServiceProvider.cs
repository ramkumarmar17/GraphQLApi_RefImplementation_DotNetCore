using Dashboard.Models;
using System.Collections.Generic;
using System.Linq;

namespace Dashboard.Services
{
    public interface IAccountServiceProvider
    {
        IEnumerable<Account> GetAccounts(string customerId);

        Account AddAccount(Account account);
    }

    public class AccountServiceProvider: IAccountServiceProvider
    {
        private List<Account> _accounts;

        public AccountServiceProvider()
        {
            _accounts = GetAllAccounts();
        }

        public IEnumerable<Account> GetAccounts(string customerId)
        {
            return _accounts.Where(a => a.CustomerId.Equals(customerId));
        }

        public Account AddAccount(Account account)
        {
            _accounts.Add(account);
            return account;
        }

        private List<Account> GetAllAccounts()
        {
            return new List<Account>
            {
                new Account { CustomerId = "110011", AccountNumber = "11111111", Balance = 100, Type = "Savings"},
                new Account { CustomerId = "110011", AccountNumber = "11111222", Balance = 500, Type = "Fixed"},
                new Account { CustomerId = "220022", AccountNumber = "22222222", Balance = 200, Type = "Savings"}
            };
        }
    }
}
