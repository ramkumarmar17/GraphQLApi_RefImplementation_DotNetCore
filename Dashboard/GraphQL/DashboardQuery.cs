using Dashboard.Services;
using GraphQL;
using GraphQL.Types;

namespace Dashboard.GraphQL
{
	//This class has all the resolvers used to fetch individual objects specified in the GraphQL Query
    public class DashboardQuery : ObjectGraphType
	{
		public DashboardQuery(IDashboardServiceProvider dashboardServiceProvider, IAccountServiceProvider accountServiceProvider)
		{
			//Name = "dashboardQuery";

			Field<ListGraphType<CardType>>(
				name: "cards", 
				resolve: context =>
				{
					return dashboardServiceProvider.GetCards();
				}
			);

			Field<CardType>(
				name: "card",
				arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "cardId" }),
				resolve: context =>
				{
					string cardId = context.GetArgument<string>("cardId");
					return dashboardServiceProvider.GetCard(cardId);
				}
			);

			Field<ListGraphType<CustomerType>>(
				name: "customers", 
				resolve: context =>
				{
					return dashboardServiceProvider.GetCustomers();
				}
			);

			Field<CustomerType>(
				name: "customer",
				arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "customerId" }),
				resolve: context =>
				{
					string customerId = context.GetArgument<string>("customerId");
					return dashboardServiceProvider.GetCustomer(customerId);
				}
			);

			Field<ListGraphType<AccountType>>(
				name: "accounts",
				arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "customerId" }),
				resolve: context =>
				{
					string customerId = context.GetArgument<string>("customerId");
					return accountServiceProvider.GetAccounts(customerId);
				}
			);

		}
	}
}
