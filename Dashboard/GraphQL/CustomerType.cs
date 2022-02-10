using CustomerApi;
using GraphQL.Types;

namespace Dashboard.GraphQL
{
    public class CustomerType : ObjectGraphType<Customer>
    {
        public CustomerType()
        {
            Name = "Customer";

            Field("id", x => x.Id);
            Field("fullName", x => x.FullName);
            Field("dateOfBirth", x => x.DateOfBirth);
            Field("gender", x => x.Gender);
            Field("socialSecurityId", x => x.SocialSecurityId);
            Field("occupation", x => x.Occupation);
            Field("age", x => x.Age);

            Field<AddressType>("address", resolve: context => context.Source.Address);
        }

    }
}
