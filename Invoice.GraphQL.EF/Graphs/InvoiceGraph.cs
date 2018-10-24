using GraphQL.EntityFramework;
using System.Linq;

public class InvoiceGraph : EfObjectGraphType<Invoice.BL.Models.Invoice>
{
    public InvoiceGraph(IEfGraphQLService graphQlService) : base(graphQlService)
    {
        Field(x => x.Id);
        Field(x => x.Description);
        AddNavigationField<InvoiceItemGraph, Invoice.BL.Models.InvoiceItem>(
            name: "items",
            resolve: context => context.Source.InvoiceItems
        );
        AddNavigationConnectionField<InvoiceItemGraph, Invoice.BL.Models.InvoiceItem>(
            name: "itemsConnection",
            resolve: context => context.Source.InvoiceItems,
            includeNames: new[] { "InvoiceItem" }
        );
    }
}