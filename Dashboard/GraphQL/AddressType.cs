using CustomerApi;
using GraphQL.Types;

namespace Dashboard.GraphQL
{
    public class AddressType : ObjectGraphType<Address>
    {
        public AddressType()
        {
            Name = "Address";

            Field("building", x => x.Building);
            Field("area", x => x.Area);
            Field("street", x => x.Street);
            Field("city", x => x.City);
            Field("state", x => x.State);
            Field("zipCode", x => x.ZipCode);
            Field("country", x => x.Country);
        }

    }
}
