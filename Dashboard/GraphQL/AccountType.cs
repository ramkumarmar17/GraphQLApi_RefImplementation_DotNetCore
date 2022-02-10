using Dashboard.Models;
using GraphQL.Types;

namespace Dashboard.GraphQL
{
    public class AccountType : ObjectGraphType<Account>
    {
        public AccountType()
        {
            Name = "Account";

            Field("customerId", x => x.CustomerId);
            Field("accountNumber", x => x.AccountNumber);
            Field("balance", x => x.Balance);
            Field("type", x => x.Type);
        }
    }
}
