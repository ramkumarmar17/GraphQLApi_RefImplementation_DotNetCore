using CardApi;
using GraphQL.Types;

namespace Dashboard.GraphQL
{
    public class CardType : ObjectGraphType<Card>
    {
        public CardType()
        {
            Name = "Card";

            Field("id", x => x.Id);
            Field("number", x => x.Number);
            Field("fullName", x => x.FullName);
            Field("validFrom", x => x.ValidFrom);
            Field("validTo", x => x.ValidTo);
            Field("cvv", x => x.CVV);
            Field("type", x => x.Type);
        }

    }
}
