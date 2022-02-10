using Dashboard.Models;
using Dashboard.Services;
using GraphQL;
using GraphQL.Types;

namespace Dashboard.GraphQL
{
    //This class has all the resolvers used to handle the GraphQL Mutations
    public class AccountMutation : ObjectGraphType
    {
        public AccountMutation(IAccountServiceProvider accountServiceProvider)
        {
            Field<AccountType>(
              "createAccount",
              arguments: new QueryArguments(new QueryArgument<NonNullGraphType<AccountInputType>> { Name = "account" }),
              resolve: context =>
              {
                  var account = context.GetArgument<Account>("account");
                  return accountServiceProvider.AddAccount(account);
              });
        }
    }
}
