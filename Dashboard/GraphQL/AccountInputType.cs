using GraphQL.Types;

namespace Dashboard.GraphQL
{
    public class AccountInputType : InputObjectGraphType
    {
        public AccountInputType()
        {
            Name = "AccountInput";
            Field<NonNullGraphType<StringGraphType>>("customerId");
            Field<NonNullGraphType<StringGraphType>>("accountNumber");
            Field<NonNullGraphType<StringGraphType>>("type");
            Field<NonNullGraphType<StringGraphType>>("balance");
        }
    }
}
